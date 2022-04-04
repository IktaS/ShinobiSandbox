using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    public void Spawn() { }

    public virtual void handleProjectileHit(Projectile p, Vector3 power)
    {
        Debug.Log("hit by " + p.name);
    }
}
