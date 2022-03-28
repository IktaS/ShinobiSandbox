using UnityEngine;
public abstract class DFANode
{
    protected Gesture _gesture;
    public abstract void enterNode(ComboActionCaller caller, DFANode prevNode);
    public abstract DFANode exitNode(ComboActionCaller caller, Gesture nextGesture);
}

public interface IAimable
{
    public void setAim(Vector3 start, Vector3 dir);
}