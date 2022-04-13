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

    [Header("Helper UI")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ShootProjectile _shootProjectile;

    [Header("Projectiles")]
    [SerializeField] private FireProjectile _fireProjectile;
    [SerializeField] private float fireProjectileSpeed = 5f;

    [SerializeField] private EarthPrisonProjectile _earthPrisonProjectile;
    [SerializeField] private float earthPrisonProjectileSpeed = 5f;

    [SerializeField] private LightningProjectile _lightningProjectile;
    [SerializeField] private float lightningProjectileSpeed = 20f;

    [SerializeField] private GustProjectile _gustProjectile;
    [SerializeField] private float gustProjectileSpeed = 5f;

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

        if (action == _shieldAction)
        {
            Debug.Log("enter shield");
            shield.SetActive(true);
            return;
        }
        else if (action == _recoverAction)
        {
            Debug.Log("enter recover");
            recoverTarget.ActivateRecover();
            return;
        }
        else if (action == _activateEarthPrisonAction)
        {
            _earthPrisonQueue.activateAllEarthPrison();
        }
        else if (action == _aimAction)
        {
            _lineRenderer.enabled = true;
            return;
        }
        else if (action == _setEarthPrisonAction)
        {
            _shootProjectile.Shoot(_earthPrisonProjectile, earthPrisonProjectileSpeed);
        }
        else if (action == _shootFireAction)
        {
            _shootProjectile.Shoot(_fireProjectile, fireProjectileSpeed);
        }
        else if (action == _shootLightningAction)
        {
            _shootProjectile.Shoot(_lightningProjectile, lightningProjectileSpeed);
        }
        else if (action == _gustAction)
        {
            _shootProjectile.Shoot(_gustProjectile, gustProjectileSpeed);
        }
        else
        {
            Debug.Log("Unknown action");
        }
        _lineRenderer.enabled = false;
        shield.SetActive(false);
        recoverTarget.DeactivateRecover();
    }
}
