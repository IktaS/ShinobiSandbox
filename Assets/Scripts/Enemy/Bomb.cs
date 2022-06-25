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
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float explodeTimer;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private float explosionDamage;

    public override void Spawn(Vector3 spawnPos)
    {
        base.Spawn(spawnPos);
        rb.MovePosition(spawnPos);
        rb.isKinematic = true;
        agent.enabled = true;
        agent.isStopped = false;
        rb.useGravity = false;
        SetChasePlayer();
        StartCoroutine(explodeAfterDelay(explodeTimer));
        audioSource.Play();
    }

    IEnumerator explodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    void OnDisable()
    {
        StopAllCoroutines();
        audioSource.Stop();
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
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var col in colliders)
        {
            if (!(col.tag == "Enemy" || col.tag == "Player") || col.transform == transform || col.transform.IsChildOf(transform))
            {
                continue;
            }
            var raycastHits = Physics.RaycastAll(transform.position, col.transform.position, explosionRadius);
            Debug.DrawLine(transform.position, col.transform.position, Color.blue, 30f);
            var isHit = false;
            foreach (var hits in raycastHits)
            {
                if (!(hits.transform.IsChildOf(transform) || hits.transform == transform))
                {
                    continue;
                }
                isHit = true;
                break;
            }
            if (isHit)
            {
                var entity = col.gameObject.GetComponent<Entity>();
                if (entity != null)
                {
                    entity.TakeDamage(explosionDamage);
                }
            }
        }
        TakeDamage(health);
    }
}
