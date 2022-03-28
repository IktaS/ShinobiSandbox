using System;
using UnityEngine;
public class GustNode : DFANode, IAimable
{
    private ComboAction _gustAction;
    private ComboActionQueue _queue;

    private Vector3 _start;
    private Vector3 _dir;
    public GustNode(ComboAction gustAction, ComboActionQueue queue)
    {
        _gustAction = gustAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        if (prevNode is AimNode)
        {
            _gustAction.start = _start;
            _gustAction.dir = _dir;
            _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _gustAction));
        }
    }

    public void setAim(Vector3 start, Vector3 dir)
    {
        _start = start;
        _dir = dir;
    }

    public override DFANode exitNode(ComboActionCaller caller, Gesture nextGesture)
    {
        return null;
    }
}