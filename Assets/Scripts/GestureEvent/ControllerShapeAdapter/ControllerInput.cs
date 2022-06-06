using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : RecognizeGesture
{
    [SerializeField]
    private OVRInput.Controller controller;

    [SerializeField] private Gesture shieldGesture;
    [SerializeField] private Gesture aimGesture;
    [SerializeField] private Gesture earthPrisonGesture;
    [SerializeField] private Gesture fireGesture;
    [SerializeField] private Gesture gustGesture;
    [SerializeField] private Gesture lightingGesture;
    [SerializeField] private Gesture recoverGesture;
    [SerializeField] private Gesture idleGesture;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void FoundGesture(Gesture Gesture)
    {
        onRecognizeGesture.Invoke(Gesture);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            Debug.Log("Enter primary hand trigger");
            FoundGesture(shieldGesture);
            return;
        }
        if (OVRInput.Get(OVRInput.Button.One, controller))
        {
            Debug.Log("Enter primary hand trigger");
            FoundGesture(recoverGesture);
            return;
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            Debug.Log("Enter primary hand trigger");
            FoundGesture(aimGesture);
        }
        var thumbStickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
        var dir = GetDirection(Vector3.zero, new Vector3(thumbStickPos.x, thumbStickPos.y, 0));
        switch (dir)
        {
            case GeneralDirection.Up:
                Debug.Log("Enter prison");
                FoundGesture(earthPrisonGesture);
                break;
            case GeneralDirection.Down:
                Debug.Log("Enter air");
                FoundGesture(gustGesture);
                break;
            case GeneralDirection.Left:
                Debug.Log("Enter fire");
                FoundGesture(earthPrisonGesture);
                break;
            case GeneralDirection.Right:
                Debug.Log("Enter lightning");
                FoundGesture(lightingGesture);
                break;
            default:
                Debug.Log("unknown thumbstick");
                break;
        }
    }

    public enum GeneralDirection
    {
        None,
        Forwards,
        Back,
        Left,
        Right,
        Up,
        Down
    }

    public GeneralDirection GetDirection(Vector3 pointOfOrigin, Vector3 vectorToTest)
    {

        GeneralDirection result = GeneralDirection.None;
        float shortestDistance = Mathf.Infinity;
        float distance = 0;

        Vector3 vectorPosition = pointOfOrigin + vectorToTest;

        distance = Mathf.Abs(((pointOfOrigin + Vector3.forward) - vectorToTest).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = GeneralDirection.Forwards;
        }
        distance = Mathf.Abs(((pointOfOrigin - Vector3.forward) - vectorToTest).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = GeneralDirection.Back;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.up) - vectorToTest).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = GeneralDirection.Up;
        }
        distance = Mathf.Abs(((pointOfOrigin + -Vector3.up) - vectorToTest).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = GeneralDirection.Down;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.left) - vectorToTest).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = GeneralDirection.Left;
        }
        distance = Mathf.Abs(((pointOfOrigin + Vector3.right) - vectorToTest).magnitude);
        if (distance < shortestDistance)
        {
            shortestDistance = distance;
            result = GeneralDirection.Right;
        }
        return result;
    }
}
