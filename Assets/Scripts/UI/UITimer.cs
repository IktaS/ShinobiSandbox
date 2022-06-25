using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;
    private bool playing;
    private float Timer;

    public void ResetAndStart()
    {
        ResetTimer();
        StartTimer();
    }

    void Update()
    {

        if (playing == true)
        {

            Timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60F);
            int seconds = Mathf.FloorToInt(Timer % 60F);
            int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }

    }

    public void Pause()
    {
        playing = false;
    }

    public void ResetTimer()
    {
        Timer = 0;
    }

    public void StopAndReset()
    {
        Pause();
        ResetTimer();
    }

    [Button]
    public void StartTimer()
    {
        playing = true;
    }

    public float GetTimer()
    {
        return Timer;
    }
}
