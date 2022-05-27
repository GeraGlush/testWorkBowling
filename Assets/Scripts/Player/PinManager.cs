using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public int pinDestroy;

    public int allPinCount;
    
    void Start()
    {
        allPinCount = FindObjectsOfType<Pin>().Length;
    }
}
