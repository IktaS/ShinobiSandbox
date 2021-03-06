using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboActionExecutor : MonoBehaviour
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
    [SerializeField] private ComboAction _idleAction;

    [Header("Helper UI")]
    [SerializeField] private Pointer _pointer;
    [SerializeField] private ShootProjectile _shootProjectile;
    [SerializeField] private CSVWriter _writer;

    [Header("Projectiles")]
    [SerializeField] private FireProjectile _fireProjectile;

    [SerializeField] private EarthPrisonProjectile _earthPrisonProjectile;

    [SerializeField] private LightningProjectile _lightningProjectile;

    [SerializeField] private GustProjectile _gustProjectile;

    [Header("Player")]
    [SerializeField] private RecoverHealth recoverTarget;
    [SerializeField] private GameObject shield;

    [Header("Queues")]
    [SerializeField] private ComboActionQueue _queue;

    [SerializeField] private EarthPrisonQueue _earthPrisonQueue;
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
        _writer.WriteGestureData(new GestureInputData(caller: ComboActionCallerUserString.GetString(message.Caller), "", action.name));
        if (action == _shieldAction)
        {
            shield.SetActive(true);
            return;
        }
        else if (action == _recoverAction)
        {
            recoverTarget.ActivateRecover();
            return;
        }
        else if (action == _activateEarthPrisonAction)
        {
            _earthPrisonQueue.activateAllEarthPrison();
        }
        else if (action == _aimAction)
        {
            _pointer.Activate();
            return;
        }
        else if (action == _setEarthPrisonAction)
        {
            _shootProjectile.Shoot(_earthPrisonProjectile);
        }
        else if (action == _shootFireAction)
        {
            _shootProjectile.Shoot(_fireProjectile);
        }
        else if (action == _shootLightningAction)
        {
            _shootProjectile.Shoot(_lightningProjectile);
        }
        else if (action == _gustAction)
        {
            _shootProjectile.Shoot(_gustProjectile);
        }
        else if (action == _idleAction)
        {
            Debug.Log("idle");
        }
        else
        {
            Debug.Log("Unknown action " + message.ToString());
        }
    }
}
