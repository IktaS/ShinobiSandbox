using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Projectile currentProjectile;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform dirPoint;
    [SerializeField] private float maxDistance = 100f;

    public void setProjectile(Projectile projectile)
    {
        currentProjectile = projectile;
    }

    public void setMaxDistance(float distance)
    {
        maxDistance = distance;
    }

    public void Shoot(Projectile projectile, float projectileSpeed)
    {
        if (projectile != null)
        {
            currentProjectile = projectile;
        }
        Vector3 dir = dirPoint.position;
        Vector3 start = startPoint.position;
        Vector3 direction = (dir - start).normalized;
        GameObject go = EasyObjectPool.instance.GetObjectFromPool(currentProjectile.PoolName(), dir, Quaternion.LookRotation(direction, Vector3.up));
        currentProjectile = go.GetComponent<Projectile>();
        currentProjectile.Shoot(start, dir, projectileSpeed, maxDistance);
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject currentProjectile;
    public Transform firePoint;
    public float projectileSpeed = 5f;
    private Vector3 destination;
    [SerializeField] private float maxDistance = 100f;

    public void setProjectile(GameObject projectile)
    {
        currentProjectile = projectile;
    }
    public void setFirePoint(Vector3 pos)
    {
        firePoint.localPosition = pos;
    }

    public void Shoot()
    {
        if (this.GetComponentInChildren<Projectile>() == null)
        {
            var projectileObj = Instantiate(currentProjectile, firePoint.position, Quaternion.identity) as GameObject;
            Ray ray = new Ray(firePoint.position, firePoint.transform.right);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                destination = hit.point;
            }
            else
            {
                destination = ray.GetPoint(maxDistance);
            }

            if (currentProjectile.GetComponent<Projectile>().magicType == Projectile.Magic.lightning)
            {
                projectileObj.transform.GetChild(0).transform.position = firePoint.position;
                projectileObj.transform.GetChild(1).transform.position = destination;
                if(hit.collider != null && hit.collider.gameObject.tag == "TrainingTarget"){
                    TrainingTarget target = hit.collider.GetComponent<TrainingTarget>();
                    target.kill();
                }else if(hit.collider != null && hit.collider.TryGetComponent<MonsterBehaviour>(out MonsterBehaviour mb)) {
                    mb.TakeDamage(10);
				}
            }
            else if (currentProjectile.GetComponent<Projectile>().magicType == Projectile.Magic.wind)
            {
                projectileObj.transform.parent = firePoint.parent;
                projectileObj.transform.rotation = Quaternion.LookRotation((destination - firePoint.position).normalized, Vector3.up);
            }
            else
            {
                projectileObj.transform.rotation = Quaternion.LookRotation((destination - firePoint.position).normalized, Vector3.up);
                projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
            }
        }
    }
}
*/