using UnityEngine;

public class GoblinRangedAttack : IAction 
{
    private bool prepared = false;
    public int getCost()
    {
        return 1;
    }

    public int getCooldown()
    {
        return 0;
    }

    public void execute(EntityScript entity)
    {
        if (prepared == false)
        {
            prepared = true;
        }
        else
        {
            
        }
    }
}
