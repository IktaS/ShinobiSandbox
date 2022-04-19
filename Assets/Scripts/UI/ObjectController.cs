using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private List<GameObject> startObjects = new List<GameObject>();
    private Dictionary<GameObject, Vector3> startObjectsInitScale = new Dictionary<GameObject, Vector3>();
    [SerializeField] private bool startObjActiveFromStart = false;
    [SerializeField] private List<GameObject> playObjects = new List<GameObject>();
    private Dictionary<GameObject, Vector3> playObjectsInitScale = new Dictionary<GameObject, Vector3>();
    [SerializeField] private bool playObjActiveFromStart = false;
    [SerializeField] private List<GameObject> dieObjects;
    private Dictionary<GameObject, Vector3> dieObjectsInitScale = new Dictionary<GameObject, Vector3>();
    [SerializeField] private bool dieObjActiveFromStart = false;

    void Start()
    {
        foreach (var go in startObjects)
        {
            startObjectsInitScale.Add(go, go.transform.localScale);
            if (!startObjActiveFromStart && go.activeInHierarchy)
            {
                go.transform.localScale = Vector3.zero;
                go.SetActive(false);
            }
            if (startObjActiveFromStart && !go.activeInHierarchy)
            {
                go.SetActive(true);
                continue;
            }
        }
        foreach (var go in dieObjects)
        {
            dieObjectsInitScale.Add(go, go.transform.localScale);
            if (!dieObjActiveFromStart && go.activeInHierarchy)
            {
                go.transform.localScale = Vector3.zero;
                go.SetActive(false);
            }
            if (dieObjActiveFromStart && !go.activeInHierarchy)
            {
                go.SetActive(true);
                continue;
            }
        }
        foreach (var go in playObjects)
        {
            playObjectsInitScale.Add(go, go.transform.localScale);
            if (!playObjActiveFromStart && go.activeInHierarchy)
            {
                go.transform.localScale = Vector3.zero;
                go.SetActive(false);
            }
            if (playObjActiveFromStart && !go.activeInHierarchy)
            {
                go.SetActive(true);
                continue;
            }
        }
    }

    [Button]
    public void ShowAfterDeathScreen()
    {
        foreach (var go in startObjects)
        {
            if (!go.activeInHierarchy) continue;
            go.transform.DOScale(Vector3.zero, 1f);
            go.SetActive(false);
        }
        foreach (var go in playObjects)
        {
            if (!go.activeInHierarchy) continue;
            go.transform.DOScale(Vector3.zero, 1f);
            go.SetActive(false);
        }
        foreach (var go in dieObjects)
        {
            if (go.activeInHierarchy) continue;
            go.SetActive(true);
            go.transform.DOScale(dieObjectsInitScale.GetValueOrDefault(go, Vector3.one), 1f);
        }
    }

    [Button]
    public void ShowMainMenu()
    {
        foreach (var go in dieObjects)
        {
            if (!go.activeInHierarchy) continue;
            go.transform.DOScale(Vector3.zero, 1f);
            go.SetActive(false);
        }
        foreach (var go in playObjects)
        {
            if (!go.activeInHierarchy) continue;
            go.transform.DOScale(Vector3.zero, 1f);
            go.SetActive(false);
        }
        foreach (var go in startObjects)
        {
            if (go.activeInHierarchy) continue;
            go.SetActive(true);
            go.transform.DOScale(dieObjectsInitScale.GetValueOrDefault(go, Vector3.one), 1f);
        }
    }

    [Button]
    public void StartPlay()
    {
        foreach (var go in dieObjects)
        {
            if (!go.activeInHierarchy) continue;
            go.transform.DOScale(Vector3.zero, 1f);
            go.SetActive(false);
        }
        foreach (var go in startObjects)
        {
            if (!go.activeInHierarchy) continue;
            go.transform.DOScale(Vector3.zero, 1f);
            go.SetActive(false);
        }
        foreach (var go in playObjects)
        {
            if (go.activeInHierarchy) continue;
            go.SetActive(true);
            go.transform.DOScale(dieObjectsInitScale.GetValueOrDefault(go, Vector3.one), 1f);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
