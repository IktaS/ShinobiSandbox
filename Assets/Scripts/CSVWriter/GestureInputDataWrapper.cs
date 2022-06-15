using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureInputDataWrapper : MonoBehaviour
{
    [SerializeField] private CSVWriter writer;

    private GestureInputData data = new GestureInputData();

    public void SetGestureInputData(string CallerHand, string GestureInput, string ActionExecuted)
    {
        data.CallerHand = CallerHand;
        data.GestureInput = GestureInput;
        data.ActionExecuted = ActionExecuted;
    }

    public void ClearGestureData()
    {
        data = new GestureInputData();
    }

    public void SetCallerHand(string CallerHand)
    {
        data.CallerHand = CallerHand;
    }

    public void SetGestureInput(string GestureInput)
    {
        data.GestureInput = GestureInput;
    }

    public void SetActionExecuted(string ActionExecuted)
    {
        data.ActionExecuted = ActionExecuted;
    }

    public void WriteToWriter()
    {
        writer.WriteGestureData(data);
        data = new GestureInputData();
    }
}
