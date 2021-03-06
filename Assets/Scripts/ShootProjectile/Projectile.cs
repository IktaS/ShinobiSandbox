using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public abstract class Projectile : MonoBehaviour
{
    public ProjectileParameters parameters;
    public GameObject impactParticle; // Effect spawned when projectile hits a collider
    public GameObject projectileParticle; // Effect attached to the gameobject as child
    public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
    [Header("Adjust if not using Sphere Collider")]
    public float colliderRadius = 1f;
    [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
    public float collideOffset = 0.15f;
    public abstract string PoolName();
    public abstract void Shoot(Vector3 start, Vector3 dir, float speed, float distance);
    [SerializeField] private float damage;

    private Vector3 spawnLoc;

    public float GetDamage()
    {
        return damage;
    }

    protected void Start()
    {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation, transform) as GameObject;
        projectileParticle.transform.localScale = transform.localScale;
        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            muzzleParticle.transform.localScale = transform.localScale;
            Destroy(muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
        }
        spawnLoc = transform.position;
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity); // Sets rotation to look at direction of movement
        }
        if (Vector3.Distance(spawnLoc, transform.position) >= parameters.maxDistance)
        {
            ReturnGameObject(transform.position, Vector3.zero);
        }
    }

    protected void ReturnGameObject(Vector3 position, Vector3 normal)
    {
        transform.position = position + (normal * collideOffset); // Move projectile to point of collision

        GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, normal)) as GameObject; // Spawns impact effect
        impactP.transform.localScale = transform.localScale;

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                             //Component at [0] is that of the parent i.e. this object (if there is any)
        for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
        {
            ParticleSystem trail = trails[i];

            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null); // Detaches the trail from the projectile
                Destroy(trail.gameObject, 2f); // Removes the trail after seconds
            }
        }

        Destroy(projectileParticle, 3f); // Removes particle effect after delay
        Destroy(impactP, 3.5f); // Removes impact effect after delay
        Destroy(gameObject); // Removes the projectile
    }
}

public interface IProjectileHittable
{
    public void HitByProjectile(Projectile p, Vector3 power);
}
