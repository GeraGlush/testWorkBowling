using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    public Wall[] nearWalls;
    public enum MathematicalOperation
    {
        Plus,
        Minus,
        Multiply
    }

    public MathematicalOperation mathematicalOperation;

    public int ballCount; 
    public TextMeshPro operationText;

    public GameObject door;

    private bool canDoOperation = true;
    private void Start()
    {
        if (mathematicalOperation == MathematicalOperation.Minus)
        {
            operationText.text = "-" + ballCount;
            door.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (mathematicalOperation == MathematicalOperation.Plus)
        {
            operationText.text = "+" + ballCount;
            door.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (mathematicalOperation == MathematicalOperation.Multiply)
        {
            operationText.text = "x" + ballCount;
            door.GetComponent<Renderer>().material.color = Color.magenta;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (canDoOperation)
            {
                WallOperation();
            }
            canDoOperation = false;
        }
        
    }

    private void WallOperation()
    {
        BallController ballController = FindObjectOfType<BallController>();
        if (mathematicalOperation == MathematicalOperation.Minus)
        {
            ballController.DeledeBallCount(ballCount);
        }
        else if (mathematicalOperation == MathematicalOperation.Plus)
        {
            ballController.AddBallCount(ballCount);
        }
        else if (mathematicalOperation == MathematicalOperation.Multiply)
        {
            ballController.AddBallCount((ballController.TakeBallList().Count * ballCount) - ballController.TakeBallList().Count);
        }

        if (nearWalls.Length > 0)
        {
            foreach (Wall wall in nearWalls)
            {
                Destroy(wall.operationText.gameObject);
                Destroy(wall.door);
                Destroy(wall);
            }
        }
        
        Destroy(operationText.gameObject);
        Destroy(door);
    }
}
