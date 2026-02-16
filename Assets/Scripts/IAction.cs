using UnityEngine;

public interface IAction
{
    int getCost();

    void execute(GameObject target);
}
