using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NaughtyAttributes;

public class CSVWriter : MonoBehaviour
{
    private StreamWriter writer;
    private List<GestureInputData> datas = new List<GestureInputData>();
    public void Start()
    {
    }

    [Button]
    public void StartWrite()
    {
        string fname = System.DateTime.Now.ToString("yyyy:MM:dd-HH:mm:ssK") + ".csv";
        string path = Path.Combine(Application.persistentDataPath, fname);
        writer = new StreamWriter(path);
        datas = new List<GestureInputData>();
    }

    public void WriteGestureData(GestureInputData data)
    {
        if (datas == null)
        {
            datas = new List<GestureInputData>();
        }
        data.Time = System.DateTime.Now.ToString("HH:mm:ss.ffffff");
        datas.Add(data);
    }

    [Button]
    public void FinishWrite()
    {
        if (writer == null)
        {
            string fname = System.DateTime.Now.ToString("yyyy:MM:dd-HH:mm:ssK") + ".csv";
            string path = Path.Combine(Application.persistentDataPath, fname);
            writer = new StreamWriter(path);
        }
        writer.WriteLine("CallerHand,GestureInput,ActionExecuted,Time");
        foreach (var data in datas)
        {
            writer.WriteLine(data.ToString());
        }
        writer.Close();
        writer = null;
        datas = new List<GestureInputData>();
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
        return $"{CallerHand},{GestureInput},{ActionExecuted},{Time}";
    }

    public GestureInputData(string caller, string gesture, string action)
    {
        this.CallerHand = caller;
        this.GestureInput = gesture;
        this.ActionExecuted = action;
        this.Time = "";
    }
}
