using System.Collections;
using UnityEngine;

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
        handRightPoint = transform.GetChild(0);
        handLeftPoint = transform.GetChild(1);
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerUIHundler = player._playerUIHundler;
        timer = GetComponent<Timer>();
    }

    protected void Update()
    {
        moveInput = new Vector2(motionJoystick.Horizontal,motionJoystick.Vertical);
        if(Mathf.Abs(attackJoystick.Horizontal) != 0f || Mathf.Abs(attackJoystick.Vertical) != 0f)
        {
            speed = 1;
            rotZ = Mathf.Atan2(attackJoystick.Vertical,attackJoystick.Horizontal) * Mathf.Rad2Deg - 90;
            if((Mathf.Abs(attackJoystick.Horizontal) > 0.7f | Mathf.Abs(attackJoystick.Vertical) > 0.7f) & !(player.Stamina <= 0))
            {
                Hit();
            }
        }
        else
        {
            if(!isActiveDash)
            {
                speed = 5;
            }
        }
        if(Mathf.Abs(moveInput.x) > 0 | Mathf.Abs(moveInput.y) > 0)
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
