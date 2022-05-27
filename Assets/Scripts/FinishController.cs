using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FinishController : MonoBehaviour
{
    public float trowPower;
    
    public GameObject mainBall;
    public Transform cameraFinalPosition;
    
    public RotateMovement ballTrowArrowMovement_cs;
    
    public ParticleSystem ballToMainBallPS;
    
    private List<GameObject> balls;
    
    private void Start()
    {
        FindObjectOfType<PlayerMovement>().canMove = false;

        if (FindObjectOfType<CameraFinalMove>())
        {
            FindObjectOfType<CameraFinalMove>().StartMove();
        }
    }
    
    /// <summary>
    /// Когда мячики игрока соприкосаются с зоной у большого мяча - он расширяется
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Player")
        {
            balls = FindObjectOfType<BallController>().TakeBallList();
            FindObjectOfType<BallController>().finishSceneStart = true;
            mainBall.transform.localScale += (Vector3.one * 0.1f);

            FindObjectOfType<BallController>().DeledeBall(other.collider.gameObject);
            ballToMainBallPS.Play();
            ballTrowArrowMovement_cs.enabled = true;
        }
    }
    

    private void FixedUpdate()
    {
        //Все мячи при финише - катятся к главному
        balls = FindObjectOfType<BallController>().TakeBallList();
        
        if (balls.Count == 0)
        {
            ballToMainBallPS.Stop();
        }
        else
        {
            foreach (GameObject ball in balls)
            {
                ball.transform.LookAt(FindObjectOfType<FinishController>().mainBall.transform);
                ball.transform.position +=(ball.transform.forward * 0.1f);
            }   
        }

        if (balls.Count == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrowMainBall();
            }
        }
    }

    private void TrowMainBall()
    {
        ballTrowArrowMovement_cs.enabled = false;
        mainBall.GetComponent<Rigidbody>().isKinematic = false;
        mainBall.GetComponent<Rigidbody>().AddForce(ballTrowArrowMovement_cs.gameObject.transform.forward * trowPower, ForceMode.Impulse);
        StartCoroutine(WaitForActiveWinPanel());
    }

    private IEnumerator WaitForActiveWinPanel()
    {
        yield return new WaitForSeconds(5);
        FindObjectOfType<GameManager>().Win();
    }
}
