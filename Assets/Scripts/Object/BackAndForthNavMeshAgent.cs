using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackAndForthNavMeshAgent : MonoBehaviour
{
    [SerializeField] private Transform otherPoint;

    void OnTriggerEnter(Collider other)
    {
        var nv = other.GetComponent<NavMeshAgent>();
        if (nv != null)
        {
            nv.SetDestination(otherPoint.position);
        }
    }
}
