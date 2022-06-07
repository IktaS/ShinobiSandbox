using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProjectileShowcaser : MonoBehaviour
{
    [SerializeField] private GameObject currentProjectile;

    [SerializeField] private AimRef aim;
    [SerializeField] private Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);

    public void ShowcaseProjectile(GameObject go)
    {
        var (start, end) = aim.GetAim();
        DeleteProjectile();
        currentProjectile = Instantiate(go, end.position, Quaternion.LookRotation(end.position, Vector3.up));
        currentProjectile.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        currentProjectile.transform.SetParent(end.transform);
    }

    [Button]
    public void ShowcaseCurrentProjectile()
    {
        ShowcaseProjectile(currentProjectile);
    }

    [Button]
    public void DeleteProjectile()
    {
        if (currentProjectile)
        {
            Destroy(currentProjectile);
            currentProjectile = null;
        }
    }
}
