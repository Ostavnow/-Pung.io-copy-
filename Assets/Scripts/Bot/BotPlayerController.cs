using UnityEngine;
using UI;
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
            _direction = Vector3.Normalize(_targetPoint - transform.position);
            _angleZ = Mathf.Atan2(_direction.y,_direction.x) * Mathf.Rad2Deg - 90;
        }
    }
    private float _angleZ;
    private Vector3 _direction;
    public bool _isBotMoving = true;
    protected float _rotationSpeed = 30f;
    protected void Update()
    {
        if(_isBotMoving)
        {
            transform.position += _direction * Time.deltaTime * _speed; 
            _direction = Vector3.Normalize(TargetPoint - transform.position);
            _angleZ = Mathf.Atan2(_direction.y,_direction.x) * Mathf.Rad2Deg - 90;
            Debug.DrawLine(transform.position,TargetPoint,Color.yellow); 
            Debug.DrawLine(transform.position,transform.position + _direction,Color.green); 
        }
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,_angleZ),_rotationSpeed * Time.deltaTime);
    }
}
