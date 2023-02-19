using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Joystick motionJoystick;
    private Joystick attackJoystick;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    private Vector2 moveInput;
    [SerializeField]
    private float speed = 5;
    private float rotZ;
    [SerializeField]
    private GameObject hand;
    private Transform handRightPoint;
    private Transform handLeftPoint;
    private Player player;
    private bool isWillNextBlowBeRightSide;
    [SerializeField]
    private float timeAfterWhichBeNextBlow = 5;
    private float time;
    private float attackSpeed = 1;
    private GameManager gameManager;
    void Start()
    {
        motionJoystick = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Joystick>();
        attackJoystick = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Joystick>();
        handRightPoint = transform.GetChild(0);
        handLeftPoint = transform.GetChild(1);
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        moveInput = new Vector2(motionJoystick.Horizontal,motionJoystick.Vertical);
        if(time >= 0)
        {
            time -= Time.deltaTime;
        }
        if(Mathf.Abs(attackJoystick.Horizontal) != 0f || Mathf.Abs(attackJoystick.Vertical) != 0f)
        {
            speed = 1;
            rotZ = Mathf.Atan2(attackJoystick.Vertical,attackJoystick.Horizontal) * Mathf.Rad2Deg - 90;
            if(time <= 0 && (Mathf.Abs(attackJoystick.Horizontal) > 0.7f | Mathf.Abs(attackJoystick.Vertical) > 0.7f) & !(player.stamina <= 0))
            {
                time = timeAfterWhichBeNextBlow;
                hit();
            }
        }
        else
        {
            speed = 5;
        }
        if(Mathf.Abs(moveInput.x) > 0 | Mathf.Abs(moveInput.y) > 0)
        {
            rotZ = Mathf.Atan2(motionJoystick.Vertical,motionJoystick.Horizontal) * Mathf.Rad2Deg - 90;
        }
        transform.rotation = Quaternion.Euler(0,0,rotZ); 
    }

    private void hit()
    {
       GameObject currentHandGameObject;
       Hand currentHand;
       Vector3 randomPosition;
       float handRadius = hand.GetComponent<CircleCollider2D>().radius;
       Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
       player.stamina--;
       if(isWillNextBlowBeRightSide)
       {
            randomPosition = new Vector3(handRightPoint.position.x + randomValue.x,handRightPoint.position.y + randomValue.y,0);
            currentHandGameObject = Instantiate(hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z - 20 - Random.Range(0,30)));
            currentHand = currentHandGameObject.GetComponent<Hand>();
            currentHand.isRightHand = true;
            currentHand.direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z + Random.Range(0f,10f)));
            isWillNextBlowBeRightSide = false;
       }
       else
       {
            randomPosition = new Vector3(handLeftPoint.position.x - randomValue.x,handLeftPoint.position.y + randomValue.y,0);
            currentHandGameObject = Instantiate(hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z + 20 + Random.Range(0,30)));
            currentHand = currentHandGameObject.GetComponent<Hand>();
            currentHand.isRightHand = false;
            currentHand.direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z - Random.Range(0f,10f)));
            isWillNextBlowBeRightSide = true;
       }
       currentHand.BlowHand += player.BlowHit;
       currentHand.killEnemy += player.KillEnemy;
       StopCoroutine(player.coroutineStaminaHundler);
       player.coroutineStaminaHundler = StartCoroutine(player.RegenerationStamina());
       currentHand = currentHandGameObject.GetComponent<Hand>();
       currentHand.speed = 5;
       currentHand.damage = player.attackDamage;
       currentHand.initialPosition = randomPosition;
    }
    public void ImproveAttackDamage()
    {
        if(player.numberImprovents > 0)
        {
            player.numberImprovents--;
            player.attackDamage += player.multiplierAttackDamageImprovement * 1;
        }
    }
    public void ImproveHealth()
    {
        if(player.numberImprovents > 0)
        {
        player.numberImprovents--;
        player.fullHealth += player.healthImprovementMultiplier * 1;
        }
    }
    public void ImproveStamina()
    {
        if(player.numberImprovents > 0)
        {
        player.numberImprovents--;
        player.fullStamina += player.staminaImprovementMultiplier * 1;
        }
    }
    public void ImproveCriticalDamage()
    { 
        if(player.numberImprovents > 0)
        {
        player.numberImprovents--;
        player.criticalDamage += player.criticalDamageImprovementMultiplier * 1;
        }
    }
    public void ImproveAttackSpeed()
    {
        if(player.numberImprovents > 0)
        {
        player.numberImprovents--;
        player.attackSpeed += player.attackSpeedImprovementMultiplier * 1;
        }
    }
    private IEnumerator CoroutineFirstAbility()
    {
        yield return new WaitForSeconds(1);
    }
    private IEnumerator FirstAbility()
    {
        yield return new WaitForSeconds(1);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.deltaTime);
    }
}
