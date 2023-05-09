using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotLogic : Player
{
    public enum BotStates {Pumping, Attack, Flight, Despair};
    public BotStates botStates;
    [HideInInspector]
    protected float speed = 5f;
    private Vector3 p_targetPoint;
    protected Vector3 targetPoint
    {
        get{return p_targetPoint;}
        set
        {
            p_targetPoint = value;
            direction = Vector3.Normalize(p_targetPoint - transform.position);
            angleZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90;
        }
    }
    private float angleZ;
    private Vector3 direction;
    protected Collider2D[] punchingBag;
    protected Collider2D[] players;
    protected Collider2D[] Hand;
    protected bool isBotMoving = true;
    protected float rotationSpeed = 30f;
    [SerializeField]
    protected float timeAfterWhichBeNextBlow = 0.5f;
    protected const float timeAfterWhichBeNextBlowConst = 0.5f;
    protected float time;
    public GameObject hand;
    public GameObject shield;
    private bool isWillNextBlowBeRightSide;
    public bool isActivePunchSwarm;
    public bool isActiveDash;
    public float flightDistance = 2.5f;
    public Transform handRightPoint;
    public Transform handLeftPoint;
    private MainUIHandler mainUIHandler;
    private int comboValue;
    private void Start()
    {
        handRightPoint = transform.GetChild(0);
        handLeftPoint = transform.GetChild(1);
    }
    private void Update()
    {
        if(time >= 0)
        {
            time -= Time.deltaTime;
        }
        if(isBotMoving)
        {
            transform.position += (direction * Time.deltaTime * speed);  
            Debug.DrawLine(transform.position,targetPoint,Color.yellow); 
            Debug.DrawLine(transform.position,direction,Color.yellow); 
        }
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,angleZ),rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        punchingBag = OverlapBoxAllPuchingBag();
        players = OverlapBoxAllPlayer();
        if(botStates == BotLogic.BotStates.Pumping)
            Pumping();
        if(botStates == BotLogic.BotStates.Attack)
            Attack();
        if(botStates == BotLogic.BotStates.Flight)
            Flight();
        if(botStates == BotLogic.BotStates.Despair)
            Despair();
    }
    public abstract void Pumping();
    public abstract void Attack();
    public abstract void Flight();
    public abstract void Despair();
    private void AvoidingObstacles()
    {
        if(Physics2D.Raycast(transform.position,Vector2.down,5))
        {
            if(Physics2D.Raycast(transform.position,Vector2.left,5))
            targetPoint = new Vector3(transform.position.x + 5,transform.position.y + Random.value,0);
            else if(Physics2D.Raycast(transform.position,Vector2.right,5))
            targetPoint = new Vector3(transform.position.x - 5,transform.position.y + Random.value,0);

        }
        else if(Physics2D.Raycast(transform.position,Vector2.up,5))
        {
            if(Physics2D.Raycast(transform.position,Vector2.left))
            targetPoint = new Vector3(transform.position.x + 5,transform.position.y - Random.value,0);
            else
            targetPoint = new Vector3(transform.position.x - 5,transform.position.y - Random.value,0);
        }
        else if(Physics2D.Raycast(transform.position,Vector2.left,5))
        {
            if(Physics2D.Raycast(transform.position,Vector2.down))
            targetPoint = new Vector3(transform.position.x + Random.value,transform.position.y + Random.value,0);
            else
            targetPoint = new Vector3(transform.position.x - 5,transform.position.y + Random.value,0);
        }
        else if(Physics2D.Raycast(transform.position,Vector2.right,5))
        {
            if(Physics2D.Raycast(transform.position,Vector2.down))
            targetPoint = new Vector3(transform.position.x + 5,transform.position.y - Random.value,0);
            else
            targetPoint = new Vector3(transform.position.x - 5,transform.position.y + Random.value,0);
        }
    }
    private Collider2D[] OverlapBoxAllPuchingBag()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position,new Vector2(16,10),0);
        Collider2D[] layerMaskColliders = new Collider2D[5];
        for(int i = 0,y = 0;i < colliders.Length && i < 5;i++)
        {
            if(colliders[i].gameObject.layer == 7)
            {
                layerMaskColliders[y] = colliders[i];
                y++;
            }
        }
        return layerMaskColliders;
    }
    private Collider2D[] OverlapBoxAllPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position,new Vector2(16,10),0);
        int numberOfPlayerColliders = colliders.Length;
        int numberSortedItems = 0;
        for(int numberCheckedItems = 0;numberCheckedItems < colliders.Length;numberCheckedItems++)
        {
            if(colliders[numberCheckedItems].gameObject.layer != 3)
            {
                numberOfPlayerColliders--;
            }
            else if(colliders[numberCheckedItems].gameObject.layer == 3)
            {
                if(numberCheckedItems != numberSortedItems)
                {
                    colliders[numberSortedItems] = colliders[numberCheckedItems];
                    colliders[numberCheckedItems] = null;
                    numberSortedItems++;
                }
                else
                {
                    numberSortedItems++;
                }
            }
        }
        for(int i = numberOfPlayerColliders - 1;i > 0;)
        {
            Debug.Log(colliders[0] + " " + colliders[1]);
            if(Vector2.Distance(colliders[i - 1].transform.position,transform.position) > Vector2.Distance(colliders[i].transform.position,transform.position))
            {
                int y = i;
                while(y != 0 && Vector2.Distance(colliders[y - 1].transform.position,transform.position) > Vector2.Distance(colliders[y].transform.position,transform.position))
                {
                    Debug.LogWarning("Сработала сортировка");
                    Collider2D collider = colliders[y];
                    colliders[y] = colliders[y - 1];
                    colliders[y - 1] = collider;
                    y--;
                }
            }
            else
            {
                i--;
            }
        }
        Collider2D[] layerMaskColliders = new Collider2D[5];
        for(int i = 1,y = 0;i < numberOfPlayerColliders && y < 5;i++)
        {
                layerMaskColliders[y] = colliders[i];
                y++;
        }
        return layerMaskColliders;
    }
    protected void Hit()
    {
        if(time <= 0)
         {
            time = timeAfterWhichBeNextBlow;
            UsualHit();
        StopCoroutine(coroutineStaminaHundler);
        coroutineStaminaHundler = StartCoroutine(RegenerationStamina());
        }
        
    }
    public void HandCreate(Vector3 randomPosition,float impactSpreat,ref bool isWillNextBlowBeRightSide)
    {
        GameObject currentHandGameObject;
        Hand currentHand;
        stamina--;
        Quaternion direction;
        if(isWillNextBlowBeRightSide)
        {
            currentHandGameObject = Instantiate(hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z - 20 - Random.Range(0,30)));
            direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z + Random.Range(0f,impactSpreat)));
        }
        else
        {
            currentHandGameObject = Instantiate(hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z + 20 + Random.Range(0,30)));
            direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z - Random.Range(0f,impactSpreat)));
        }
        currentHand = currentHandGameObject.GetComponent<Hand>();
        currentHand.isRightHand = isWillNextBlowBeRightSide;
        currentHand.direction = direction;
        isWillNextBlowBeRightSide = !isWillNextBlowBeRightSide;
        currentHand.BlowHand += BlowHit;
        currentHand.damage = attackDamage;
        currentHand.initialPosition = randomPosition;
        currentHand.flightDistance = flightDistance;
    }
    private void UsualHit()
    {
        Vector3 randomPosition;
        float handRadius = hand.GetComponent<CircleCollider2D>().radius;
        Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
            if(isWillNextBlowBeRightSide)
            {
                randomPosition = new Vector3(handRightPoint.position.x + randomValue.x,handRightPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,10,ref isWillNextBlowBeRightSide);
            }
            else
            {
                randomPosition = new Vector3(handLeftPoint.position.x + randomValue.x,handLeftPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,10,ref isWillNextBlowBeRightSide);
            }
    }
    public void ImproveAttackDamage()
    {
        if(numberImprovents > 0)
        {
            numberImprovents--;
            attackDamage += multiplierAttackDamageImprovement * 1;
        }
    }
    public void ImproveHealth()
    {
        if(numberImprovents > 0)
        {
        numberImprovents--;
        fullHealth += healthImprovementMultiplier * 1;
        }
    }
    public void ImproveStamina()
    {
        if(numberImprovents > 0)
        {
        numberImprovents--;
        fullStamina += staminaImprovementMultiplier * 1;
        }
    }
    public void ImproveCriticalDamage()
    { 
        if(numberImprovents > 0)
        {
        numberImprovents--;
        criticalDamage += criticalDamageImprovementMultiplier * 1;
        }
    }
    public void ImproveAttackSpeed()
    {
        if(numberImprovents > 0 & attackSpeed < 1000)
        {
        numberImprovents--;
        attackSpeed += attackSpeedImprovementMultiplier * 1;
        timeAfterWhichBeNextBlow = timeAfterWhichBeNextBlowConst - (attackSpeed / 700f);
        if(timeAfterWhichBeNextBlowConst - (attackSpeed / 700f) < 0.01f)
        {
            timeAfterWhichBeNextBlow = 0.01f;
        }
        if(attackSpeed > 1000)
        {
            attackSpeed = 1000;
        }
        }
    }
}