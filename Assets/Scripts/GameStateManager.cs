using Assets.Scripts;
using Assets.Scripts.player_actions;
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
    [SerializeField] public GameObject goblinSlashPrefab;
    [SerializeField] private float actionRoundDelay = 1f;

    public static GameStateManager Instance;
    void Awake()
    {
        state = "prep";
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
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

    public GameObject GetGoblinSlashPrefab()
    {
        return goblinSlashPrefab;
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
    }

    System.Collections.IEnumerator ExecuteActionsWithDelay()
    {
        for (int i = 0; i < 3; i++)
        {
            executeActions();
            yield return new WaitForSeconds(actionRoundDelay);
        }
        state = "prep";
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

        foreach (EntityScript entity in gameEntities)
        {
            print("we are in the check dead loop");
            if (entity.isDead)
            {
                print("DIE!!!!");
                Destroy(entity.gameObject);
            }
        }
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
            if (!(a.Value is MeleeAttack || a.Value is GoblinAttackAction)) continue;

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
}
