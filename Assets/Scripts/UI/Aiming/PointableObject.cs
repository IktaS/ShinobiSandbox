using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class PointableObject : MonoBehaviour, IPointable
{
    public UnityEvent<Pointer> onPointed;
    public UnityEvent<Pointer> onStay;
    public UnityEvent<Pointer> onUnpointed;

    public void WhenPointed(Pointer p)
    {
        onPointed.Invoke(p);
    }
    public void WhenStay(Pointer p)
    {
        onStay.Invoke(p);
    }
    public void WhenUnpointed(Pointer p)
    {
        onUnpointed.Invoke(p);
    }
}
