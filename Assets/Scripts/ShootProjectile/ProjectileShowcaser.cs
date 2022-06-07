using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProjectileShowcaser : MonoBehaviour
{
    [SerializeField] private GameObject currentProjectile;

    [SerializeField] private AimRef aim;
    [SerializeField] private Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);

    private GameObject _go;

    public void ShowcaseProjectile(GameObject go)
    {
        var (start, end) = aim.GetAim();
        currentProjectile = go;
        _go = Instantiate(currentProjectile, end.position, Quaternion.LookRotation(end.position, Vector3.up));
        _go.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        _go.transform.SetParent(end.transform);
    }

    [Button]
    public void ShowcaseCurrentProjectile()
    {
        ShowcaseProjectile(currentProjectile);
    }

    [Button]
    public void DeleteProjectile()
    {
        if (_go)
        {
            Destroy(_go);
        }
    }
}
