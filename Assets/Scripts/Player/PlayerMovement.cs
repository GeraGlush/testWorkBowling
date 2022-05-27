using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool cameraMoving;
    private bool MouseButtonDown;
    private Vector3 swipePos;
    private Vector3 dragPos;

    private BallController ballController_cs;

    public float mowementAllBallsSpeed;
    
    public bool canMove;
    

    private void Start()
    {
        if (GetComponent<BallController>())
        {
            ballController_cs = GetComponent<BallController>();
        }
        else
        {
            Debug.LogError("PlayerMovement и BallController должны находиться на одном обьекте!");
        }
    }

    /// <summary>
    /// Управление
    /// </summary>
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0) && MouseButtonDown == false)
            {

                swipePos = Input.mousePosition;
                MouseButtonDown = true;
                cameraMoving = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                MouseButtonDown = false;
                dragPos = swipePos;
            }

            if (MouseButtonDown)
            {
                dragPos = Input.mousePosition;

                if (swipePos.x < dragPos.x - Screen.width / 15)
                {
                    cameraMoving = true;
                    transform.position -= new Vector3((dragPos.x - swipePos.x) / (Screen.width / 2.5f), 0, 0);
                }

                if (swipePos.x > dragPos.x + Screen.width / 15)
                {
                    cameraMoving = true;
                    transform.position += new Vector3((swipePos.x - dragPos.x) / (Screen.width / 2.5f), 0, 0);
                }

            }
            
            transform.position += new Vector3(0f, 0f, mowementAllBallsSpeed / 10f);
        }
    }
}
