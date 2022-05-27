using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeledePsForTime : MonoBehaviour
{
    public float timeForDelede = 10f;
    void Start()
    {
        StartCoroutine(DeledeForTime());
    }

    private IEnumerator DeledeForTime()
    {
        yield return new WaitForSeconds(timeForDelede);
        Destroy(gameObject);
    }
}
