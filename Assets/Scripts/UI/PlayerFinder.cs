using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour, ICanvasTarget
{
    private Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public Transform GetTarget()
    {
        if (playerPos != null)
        {
            return playerPos;
        }
        return null;
    }
}
