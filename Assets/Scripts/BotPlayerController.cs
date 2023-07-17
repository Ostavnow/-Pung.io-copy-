using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerUIHundlerBot))]
[RequireComponent(typeof(BotPlayerController))]
public class BotPlayerController : PlayerController
{
    private Vector3 _targetPoint;
    public Vector3 TargetPoint
    {
        get{return _targetPoint;}
        set
        {
            _targetPoint = value;
            direction = Vector3.Normalize(_targetPoint - transform.position);
            angleZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90;
        }
    }
    private float angleZ;
    private Vector3 direction;
    public bool isBotMoving = true;
    protected float rotationSpeed = 30f;
    protected void Update()
    {
        if(isBotMoving)
        {
            transform.position += (direction * Time.deltaTime * speed); 
            direction = Vector3.Normalize(TargetPoint - transform.position);
            angleZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90;
            Debug.DrawLine(transform.position,TargetPoint,Color.yellow); 
            Debug.DrawLine(transform.position,transform.position + direction,Color.green); 
        }
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,angleZ),rotationSpeed * Time.deltaTime);
    }
}
