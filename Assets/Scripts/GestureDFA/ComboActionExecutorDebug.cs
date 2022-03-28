using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboActionExecutorDebug : MonoBehaviour
{
    [Header("Combo Action")]
    [SerializeField] private ComboAction _shieldAction;
    [SerializeField] private ComboAction _activateEarthPrisonAction;
    [SerializeField] private ComboAction _aimAction;
    [SerializeField] private ComboAction _setEarthPrisonAction;
    [SerializeField] private ComboAction _shootFireAction;
    [SerializeField] private ComboAction _shootLightningAction;
    [SerializeField] private ComboAction _recoverAction;
    [SerializeField] private ComboAction _gustAction;

    [SerializeField] private ComboActionQueue _queue;
    // Update is called once per frame
    void Update()
    {
        if (_queue.queue.Count > 0)
        {
            ExecuteAction(_queue.queue.Dequeue());
        }
    }

    void ExecuteAction(ComboActionQueueMessage message)
    {
        ComboAction action = message.Action;

        if (action == _shieldAction)
        {
            Debug.Log("Shield up");
        }
        else if (action == _recoverAction)
        {
            Debug.Log("Recovering");
        }
        else if (action == _activateEarthPrisonAction)
        {
            Debug.Log("Activating Earth Prison");
        }
        else if (action == _aimAction)
        {
            Debug.Log("Aiming...");
        }
        else if (action == _setEarthPrisonAction)
        {
            Debug.Log("Setting Earth Prison in: ");
            Debug.Log(action.start);
            Debug.Log(action.dir);
            Debug.DrawRay(action.start, action.dir * 1000, Color.yellow, 10f);
        }
        else if (action == _shootFireAction)
        {
            Debug.Log("Shooting fire to: ");
            Debug.Log(action.start);
            Debug.Log(action.dir);
            Debug.DrawRay(action.start, action.dir * 1000, Color.red, 10f);
        }
        else if (action == _shootLightningAction)
        {
            Debug.Log("Shooting lightning to: ");
            Debug.Log(action.start);
            Debug.Log(action.dir);
            Debug.DrawRay(action.start, action.dir * 1000, Color.magenta, 10f);
        }
        else if (action == _gustAction)
        {
            Debug.Log("Shooting gust to: ");
            Debug.Log(action.start);
            Debug.Log(action.dir);
            Debug.DrawRay(action.start, action.dir * 1000, Color.cyan, 10f);
        }
        else
        {
            Debug.Log("Unknown action");
        }
    }
}
