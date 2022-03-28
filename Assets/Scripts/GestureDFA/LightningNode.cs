using System;
using UnityEngine;
public class LightningNode : DFANode, IAimable
{
    private ComboAction _lightningAction;
    private ComboActionQueue _queue;

    private Vector3 _start;
    private Vector3 _dir;
    public LightningNode(ComboAction lightningAction, ComboActionQueue queue)
    {
        _lightningAction = lightningAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        if (prevNode is AimNode)
        {
            _lightningAction.start = _start;
            _lightningAction.dir = _dir;
            _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _lightningAction));
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