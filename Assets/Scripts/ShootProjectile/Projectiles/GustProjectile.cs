using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class GustProjectile : Projectile
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float offset;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float power;
    public override void Shoot(Vector3 start, Vector3 dir, float speed, float distance)
    {
        Vector3 direction = (dir - start).normalized;
        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    public override string PoolName()
    {
        return "Gust";
    }

    void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            if (otherRB != null)
            {
                otherRB.AddForce(rb.velocity.normalized * power, ForceMode.Impulse);
            }
        }
    }
}
