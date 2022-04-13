using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealth : MonoBehaviour
{
    private bool active;

    [SerializeField] private float recoverRate;
    [SerializeField] private float recoverTime;
    [SerializeField] private Entity targetEntity;
    [SerializeField] private GameObject recoverParticle;

    private float elapsedTime = 0;
    public void ActivateRecover()
    {
        active = true;
        elapsedTime = 0;
        recoverParticle.SetActive(true);
    }

    public void DeactivateRecover()
    {
        active = false;
        elapsedTime = 0;
        recoverParticle.SetActive(false);
    }

    void Update()
    {
        if (active)
        {
            if (elapsedTime > recoverTime)
            {
                targetEntity.TakeHeal(recoverRate);
            }
            elapsedTime += Time.deltaTime;
        }
    }
}
