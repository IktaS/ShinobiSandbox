using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class FireProjectile : Projectile
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float offset;

    [SerializeField] private LayerMask layerMask;
    public override void Shoot(Vector3 start, Vector3 dir, float speed, float distance)
    {
        Vector3 direction = (dir - start).normalized;
        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    public override string PoolName()
    {
        return "Fireball";
    }

    void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
        }
    }
}
