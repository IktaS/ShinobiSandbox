using UnityEngine;
public class IdleNode : DFANode
{
    private ShieldNode _shieldNode;
    private EarthPrisonNode _earthPrisonNode;
    private AimNode _aimNode;
    private RecoverNode _recoverNode;
    private ComboAction _idleAction;

    private ComboActionQueue _queue;

    public IdleNode(
        ShieldNode shieldNode,
        EarthPrisonNode earthPrisonNode,
        AimNode aimNode,
        RecoverNode recoverNode,
        ComboAction idleAction,
        ComboActionQueue queue
    )
    {
        _shieldNode = shieldNode;
        _earthPrisonNode = earthPrisonNode;
        _aimNode = aimNode;
        _recoverNode = recoverNode;
        _idleAction = idleAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _idleAction));
        return;
    }

    public override DFANode exitNode(ComboActionCaller caller, Gesture nextGesture)
    {
        // check gesture
        switch (nextGesture.type)
        {
            case GestureType.Shield:
                return _shieldNode;
            case GestureType.EarthPrison:
                return _earthPrisonNode;
            case GestureType.Recover:
                return _recoverNode;
            case GestureType.Aim:
                return _aimNode;
        }
        return this;
    }
}