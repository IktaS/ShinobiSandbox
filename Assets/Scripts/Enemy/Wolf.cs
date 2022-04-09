using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Wolf : Enemy
{
    private WolfRotation wr;
    [SerializeField] private Transform target;
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
            wr = GameObject.FindObjectOfType<WolfRotation>();
        }
        target = wr.GetNode();
        animator.SetBool("Run Forward", true);
        randomAttackCoroutine = StartCoroutine(randomlyAttack());
    }

    void FixedUpdate()
    {
        if (!agent.isStopped)
        {
            agent.SetDestination(target.position);
        }
    }

    [SerializeField] private Coroutine randomAttackCoroutine;
    IEnumerator randomlyAttack()
    {
        for (; ; )
        {
            var rand = Random.Range(0f, 1f);
            if (rand < attackChance)
            {
                SetAttackPlayer();
                randomAttackCoroutine = null;
                yield break;
            }
            yield return new WaitForSeconds(attackChanceTick);
        }
    }

    [Button]
    void SetAttackPlayer()
    {
        if (wr == null)
        {
            wr = GameObject.FindObjectOfType<WolfRotation>();
        }
        target = wr.GetTrueTarget();
        if (randomAttackCoroutine != null) StopCoroutine(randomAttackCoroutine);
    }

    IEnumerator DoAttack()
    {
        animator.SetTrigger("Bite Attack");
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        agent.isStopped = false;
        SetRunAround();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {

            StartCoroutine(DoAttack());
        }
    }

    public override void HitByProjectile(Projectile p, Vector3 power)
    {
        TakeDamage(p.GetDamage());
        if (p is GustProjectile)
        {
            StartCoroutine(ragdollByGust(power));
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
