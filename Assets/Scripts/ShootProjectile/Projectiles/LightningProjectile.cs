using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class LightningProjectile : Projectile
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
        return "Lightning";
    }

    void OnDisable()
    {
        rb.AddForce(Vector3.zero, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision c)
    {
        var contactPoint = c.GetContact(0);
        if (layerMask == (layerMask | (1 << c.gameObject.layer)) && c.contacts.Length > 0)
        {
            var enemy = contactPoint.otherCollider.gameObject.GetComponent<IProjectileHittable>();
            if (enemy != null)
            {
                enemy.HitByProjectile(this, rb.velocity.normalized);
            }
        }
        ReturnGameObject(contactPoint.point, contactPoint.normal);
    }
}
