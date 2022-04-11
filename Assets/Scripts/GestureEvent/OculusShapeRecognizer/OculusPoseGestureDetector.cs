using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusPoseGestureDetector : RecognizeGesture
{
    public float idleDelay = 1000f;
    public Gesture idleGesture;
    public void FoundGesture(Gesture Gesture)
    {
        onRecognizeGesture.Invoke(Gesture);
        _elapsedTimeWithoutGesture = 0;
    }

    private float _elapsedTimeWithoutGesture;
    void Update()
    {
        if (_elapsedTimeWithoutGesture >= idleDelay)
        {
            FoundGesture(idleGesture);
        }
        _elapsedTimeWithoutGesture += Time.deltaTime;
    }
}
