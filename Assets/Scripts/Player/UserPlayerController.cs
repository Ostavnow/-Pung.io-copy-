using System.Collections;
using UnityEngine;
using UI;
public class UserPlayerController : PlayerController
{
    private Joystick motionJoystick;
    private Joystick attackJoystick;
    [HideInInspector]
    private float rotZ;
    [SerializeField]

    void Start()
    {
        motionJoystick = GameObject.FindGameObjectWithTag("player canvas").transform.GetChild(0).GetComponent<Joystick>();
        attackJoystick = GameObject.FindGameObjectWithTag("player canvas").transform.GetChild(1).GetComponent<Joystick>();
        _handRightPoint = transform.GetChild(0);
        _handLeftPoint = transform.GetChild(1);
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _playerUIHundler = _player._playerUIHundler;
        _timer = GetComponent<Timer>();
    }

    protected void Update()
    {
        _moveInput = new Vector2(motionJoystick.Horizontal,motionJoystick.Vertical);
        rotZ = Mathf.Atan2(attackJoystick.Vertical,attackJoystick.Horizontal) * Mathf.Rad2Deg - 90;
        if(Mathf.Abs(attackJoystick.Horizontal) != 0f || Mathf.Abs(attackJoystick.Vertical) != 0f)
        {
            _speed = 1;
            if((Mathf.Abs(attackJoystick.Horizontal) > 0.7f | Mathf.Abs(attackJoystick.Vertical) > 0.7f) & !(_player.Stamina <= 0))
            {
                Hit();
            }
        }
        if(Mathf.Abs(_moveInput.x) > 0 | Mathf.Abs(_moveInput.y) > 0)
        {
            rotZ = Mathf.Atan2(motionJoystick.Vertical,motionJoystick.Horizontal) * Mathf.Rad2Deg - 90;
            if(Mathf.Abs(attackJoystick.Horizontal) != 0f || Mathf.Abs(attackJoystick.Vertical) != 0f)
            {
                rotZ = Mathf.Atan2(attackJoystick.Vertical,attackJoystick.Horizontal) * Mathf.Rad2Deg - 90;
            }
        }
        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
