using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class PointableDummyButton : PointableObject, IProjectileHittable
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private float canvasSelectedScale;
    [SerializeField] private float canvasAnimationDuration;
    [SerializeField] private UnityEvent onHit;

    private Vector3 initScale;

    void Awake()
    {
        initScale = canvas.transform.localScale;
    }

    void Start()
    {
        onPointed.AddListener(ScaleTextWhenPointed);
        onUnpointed.AddListener(ScaleTextWhenUnpointed);
    }

    private void ScaleTextWhenPointed(Pointer p)
    {
        canvas.transform.DOScale(canvasSelectedScale, canvasAnimationDuration);
    }

    private void ScaleTextWhenUnpointed(Pointer p)
    {
        canvas.transform.DOScale(initScale, canvasAnimationDuration);
    }

    public virtual void HitByProjectile(Projectile p, Vector3 power)
    {
        Debug.Log("enter  sdasdf");
        onHit.Invoke();
    }
}
