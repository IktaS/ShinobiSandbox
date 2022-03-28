using System;
using UnityEngine;
public class EarthPrisonNode : DFANode, IAimable
{
    private ComboAction _activatePrisonAction;
    private ComboAction _setPrisonAction;
    private ComboActionQueue _queue;

    private Vector3 _start;
    private Vector3 _dir;
    public EarthPrisonNode(ComboAction activatePrisonAction, ComboAction setPrisonAction, ComboActionQueue queue)
    {
        _activatePrisonAction = activatePrisonAction;
        _setPrisonAction = setPrisonAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        if (prevNode is AimNode)
        {
            _setPrisonAction.start = _start;
            _setPrisonAction.dir = _dir;
            _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _setPrisonAction));
            return;
        }
        _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _activatePrisonAction));
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