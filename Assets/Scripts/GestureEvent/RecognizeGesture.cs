using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class RecognizeGesture : MonoBehaviour
{
    protected UnityEvent<Gesture> onRecognizeGesture = new UnityEvent<Gesture>();
    public void addOnRecognizeGestureListener(UnityAction<Gesture> action)
    {
        onRecognizeGesture.AddListener(action);
    }
}