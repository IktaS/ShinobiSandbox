using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Wolf : Enemy
{
    private WolfRotation wr;
    private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private string playerTag;

    [SerializeField] private float updateDestinationRate = 0.5f;

    new public void Spawn()
    {
        SetRunAround();
    }

    void SetRunAround()
    {
        if (wr == null)
        {
            var go = GameObject.Find("WolfRotation");
            this.wr = go.GetComponent<WolfRotation>();
        }
        target = wr.GetNode();
        animator.SetBool("Run Forward", true);
        agent.isStopped = false;
        StartCoroutine(updateDestination());
    }

    IEnumerator updateDestination()
    {
        for (; ; )
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
            yield return new WaitForSeconds(updateDestinationRate);
        }
    }

    [Button]
    void SetAttackPlayer()
    {
        if (wr != null)
        {
            StopCoroutine(updateDestination());
            target = wr.trueTarget;
        }
    }

    IEnumerator DoAttack()
    {
        animator.SetTrigger("Bite Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SetRunAround();
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == playerTag)
        {
            agent.isStopped = false;
            StartCoroutine(DoAttack());
        }
    }

    public override void handleProjectileHit(Projectile p, Vector3 power)
    {
        if (p is GustProjectile)
        {
            Debug.Log("Hit by gust");
            StopCoroutine(updateDestination());
            animator.SetBool("Run Forward", false);
            animator.SetBool("Resting", true);
            agent.isStopped = true;
            agent.enabled = false;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(power, ForceMode.Impulse);
            StartCoroutine(monitorVelocity());
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

    IEnumerator monitorVelocity()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("Resting", false);
        rb.isKinematic = true;
        agent.enabled = true;
        agent.isStopped = false;
        rb.useGravity = false;
        SetRunAround();
    }
}
