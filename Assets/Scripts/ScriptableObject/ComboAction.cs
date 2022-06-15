using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

[CreateAssetMenu(fileName = "ComboAction", menuName = "ScriptableObjects/ComboAction", order = 1)]
public class ComboAction : ScriptableObject
{
    public string name;
    public Vector3 start;
    public Vector3 dir;
}

public enum ComboActionCaller
{
    Unknown,
    LeftHand,
    RightHand
}

public static class ComboActionCallerUserString
{
    public static string GetString(ComboActionCaller caller)
    {
        switch (caller)
        {
            case ComboActionCaller.Unknown:
                return "Unknown";
            case ComboActionCaller.LeftHand:
                return "LeftHand";
            case ComboActionCaller.RightHand:
                return "RightHand";
            default:
                return "Unknown";
        }
    }
}