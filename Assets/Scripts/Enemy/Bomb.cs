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

    public override void Spawn(Vector3 spawnPos)
    {
        base.Spawn(spawnPos);
        rb.MovePosition(spawnPos);
        SetChasePlayer();
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
        if (p is FireProjectile)
        {
            Debug.Log("Hit by fire");
        }
        if (p is LightningProjectile)
        {
            Debug.Log("Hit by lightning");
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
}
