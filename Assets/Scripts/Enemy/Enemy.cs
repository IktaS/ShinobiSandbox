using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IProjectileHittable
{
    public Spawner spawner;
    [SerializeField] private GameObject deathEffect;

    void Start()
    {
        OnDeath.AddListener(() => Die());
    }

    public virtual void HitByProjectile(Projectile p, Vector3 power)
    {
        Debug.Log("hit by " + p.name);
    }

    protected virtual void Die()
    {
        GameObject impactP = Instantiate(deathEffect, transform.position, Quaternion.FromToRotation(Vector3.up, transform.up)) as GameObject; // Spawns impact effect
        Destroy(impactP, 3.5f); // Removes impact effect after delay
        if (spawner != null)
        {
            spawner.Dead(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}