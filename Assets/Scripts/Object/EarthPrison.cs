using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;
using UnityEngine.AI;
using NaughtyAttributes;

public class EarthPrison : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject placementObject;
    [SerializeField] private GameObject prisonObject;
    [SerializeField] private float removeDelay;
    [SerializeField] private float raiseAmount;
    [SerializeField] private float raiseTime;

    public void OnEnable()
    {
        prisonObject.SetActive(false);
    }

    [Button]
    public void Activate()
    {
        placementObject.SetActive(false);
        prisonObject.SetActive(true);
        var newPos = transform.position + transform.up * raiseAmount;
        StartCoroutine(MoveToPos(newPos, raiseTime));
        Invoke("ReturnGO", removeDelay);
        foreach (var col in colliderList)
        {
            var navmeshAgent = col.GetComponent<NavMeshAgent>();
            if (navmeshAgent)
            {
                navmeshAgent.isStopped = true;
            }
        }
    }

    IEnumerator MoveToPos(Vector3 position, float time)
    {
        float elapsedTime = 0;
        var currentPos = rb.position;

        while (elapsedTime < time)
        {
            Vector3 newPos = Vector3.Lerp(currentPos, position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            rb.MovePosition(newPos);

            yield return null;
        }
        rb.MovePosition(position);
        yield return null;
    }

    private void ReturnGO()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
        foreach (var col in colliderList)
        {
            var navmeshAgent = col.GetComponent<NavMeshAgent>();
            if (navmeshAgent)
            {
                navmeshAgent.isStopped = false;
            }
        }
        placementObject.SetActive(true);
    }

    public List<GameObject> colliderList = new List<GameObject>();

    public void OnTriggerEnter(Collider collider)
    {
        if (!colliderList.Contains(collider.gameObject))
        {
            colliderList.Add(collider.gameObject);
            Debug.Log("Added " + gameObject.name);
            Debug.Log("GameObjects in list: " + colliderList.Count);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (colliderList.Contains(collider.gameObject))
        {
            colliderList.Remove(collider.gameObject);
            Debug.Log("Removed " + gameObject.name);
            Debug.Log("GameObjects in list: " + colliderList.Count);
        }
    }
}
