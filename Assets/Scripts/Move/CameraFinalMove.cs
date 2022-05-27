using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinalMove : MonoBehaviour
{
    public float speed = 1f;
    
    public void StartMove()
    {
        StartCoroutine(MoveCor(FindObjectOfType<FinishController>().cameraFinalPosition.position));
    }
    
    private IEnumerator MoveCor(Vector3 to)
    {
        Vector3 from = transform.position;
        float distance = Vector3.Distance(from, to);
        float rate = speed / distance;

        for (float t = 0; t < 1; t += rate * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        
        transform.position = to;
    }
}
