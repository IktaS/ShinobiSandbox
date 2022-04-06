using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bat : Enemy
{
    private BatPerch bp;
    private Transform target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private string playerTag;
    [SerializeField] private float speed;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private ShootProjectile shoot;

    private Sequence seq;

    public override void Spawn(Vector3 spawnPos)
    {
        rb.MovePosition(spawnPos);
        SetMoveAround();
    }

    void SetMoveAround()
    {
        if (bp == null)
        {
            var go = GameObject.Find("BatPerch");
            this.bp = go.GetComponent<BatPerch>();
        }
        rb.isKinematic = true;
        target = bp.GetNode();
        animator.SetBool("Fly Forward", true);
        var duration = Vector3.Distance(target.position, rb.position) / speed;
        if (seq == null)
        {
            seq = DOTween.Sequence();
        }
        else
        {
            seq.Kill();
        }
        transform.LookAt(target.position, Vector3.up);
        seq.Append(rb.DOMove(target.position, duration)).
            OnComplete(() =>
            {
                transform.LookAt(bp.trueTarget.position, Vector3.up);
                shoot.Shoot(projectile, projectileSpeed);
            });
    }

    public override void handleProjectileHit(Projectile p, Vector3 power)
    {
        if (p is GustProjectile)
        {
            Debug.Log("Hit by gust");
            seq.Kill();
            animator.SetBool("Fly Forward", false);
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
        rb.isKinematic = true;
        rb.useGravity = false;
        SetMoveAround();
    }
}
