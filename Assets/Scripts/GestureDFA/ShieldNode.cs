
public class ShieldNode : DFANode
{
    private ComboAction _shieldAction;
    private ComboActionQueue _queue;
    public ShieldNode(ComboAction shieldAction, ComboActionQueue queue)
    {
        _shieldAction = shieldAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _shieldAction));
    }

    public override DFANode exitNode(ComboActionCaller caller, Gesture nextGesture)
    {
        return null;
    }
}