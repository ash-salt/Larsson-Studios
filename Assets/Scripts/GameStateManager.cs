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
    private PlayerScript player;

    public Dictionary<EntityScript, CharacterSnapshot> snapshot;

    private Dictionary<EntityScript, AAction> currentActions;

    [SerializeField] public GameObject slashPrefab;
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


    public void newAction(AAction action, Sprite sprite)
    {
        if (cd.onCooldown(action)) return;
        player.EnqueueAction(action);
        if (action.getCooldown() > 0)
        {
            cd.addCooldown(action);
        }
        actionUIManager.UpdateActionUI(sprite);
    }

    public void newMove(MoveAction action, Sprite sprite)
    {
        if (cd.onCooldown(action)) return;
        if (player.fullActionQueue()) return;
        player.QueueMove(action);
        actionUIManager.newMove(action.getTargetPosition()); // Store validated position
        actionUIManager.UpdateActionUI(sprite);
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
    }

    

    void executeActions()
    {
        currentActions = new Dictionary<EntityScript, AAction>();
        CreateSnapshot();

        foreach (EntityScript entity in gameEntities)
        {
            AAction action = entity.DequeueAction();
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
            if (!(a.Value is MeleeAttack || a.Value is GoblinAttackAction || a.Value is GoblinRangedAttack)) continue;

            a.Value.execute(a.Key);
        }
        return;
    }

    private void ResolveBlocks(Dictionary<EntityScript, AAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is BlockAction)) continue;

            a.Value.execute(a.Key);
        }
        return;
    }

    private void ResolveMove(Dictionary<EntityScript, AAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is MoveAction)) continue;

            a.Value.execute(a.Key);
        }
    }

    private void DisposeAttackProjectiles()
    {
        foreach (KeyValuePair<EntityScript, AAction> unit in currentActions)
        {
            AAction action = unit.Value;
            if (action is MeleeAttack)
            {
                MeleeAttack attack = (MeleeAttack) action;
                attack.Dispose();
            }
            if (action is GoblinAttackAction)
            {
                GoblinAttackAction attack = (GoblinAttackAction) action;
                attack.Dispose();
            }
            if (action is BlockAction)
            {
                BlockAction act = (BlockAction) action;
                act.Dispose();
            }
             if (action is GoblinRangedAttack)
            {
                GoblinRangedAttack attack = (GoblinRangedAttack) action;
                attack.Dispose();
            }
        }
    }
}
