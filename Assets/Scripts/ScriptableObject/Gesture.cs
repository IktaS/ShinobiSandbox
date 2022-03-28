using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gesture", menuName = "ScriptableObjects/Gesture", order = 1)]
public class Gesture : ScriptableObject
{
    public GestureType type;
}

public enum GestureType
{
    Idle,
    Aim,
    EarthPrison,
    Fire,
    Gust,
    Lightning,
    Recover,
    Shield
}