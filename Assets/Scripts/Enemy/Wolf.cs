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
    [SerializeField] private float attackChance = 0.1f;
    [SerializeField] private float attackChanceTick = 30f;

    public override void Spawn(Vector3 spawnPos)
    {
        base.Spawn(spawnPos);
        rb.MovePosition(spawnPos);
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
        if (target == null)
        {
            return;
        }
        animator.SetBool("Run Forward", true);
        agent.isStopped = false;
        updateDestinationCoroutine = StartCoroutine(updateDestination());
        randomAttackCoroutine = StartCoroutine(randomlyAttack());
    }

    void StopRunAround()
    {
        if (randomAttackCoroutine != null) StopCoroutine(randomAttackCoroutine);
        if (updateDestinationCoroutine != null) StopCoroutine(updateDestinationCoroutine);
    }

    private Coroutine randomAttackCoroutine;
    IEnumerator randomlyAttack()
    {
        for (; ; )
        {
            var rand = Random.Range(0f, 1f);
            if (rand < attackChance)
            {
                SetAttackPlayer();
                yield break;
            }
            yield return new WaitForSeconds(attackChanceTick);
        }
    }

    private Coroutine updateDestinationCoroutine;
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
            target = wr.GetTrueTarget();
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
            StopRunAround();
            agent.isStopped = false;
            StartCoroutine(DoAttack());
        }
    }

    public override void HitByProjectile(Projectile p, Vector3 power)
    {
        TakeDamage(p.GetDamage());
        if (p is GustProjectile)
        {
            Debug.Log("Hit by gust");
            StopRunAround();
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
        animator.SetBool("Run Forward", false);
        animator.SetBool("Resting", true);
        agent.isStopped = true;
        agent.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(power, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        animator.SetBool("Resting", false);
        rb.isKinematic = true;
        agent.enabled = true;
        agent.isStopped = false;
        rb.useGravity = false;
        SetRunAround();
    }
}
