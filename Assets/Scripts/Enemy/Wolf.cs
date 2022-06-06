using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Wolf : Enemy
{
    private WolfRotation wr;
    [SerializeField] private Transform _target;
    private Transform target
    {
        get => _target;
        set => _target = value;
    }
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip growlAudio;
    [SerializeField] private AudioClip biteAudio;

    [SerializeField] private string playerTag;
    [SerializeField] private string shieldTag;

    [SerializeField] private float updateDestinationRate = 0.5f;
    [SerializeField] private float attackChance = 0.1f;
    [SerializeField] private float attackChanceTick = 30f;
    [SerializeField] private float damage = 30f;

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

    void OnDisable()
    {
        StopAllCoroutines();
        randomAttackCoroutine = null;
    }

    void FixedUpdate()
    {
        if (!agent.isStopped)
        {
            agent.SetDestination(target.position);
        }
    }

    private float minimumDelayTime = 2f;
    private Coroutine randomAttackCoroutine;
    IEnumerator randomlyAttack()
    {
        yield return new WaitForSeconds(minimumDelayTime);
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
        audioSource.PlayOneShot(growlAudio);

        if (wr == null)
        {
            wr = GameObject.FindObjectOfType<WolfRotation>();
        }
        target = wr.GetTrueTarget();
        if (randomAttackCoroutine != null) StopCoroutine(randomAttackCoroutine);
    }

    IEnumerator DoAttack(Player p)
    {
        audioSource.PlayOneShot(biteAudio);
        animator.SetTrigger("Bite Attack");
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        p?.TakeDamage(damage);
        agent.isStopped = false;
        SetRunAround();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            Player p = other.gameObject.GetComponent<Player>();
            StartCoroutine(DoAttack(p));
        }
        if (other.gameObject.tag == shieldTag)
        {
            SetRunAround();
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

    protected IEnumerator ragdollByGust(Vector3 power)
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
