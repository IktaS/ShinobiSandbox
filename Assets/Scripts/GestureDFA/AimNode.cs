using UnityEngine;
public class AimNode : DFANode
{
    private ComboAction _aimAction;
    private EarthPrisonNode _earthPrisonNode;
    private FireNode _fireNode;
    private LightningNode _lightningNode;
    private GustNode _gustNode;

    private ComboActionQueue _queue;

    private Transform _startPoint;
    private Transform _dirPoint;
    public AimNode(
        Transform startPoint,
        Transform dirPoint,
        ComboAction aimAction,
        ComboActionQueue queue,
        EarthPrisonNode earthPrisonNode,
        FireNode fireNode,
        LightningNode lightningNode,
        GustNode gustNode
    )
    {
        _aimAction = aimAction;
        _queue = queue;
        _startPoint = startPoint;
        _dirPoint = dirPoint;
        _earthPrisonNode = earthPrisonNode;
        _fireNode = fireNode;
        _lightningNode = lightningNode;
        _gustNode = gustNode;
    }

    private Vector3 getDir()
    {
        Vector3 dir = (_dirPoint.position - _startPoint.position).normalized;
        return dir;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _aimAction));
    }

    public override DFANode exitNode(ComboActionCaller caller, Gesture nextGesture)
    {
        Vector3 start = _startPoint.position;
        Vector3 dir = getDir();
        // check gesture
        switch (nextGesture.type)
        {
            case GestureType.EarthPrison:
                _earthPrisonNode.setAim(start, dir);
                return _earthPrisonNode;
            case GestureType.Fire:
                _fireNode.setAim(start, dir);
                return _fireNode;
            case GestureType.Lightning:
                _lightningNode.setAim(start, dir);
                return _lightningNode;
            case GestureType.Gust:
                _gustNode.setAim(start, dir);
                return _gustNode;
        }
        return null;
    }
}