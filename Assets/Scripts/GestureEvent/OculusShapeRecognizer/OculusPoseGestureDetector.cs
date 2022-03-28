using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusPoseGestureDetector : RecognizeGesture
{
    public void FoundGesture(Gesture Gesture)
    {
        onRecognizeGesture.Invoke(Gesture);
    }
}
