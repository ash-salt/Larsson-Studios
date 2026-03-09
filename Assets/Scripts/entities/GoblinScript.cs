using Assets.Scripts.player_actions;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Assets.Scripts
{
	public class GoblinScript : EntityScript
{
    [SerializeField] public MoveActionData moveActionData;
    [SerializeField] public ActionData attackActionData;
    [SerializeField] public GameObject player;
    [SerializeField] FloatingHealthBar healthBar;
    public float farAway = 5f;
    public float mediumDistance = 4f;
    public float shortDistance = 2f;
    public float attackDistance = 0.5f;
    [SerializeField] private float randomRadius = 1f;
    private DamageFlash damageFlash;

    void Start()
    {
        GameStateManager.Instance.AddToEnemyList(this);
        GameStateManager.Instance.AddToEntityList(this);
        player = GameStateManager.Instance.player.gameObject;
    }

    private new void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        damageFlash = GetComponent<DamageFlash>();
    }

    public override void damage(int damage)
    {
        if (isBlocking) return;

        currentHealth -= damage;
        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        damageFlash.CallFlashDamage();

        if (currentHealth <= 0)
            Die();
    }

    public virtual void PlanTurn()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 targetPosition = player.transform.position - direction * 0.3f;
        Vector3 randomOffset = (Vector3)(Random.insideUnitCircle * randomRadius);
        Vector3 finalTarget = targetPosition + randomOffset;

        if (distanceToPlayer < attackDistance)
        {
            QueueAttack();
        }
        else if (distanceToPlayer < shortDistance)
        {
            QueueMove(finalTarget);
            QueueAttack();
        }
        else if (distanceToPlayer < mediumDistance)
        {
            QueueMove(finalTarget);
            QueueMove(finalTarget);
            QueueAttack();
        }
        else
        {
            QueueMove(finalTarget);
            QueueMove(finalTarget);
            QueueMove(finalTarget);
        }
    }

    public void QueueMove(Vector2 targetPos, float maxDistance = 2f)
    {
        MoveAction action = (MoveAction)moveActionData.createAction();
        action.Initialize(targetPos, maxDistance, transform.position);
        EnqueueAction(action);
    }

    private void QueueAttack()
    {
        EnqueueAction(attackActionData.createAction());
    }
}
}