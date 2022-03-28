using UnityEngine;
public class IdleNode : DFANode
{
    private ShieldNode _shieldNode;
    private EarthPrisonNode _earthPrisonNode;
    private AimNode _aimNode;
    private RecoverNode _recoverNode;

    public IdleNode(
        ShieldNode shieldNode,
        EarthPrisonNode earthPrisonNode,
        AimNode aimNode,
        RecoverNode recoverNode
    )
    {
        _shieldNode = shieldNode;
        _earthPrisonNode = earthPrisonNode;
        _aimNode = aimNode;
        _recoverNode = recoverNode;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
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
        return null;
    }
}