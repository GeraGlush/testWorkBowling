using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateMovement : MonoBehaviour
{
    [SerializeField] private bool circleMove = true;

    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Vector3 move;
    
    [SerializeField] private float minAngle = 0; 
    [SerializeField] private float maxAngle = 90f;
    
    private float _angle = 0;

    private void FixedUpdate()
    {
        if (circleMove)
        {
            transform.Rotate(move * speed); 
        }
        else
        {
            AngleRotation();
        }
    }
    
    private void AngleRotation()
    {
        var nextAngle = _angle + speed * Time.deltaTime;

        _angle = Mathf.Clamp(nextAngle, minAngle, maxAngle);

        transform.rotation = Quaternion.Euler(0, _angle, 0);

        if (_angle == minAngle || _angle == maxAngle) SwitchDirection();
    }

    private void SwitchDirection() => speed *= -1;
}
