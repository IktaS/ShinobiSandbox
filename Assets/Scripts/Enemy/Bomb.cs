using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bomb : Enemy
{
    private WolfRotation wr;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    public override void Spawn(Vector3 spawnPos)
    {
        rb.MovePosition(spawnPos);
        SetChasePlayer();
    }

    void SetChasePlayer()
    {
        if (wr == null)
        {
            var go = GameObject.Find("WolfRotation");
            this.wr = go.GetComponent<WolfRotation>();
        }
        var target = wr.trueTarget;
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
    public override void HitByProjectile(Projectile p, Vector3 power)
    {
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
