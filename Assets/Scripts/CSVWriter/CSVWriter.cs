using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NaughtyAttributes;

public class CSVWriter : MonoBehaviour
{
    private StreamWriter writer;
    public void Start()
    {
    }

    [Button]
    public void StartWrite()
    {
        if (writer == null)
        {
            string fname = System.DateTime.Now.ToString(Application.productName + "-yyyy:MM:dd-HH:mm:ssK") + ".csv";
            string path = Path.Combine(Application.persistentDataPath, fname);
            writer = new StreamWriter(path);
            writer.WriteAsync("CallerHand,GestureInput,ActionExecuted,Time\n");
        }
    }

    public void WriteGestureData(GestureInputData data)
    {
        if (writer != null)
        {
            data.Time = System.DateTime.Now.ToString("HH:mm:ss.ffffff");
            writer.WriteAsync(data.ToString());
        }
    }

    [Button]
    public void FinishWrite()
    {
        if (writer != null)
        {
            writer.Close();
        }
    }

}

public struct GestureInputData
{
    public string CallerHand;
    public string GestureInput;
    public string ActionExecuted;
    public string Time;

    public override string ToString()
    {
        return $"{CallerHand},{GestureInput},{ActionExecuted},{Time}\n";
    }

    public GestureInputData(string caller, string gesture, string action)
    {
        this.CallerHand = caller;
        this.GestureInput = gesture;
        this.ActionExecuted = action;
        this.Time = "";
    }
}
