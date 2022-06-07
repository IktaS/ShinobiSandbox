using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AimEnum
{
    Palm,
    FingerEnd,
}

public class AimRef : MonoBehaviour
{
    [SerializeField] protected Transform start;
    [SerializeField] protected Transform end;

    public (Transform, Transform) GetAim()
    {
        return (start, end);
    }
}
