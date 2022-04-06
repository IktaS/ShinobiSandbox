using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IProjectileHittable
{
    public virtual void HitByProjectile(Projectile p, Vector3 power)
    {
        Debug.Log("hit by " + p.name);
    }
}
