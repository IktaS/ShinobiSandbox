using System;
using UnityEngine;
public class FireNode : DFANode, IAimable
{
    private ComboAction _fireAction;
    private ComboActionQueue _queue;

    private Vector3 _start;
    private Vector3 _dir;
    public FireNode(ComboAction fireAction, ComboActionQueue queue)
    {
        _fireAction = fireAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        if (prevNode is AimNode)
        {
            _fireAction.start = _start;
            _fireAction.dir = _dir;
            _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _fireAction));
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