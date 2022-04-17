using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

[RequireComponent(typeof(Canvas))]
public class CanvasPlayerFacer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas canvas;
    [SerializeField, Interface(typeof(ICanvasTarget))] private MonoBehaviour _targetSource;
    private ICanvasTarget targetSource;

    private Transform target;
    void Start()
    {
        if (canvas != null)
        {
            canvas = GetComponent<Canvas>();
        }
        targetSource = _targetSource as ICanvasTarget;
    }

    // Update is called once per frame
    void Update()
    {
        target = targetSource.GetTarget();
        if (target != null)
        {
            canvas.transform.LookAt(target.position, Vector3.up);
        }
    }
}

public interface ICanvasTarget
{
    public Transform GetTarget();
}
