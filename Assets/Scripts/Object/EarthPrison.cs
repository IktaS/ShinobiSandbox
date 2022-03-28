using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class EarthPrison : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        Debug.Log("Activating earth prison in :" + transform.position + ":" + transform.rotation);
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}
