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
    [SerializeField] private float shootMoveAgainDelay;

    private Sequence seq;

    public override void Spawn(Vector3 spawnPos)
    {
        base.Spawn(spawnPos);
        rb.MovePosition(spawnPos);
        SetMoveAround();
    }

    void OnDisable()
    {
        StopAllCoroutines();
        seq.Kill();
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
        if (target == null)
        {
            return;
        }
        animator.SetBool("Fly Forward", true);
        var duration = Vector3.Distance(target.position, rb.position) / speed;
        if (seq != null)
        {
            seq.Kill();
        }
        transform.DOLookAt(target.position, 0f, AxisConstraint.None, Vector3.up);
        seq = DOTween.Sequence();
        seq.Append(rb.DOMove(target.position, duration)).
            Append(transform.DOLookAt(bp.GetTrueTarget().position, 2f, AxisConstraint.None, Vector3.up)).
            AppendCallback(() =>
            {
                shoot.Shoot(projectile, projectileSpeed);
            }).AppendInterval(shootMoveAgainDelay).OnComplete(() => SetMoveAround());

    }

    public override void HitByProjectile(Projectile p, Vector3 power)
    {
        TakeDamage(p.GetDamage());
        if (p is GustProjectile)
        {
            Debug.Log("Hit by gust");
            seq.Kill();
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

    protected IEnumerator ragdollByGust(Vector3 power)
    {
        animator.SetBool("Fly Forward", false);
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(power, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        rb.isKinematic = true;
        rb.useGravity = false;
        SetMoveAround();
    }
}
