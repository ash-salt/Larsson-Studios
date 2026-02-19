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

    public void startActionPhase()
    {
        state = "action";
        for (int i = 0; i < 3; i++)
        {
            executeActions();

        }
        state = "prep";
    }

    public void endActionPhase()
    {
        state = "prep";
        foreach (EntityScript entity in gameEntities)
        {
        }
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
        //ResolveMove(currentActions);

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


    private void ResolveAttacks(Dictionary<EntityScript, IAction> queuedActions)
    {
        foreach (var a in queuedActions)
        {
            if (!(a.Value is MeleeAttack)) continue;

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
        return;
        /*foreach (var a in queuedActions)
        {
            if (!(a.Value is MoveAction)) continue;

            a.Value.execute(a.Key);
        }
        */
    }
}
