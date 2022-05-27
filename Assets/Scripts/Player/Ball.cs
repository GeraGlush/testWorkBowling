using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform myStartPosition;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "KillWall")
        {
            FindObjectOfType<BallController>().DeledeBall(gameObject);
        }
        else if (other.gameObject.tag == "Finish")
        {
            FindObjectOfType<FinishController>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Trampline")
        {
            for (int i = 0; i < 20; i++)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);   
            }
        }
    }
}
