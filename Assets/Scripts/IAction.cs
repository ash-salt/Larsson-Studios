using UnityEngine;

public interface IAction
{
    int getCost();

    void execute(EntityScript target);
}
