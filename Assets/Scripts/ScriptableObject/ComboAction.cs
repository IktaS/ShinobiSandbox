using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboAction", menuName = "ScriptableObjects/ComboAction", order = 1)]
public class ComboAction : ScriptableObject
{
    public Vector3 start;
    public Vector3 dir;
}

public enum ComboActionCaller
{
    Unknown,
    LeftHand,
    RightHand
}
