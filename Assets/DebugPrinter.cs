using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrinter : MonoBehaviour
{
    public void PrintDebug(string str)
    {
        Debug.Log(str);
    }

    public void PrintWarn(string str)
    {
        Debug.LogWarning(str);
    }

    public void PrintError(string str)
    {
        Debug.LogError(str);
    }
}
