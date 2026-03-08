using UnityEngine;

public abstract class ButtonInstruction
{
    public abstract void Instruct(GenericButton button);
    public abstract void Execute();
    public abstract void Update();
}