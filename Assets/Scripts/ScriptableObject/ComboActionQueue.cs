using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboActionQueue", menuName = "ScriptableObjects/Queue/ComboActionQueue", order = 2)]
public class ComboActionQueue : ScriptableObject
{
    public Queue<ComboActionQueueMessage> queue = new Queue<ComboActionQueueMessage>();
}

public struct ComboActionQueueMessage
{
    public ComboActionQueueMessage(ComboActionCaller caller, ComboAction action)
    {
        Caller = caller;
        Action = action;
    }
    public ComboActionCaller Caller { get; }
    public ComboAction Action { get; }

    public override string ToString() => $"({Caller}, {Action})";
}