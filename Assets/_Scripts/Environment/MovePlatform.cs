using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 from;
    public Vector3 to;
    public float speed;

    public float duration;

    private void Start()
    {
        
    }
    public void ReturnPosition()
    {   
        StopAllCoroutines();
        StartCoroutine(TranPlatform(from));
    }

    public void ChangePosition()
    {
        StopAllCoroutines();
        StartCoroutine(TranPlatform(to));
    }

    private IEnumerator TranPlatform(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void Update()
    {
        Debug.Log("Position:"+transform.position);
    }
}
