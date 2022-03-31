using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class EarthPrisonProjectile : Projectile
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float offset;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private EarthPrisonQueue queue;
    [SerializeField] private string earthPrisonGOPoolName = "EarthPrison";
    public override void Shoot(Vector3 start, Vector3 dir, float speed, float distance)
    {
        Vector3 direction = (dir - start).normalized;
        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    public override string PoolName()
    {
        return "EarthPrisonProj";
    }

    void OnCollisionEnter(Collision c)
    {
        if (layerMask == (layerMask | (1 << c.gameObject.layer)) && c.contacts.Length > 0)
        {
            var contactPoint = c.GetContact(0);
            var go = EasyObjectPool.instance.GetObjectFromPool(
                earthPrisonGOPoolName,
                contactPoint.point,
                Quaternion.identity
            );
            go.transform.up = contactPoint.normal;
            var ep = go.GetComponent<EarthPrison>();
            if (ep != null)
            {
                queue.addEarthPrison(ep);
            }
            ReturnGameObject(contactPoint.point, contactPoint.normal);
        }
    }
}
