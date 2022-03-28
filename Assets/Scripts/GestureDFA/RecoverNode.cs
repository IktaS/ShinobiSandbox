
public class RecoverNode : DFANode
{
    private ComboAction _recoverAction;
    private ComboActionQueue _queue;
    public RecoverNode(ComboAction recoverAction, ComboActionQueue queue)
    {
        _recoverAction = recoverAction;
        _queue = queue;
    }

    public override void enterNode(ComboActionCaller caller, DFANode prevNode)
    {
        _queue.queue.Enqueue(new ComboActionQueueMessage(caller, _recoverAction));
    }

    public override DFANode exitNode(ComboActionCaller caller, Gesture nextGesture)
    {
        return null;
    }
}