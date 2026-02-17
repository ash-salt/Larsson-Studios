using Assets.Scripts;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int damage;

    public int getCost()
    {
        return 1;
    }

    public void execute(GameObject target)
    {
        if (target.TryGetComponent<GoblinScript>(out GoblinScript goblin))
        {
            goblin.damage(50);
        }
    }
}
