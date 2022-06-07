using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Projectile currentProjectile;
    [SerializeField] private AimRef aim;
    [SerializeField] private float maxDistance = 100f;

    public void setProjectile(Projectile projectile)
    {
        currentProjectile = projectile;
    }

    public void setMaxDistance(float distance)
    {
        maxDistance = distance;
    }

    private float speed = 5f;

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void Shoot(Projectile projectile, float projectileSpeed)
    {
        var (startPoint, dirPoint) = aim.GetAim();
        if (projectile != null)
        {
            currentProjectile = projectile;
        }
        Vector3 dir = dirPoint.position;
        Vector3 start = startPoint.position;
        Vector3 direction = (dir - start).normalized;
        GameObject go = Instantiate(currentProjectile.gameObject, dir, Quaternion.LookRotation(direction, Vector3.up));
        var spawnedProj = go.GetComponent<Projectile>();
        spawnedProj.Shoot(start, dir, projectileSpeed, maxDistance);
    }

    public void ShootDEBUG(Projectile projectile)
    {
        Shoot(projectile, speed);
    }
}