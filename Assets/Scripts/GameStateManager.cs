using Assets.Scripts;
using Assets.Scripts.player_actions;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System;

public class GameStateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string state;
    private List<EntityScript> gameEntities = new List<EntityScript>();
    private List<EntityScript> enemies = new List<EntityScript>();
    private WorldManager worldManager;
    [SerializeField] public CooldownManager cd;
    public PlayerScript player;

    public Dictionary<EntityScript, CharacterSnapshot> snapshot;

    private Dictionary<EntityScript, AAction> currentActions;

    [SerializeField] public GameObject slashPrefab;
    [SerializeField] public GameObject explosionPrefab;
    [SerializeField] public GameObject shieldPrefab;
    [SerializeField] public GameObject goblinSlashPrefab;
    [SerializeField] public GameObject moveAnimationPrefab;
    [SerializeField] public GameObject ghostSlashPrefab;
    [SerializeField] public Sprite archerAttackSprite;
    [SerializeField] public Sprite archerDefaultSprite;
    [SerializeField] private float actionRoundDelay = 1f;

    [SerializeField] private ActionUIManager actionUIManager;

    public static GameStateManager Instance;
    [SerializeField] AudioSource backgroundMusic;
    void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            backgroundMusic.volume = 1;
        }
        worldManager = WorldManager.Instance;
        player = FindObjectOfType<PlayerScript>();
        state = "prep";
        if (Instance == null) {
            Instance = this;
    }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (enemies.Count == 0)
        {
            worldManager = WorldManager.Instance;
            worldManager.victory();
            //SceneManager.LoadScene("Overworld");
        }
    }

    void CreateSnapshot()
    {
        snapshot = new Dictionary<EntityScript, CharacterSnapshot>();

        foreach (var c in gameEntities)
        {
            snapshot[c] = new CharacterSnapshot
            {
                position = c.transform.position,
                hp = c.currentHealth,
                isBlocking = c.isBlocking
            };
        }
    }


    public GameObject GetSlashPrefab()
    {
        return slashPrefab;
    }
    public GameObject GetExplosionPrefab()
    {
        return explosionPrefab;
    }

    public GameObject GetShieldPrefab()
    {
        return shieldPrefab;
    }

    public GameObject GetGoblinSlashPrefab()
    {
        return goblinSlashPrefab;
    }

    public GameObject GetMoveAnimationPrefab()
    {
        return moveAnimationPrefab;
    }

    public GameObject GetGhostSlashPrefab()
    {
        return ghostSlashPrefab;
    }

    public Vector2 getUIPosition()
    {
        return actionUIManager.getUIPosition();
    }


    public void newAction(AAction action)
    {
        if (cd.onCooldown(action)) return;
        player.EnqueueAction(action);
        if (action.getCooldown() > 0)
        {
            cd.addCooldown(action);
        }
        actionUIManager.UpdateActionUI(action.getSprite());
    }

    public void Indicate(Vector3 targetPosition, GameObject indicator)
    {
        actionUIManager.Indicate(targetPosition, indicator);
    }

    public void newMove(MoveAction action)
    {
        if (cd.onCooldown(action)) return;
        if (player.fullActionQueue()) return;
        player.EnqueueAction(action);
        actionUIManager.newMove(action.getTargetPosition()); // Store validated position
        actionUIManager.UpdateActionUI(action.getSprite());
        if (action.getCooldown() > 0)
        {
            cd.addCooldown(action);
        }
    }

    public void startActionPhase()
    {
        foreach (EntityScript entity in gameEntities)
        {
            if (entity is GoblinScript)
            {
                GoblinScript goblin = (GoblinScript)entity;
                goblin.PlanTurn();
            }
        }

        state = "action";
        StartCoroutine(ExecuteActionsWithDelay());
        cd.tickCooldowns();
        actionUIManager.clearActionUI();
    }

    IEnumerator ExecuteActionsWithDelay()
    {
        for (int i = 0; i < 3; i++)
        {
            executeActions();
            yield return new WaitForSeconds(actionRoundDelay);
            DisposeAttackProjectiles();
            foreach (EntityScript entity in gameEntities)
            {
                entity.isBlocking = false;
            }
            List<EntityScript> removeList = new List<EntityScript>();
            foreach (EntityScript entity in gameEntities)
            {
                if (entity.isDead)
                {
                    Destroy(entity.gameObject);
                    removeList.Add(entity);
                }
                if (entity is FatGoblinBoss boss)
                {
                    boss.idle();
                }
            }
            foreach (EntityScript entity in removeList)
            {
                gameEntities.Remove(entity);
                enemies.Remove(entity);
            }
            removeList.Clear();
            
        }
        if (player.isDead)
            {
                worldManager = WorldManager.Instance;
                worldManager.defeat();
                yield break;
            }
        if (enemies.Count == 0)
            {
                worldManager.victory();
                yield break;
            }
        state = "prep";
        actionUIManager.updateMove();
        NotifyRoundFinish();
    }

    public event Action roundFinished;
	public void NotifyRoundFinish()
	{		
			roundFinished?.Invoke();
	}

    

    void executeActions()
    {
        currentActions = new Dictionary<EntityScript, AAction>();
        CreateSnapshot();

        foreach (EntityScript entity in gameEntities)
        {
            AAction action = entity.DequeueAction();
            if (action == null) continue;
            action.target = entity;
            currentActions[entity] = action;
        }
        ResolveBlocks(currentActions);
        ResolveAttacks(currentActions);
        ResolveMove(currentActions);
    }

    public void AddToEnemyList(EntityScript obj)
    {
        enemies.Add(obj);
    }

    public List<EntityScript> GetEnemyList()
    {
        return enemies;
    }

    public void AddToEntityList(EntityScript obj)
    {
        gameEntities.Add(obj);
    }

    public List<EntityScript> GetEntityList()
    {
        print(gameEntities);
        return gameEntities;
    }


    private void ResolveAttacks(Dictionary<EntityScript, AAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is MeleeAttack || a.Value is GoblinAttackAction || a.Value is GoblinRangedAttack || a.Value is FatGoblinSummonAction || a.Value is FireballAction || a.Value is MagicMissile)) continue;
            a.Value.execute();
        }
        return;
    }

    private void ResolveBlocks(Dictionary<EntityScript, AAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is BlockAction)) continue;

            a.Value.execute();
        }
        return;
    }

    private void ResolveMove(Dictionary<EntityScript, AAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is MoveAction)) continue;

            a.Value.execute();
        }
    }

    private void DisposeAttackProjectiles()
    {
        foreach (KeyValuePair<EntityScript, AAction> unit in currentActions)
        {
            AAction action = unit.Value;
            if (action is Disposable act)
            {
                act.Dispose();
            }
        }
    }

    public void return_to_overworld()
    {
        worldManager = WorldManager.Instance;
        worldManager.defeat();
    }
}
