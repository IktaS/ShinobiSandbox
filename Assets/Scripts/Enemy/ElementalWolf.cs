using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalWolf : Wolf
{
    [SerializeField] protected Projectile weakProjectile;
    public override void HitByProjectile(Projectile p, Vector3 power)
    {
        if (p is GustProjectile)
        {
            StartCoroutine(ragdollByGust(power));
            return;
        }
        if (p.GetType() == weakProjectile.GetType())
        {
            TakeDamage(p.GetDamage() * 2);
            return;
        }
        TakeDamage(p.GetDamage() / 2);
    }
}
