using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

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
    public void Activate()
    {
        placementObject.SetActive(false);
        prisonObject.SetActive(true);
        var newPos = transform.position + transform.up * raiseAmount;
        StartCoroutine(MoveToPos(newPos, raiseTime));
        Invoke("ReturnGO", removeDelay);
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
        placementObject.SetActive(true);
    }
}
