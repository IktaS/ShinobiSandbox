using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bomb : Enemy
{
    private NodeRepository nr;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float explodeTimer;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private float explosionDamage;

    public override void Spawn(Vector3 spawnPos)
    {
        base.Spawn(spawnPos);
        rb.MovePosition(spawnPos);
        SetChasePlayer();
        StartCoroutine(explodeAfterDelay(explodeTimer));
    }

    IEnumerator explodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    void SetChasePlayer()
    {
        if (nr == null)
        {
            var go = GameObject.FindObjectOfType<NodeRepository>();
            this.nr = go.GetComponent<NodeRepository>();
        }
        var target = nr.GetTrueTarget();
        if (target == null)
        {
            return;
        }
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
    public override void HitByProjectile(Projectile p, Vector3 power)
    {
        TakeDamage(p.GetDamage());
        if (p is GustProjectile)
        {
            Debug.Log("Hit by gust");
            StartCoroutine(ragdollByGust(power));
        }
    }

    IEnumerator ragdollByGust(Vector3 power)
    {
        agent.isStopped = true;
        agent.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(power, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        rb.isKinematic = true;
        agent.enabled = true;
        agent.isStopped = false;
        rb.useGravity = false;
        SetChasePlayer();
    }

    void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (var col in colliders)
        {
            var isHit = Physics.Raycast(transform.position, col.ClosestPoint(transform.position), explosionRadius);
            if (isHit)
            {
                var entity = col.gameObject.GetComponent<Entity>();
                if (entity != null)
                {
                    Debug.Log("find entity " + entity.name);
                    entity.TakeDamage(explosionDamage);
                }
            }
        }
        TakeDamage(health);
    }
}
