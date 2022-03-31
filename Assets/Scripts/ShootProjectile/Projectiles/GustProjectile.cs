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

    void OnCollisionEnter(Collision c)
    {
        if (layerMask == (layerMask | (1 << c.gameObject.layer)) && c.contacts.Length > 0)
        {
            var contactPoint = c.GetContact(0);
            Rigidbody otherRB = contactPoint.thisCollider.gameObject.GetComponent<Rigidbody>();
            if (otherRB != null)
            {
                otherRB.AddForce(rb.velocity.normalized * power, ForceMode.Impulse);
            }
            ReturnGameObject(contactPoint.point, contactPoint.normal);
        }
    }
}
