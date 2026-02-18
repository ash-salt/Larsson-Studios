using Assets.Scripts.player_actions;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string state;
    private List<EntityScript> gameEntities = new List<EntityScript>();
    private List<EntityScript> enemies = new List<EntityScript>();

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
        if (state == "prep")
        {
            
        }
        else if (state == "action")
        {
            for (int i = 0; i < 3; i++)
            {
               executeActions();
            }
        }
        state = "prep";
    }

    public GameObject GetSlashPrefab()
    {
        return slashPrefab;
    }

    public void startActionPhase()
    {
        state = "action";
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

        foreach (EntityScript entity in gameEntities)
        {
            if (entity is PlayerScript)
            {
                IAction action = entity.DequeueAction();
                print(action);
                action.execute(entity);
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
}
