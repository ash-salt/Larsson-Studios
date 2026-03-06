using UnityEngine;

public class DamageAllGoblins : MonoBehaviour
{
    [SerializeField] private int damageAmount = 50;

    private void OnMouseDown()
    {
        DamageAllEntities();
    }

    private void DamageAllEntities()
    {
        EntityScript[] entities = FindObjectsByType<EntityScript>(FindObjectsSortMode.None);
        
        Debug.Log($"Found {entities.Length} entities total");

        int entitiesHit = 0;
        foreach (EntityScript entity in entities)
        {
            // Skip the player
            if (entity.CompareTag("Player"))
            {
                continue;
            }
            
            Debug.Log($"Found entity: {entity.gameObject.name}, isDead: {entity.isDead}");
            
            if (entity != null && !entity.isDead)
            {
                Debug.Log($"Found: {entity.gameObject.name}");
                Debug.Log($"Type: {entity.GetType()}");
                Debug.Log($"Health BEFORE: {entity.currentHealth}");

                entity.damage(damageAmount);

                Debug.Log($"Health AFTER: {entity.currentHealth}");
                entitiesHit++;
            }
        }

        Debug.Log($"Damaged {entitiesHit} entities for {damageAmount} damage each!");
    }
}