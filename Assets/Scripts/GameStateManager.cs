using Assets.Scripts;
using Assets.Scripts.player_actions;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Rendering;

public class GameStateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string state;
    private List<EntityScript> gameEntities = new List<EntityScript>();
    private List<EntityScript> enemies = new List<EntityScript>();

    public Dictionary<EntityScript, CharacterSnapshot> snapshot;

    private Dictionary<EntityScript, IAction> currentActions;

    [SerializeField] public GameObject slashPrefab;
    [SerializeField] public GameObject shieldPrefab;
    [SerializeField] public GameObject goblinSlashPrefab;
    [SerializeField] public GameObject moveAnimationPrefab;
    [SerializeField] public GameObject ghostSlashPrefab;
    [SerializeField] private float actionRoundDelay = 1f;
    [SerializeField] private ActionUIManager actionUIManager;

    public static GameStateManager Instance;
    void Awake()
    {
        state = "prep";
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

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

        state = "prep";
        actionUIManager.updateMove();
    }

    void executeActions()
    {
        currentActions = new Dictionary<EntityScript, IAction>();
        CreateSnapshot();

        foreach (EntityScript entity in gameEntities)
        {
            IAction action = entity.DequeueAction();
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


    private void ResolveAttacks(Dictionary<EntityScript, IAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is MeleeAttack || a.Value is GoblinAttackAction || a.Value is GoblinRangedAttack)) continue;

            a.Value.execute(a.Key);
        }
        return;
    }

    private void ResolveBlocks(Dictionary<EntityScript, IAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is BlockAction)) continue;

            a.Value.execute(a.Key);
        }
        return;
    }

    private void ResolveMove(Dictionary<EntityScript, IAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is MoveAction)) continue;

            a.Value.execute(a.Key);
        }
    }

    private void DisposeAttackProjectiles()
    {
        foreach (KeyValuePair<EntityScript, IAction> unit in currentActions)
        {
            IAction action = unit.Value;
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
        }
    }
}
