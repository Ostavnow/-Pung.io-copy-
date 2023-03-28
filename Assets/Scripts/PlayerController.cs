using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MainUIHandler mainUIHandler;
    private Joystick motionJoystick;
    private Joystick attackJoystick;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    private Vector2 moveInput;
    [HideInInspector]
    public float speed = 5f;
    private float rotZ;
    [SerializeField]
    public GameObject hand;
    public GameObject shield;
    private Transform handRightPoint;
    private Transform handLeftPoint;
    [HideInInspector]
    public Player player;
    private bool isWillNextBlowBeRightSide;
    private bool isWillNextBlowBeRightSide1;
    [SerializeField]
    private float timeAfterWhichBeNextBlow = 5f;
    private const float timeAfterWhichBeNextBlowConst = 0.5f;
    private float time;
    private float timeFourarms;
    private float attackSpeed = 1;
    public bool isActivePunchSwarm;
    public bool isActiveDash;
    public bool isFourarms;
    private GameManager gameManager;
    public float flightDistance = 2.5f;
    private bool isFirstAbilityRecovered = true;
    private bool isSecondAbilityRecovered = true;
    private bool isThirdAbilityRecovered = true;
    private bool isFourthAbilityRecovered = true;
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
        mainUIHandler = FindObjectOfType<MainUIHandler>();
    }

    void Update()
    {
        moveInput = new Vector2(motionJoystick.Horizontal,motionJoystick.Vertical);
        if(time >= 0)
        {
            time -= Time.deltaTime;
        }
        if(isFourarms & timeFourarms >= 0)
        {
            timeFourarms -= Time.deltaTime;
        }
        else if(isFourarms & timeFourarms <= 0)
        {
            timeFourarms = timeAfterWhichBeNextBlow;
            Vector3 randomPosition;
        float handRadius = hand.GetComponent<CircleCollider2D>().radius;
        Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
            if(isWillNextBlowBeRightSide1)
            {
                randomPosition = new Vector3(handRightPoint.position.x + randomValue.x,handRightPoint.position.y + randomValue.y,0);
                isWillNextBlowBeRightSide1 = false;
                HandCreate(randomPosition,360);
            }
            else
            {
                randomPosition = new Vector3(handLeftPoint.position.x + randomValue.x,handLeftPoint.position.y + randomValue.y,0);
                isWillNextBlowBeRightSide1 = true;
                HandCreate(randomPosition,360);
            }
        }
        if(Mathf.Abs(attackJoystick.Horizontal) != 0f || Mathf.Abs(attackJoystick.Vertical) != 0f)
        {
            speed = 1;
            rotZ = Mathf.Atan2(attackJoystick.Vertical,attackJoystick.Horizontal) * Mathf.Rad2Deg - 90;
            if(time <= 0 && (Mathf.Abs(attackJoystick.Horizontal) > 0.7f | Mathf.Abs(attackJoystick.Vertical) > 0.7f) & !(player.stamina <= 0))
            {
                time = timeAfterWhichBeNextBlow;
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

    private void Hit()
    {
        if(isActivePunchSwarm)
        {
            PunchSwarm();
        }
        else
        {
            UsualHit();
        }
    StopCoroutine(player.coroutineStaminaHundler);
    player.coroutineStaminaHundler = StartCoroutine(player.RegenerationStamina());
    }
    private void HandCreate(Vector3 randomPosition,float impactSpreat)
    {
        GameObject currentHandGameObject;
        Hand currentHand;
        player.stamina--;
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
        currentHand.BlowHand += player.BlowHit;
        currentHand.killEnemy += player.KillEnemy;
        currentHand.damage = player.attackDamage;
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
                HandCreate(randomPosition,10);
            }
            else
            {
                randomPosition = new Vector3(handLeftPoint.position.x + randomValue.x,handLeftPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,10);
            }
    }
    private void PunchSwarm()
    {
        Vector3 randomPosition;
        float handRadius = hand.GetComponent<CircleCollider2D>().radius;
        Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
        if(isWillNextBlowBeRightSide)
        {
            for(int i = 0;i < 3;i++)
            {
                randomPosition = new Vector3(handRightPoint.position.x + randomValue.x,handRightPoint.position.y + randomValue.y,0);
                HandCreate(new Vector3(Random.Range(randomPosition.x - 1,randomPosition.x + 1),Random.Range(randomPosition.y - 1,randomPosition.y + 1),0),10);
            }
        }
        else
        {
            for(int i = 0;i < 3;i++)
            {
                randomPosition = new Vector3(handLeftPoint.position.x + randomValue.x,handLeftPoint.position.y + randomValue.y,0);
                HandCreate(new Vector3(Random.Range(randomPosition.x - 1,randomPosition.x + 1),Random.Range(randomPosition.y - 1,randomPosition.y + 1),0),10);
            }
        }
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
        if(player.numberImprovents > 0 & player.attackSpeed < 1000)
        {
        player.numberImprovents--;
        player.attackSpeed += player.attackSpeedImprovementMultiplier * 1;
        timeAfterWhichBeNextBlow = timeAfterWhichBeNextBlowConst - (player.attackSpeed / 700f);
        if(timeAfterWhichBeNextBlowConst - (player.attackSpeed / 700f) < 0.01f)
        {
            timeAfterWhichBeNextBlow = 0.01f;
        }
        if(player.attackSpeed > 1000)
        {
            player.attackSpeed = 1000;
        }
        }
    }
    private IEnumerator FirstAbilityCoroutine()
    {
        float[] numbers = GameManager.instance.firstAbilityDelegate?.Invoke(this,true);
        isFirstAbilityRecovered = false;
        StartCoroutine(RestoringFirstAbilityCorutine(numbers[1]));
        yield return new WaitForSeconds(numbers[0]);
        GameManager.instance.firstAbilityDelegate?.Invoke(this,false);
    }
    private IEnumerator RestoringFirstAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            mainUIHandler.firstAbilityImage.fillAmount = (time / recoveryTime);
            yield return null;
        }
        isFirstAbilityRecovered = true;
    }
    private IEnumerator SecondAbilityCoroutine()
    {
        float[] numbers = GameManager.instance.secondAbilityDelegate?.Invoke(this,true);
        isSecondAbilityRecovered = false;
        StartCoroutine(RestoringSecondAbilityCorutine(numbers[1]));
        yield return new WaitForSeconds(numbers[0]);
        GameManager.instance.secondAbilityDelegate?.Invoke(this,false);
    }
    private IEnumerator RestoringSecondAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            mainUIHandler.secondAbilityImage.fillAmount = (time / recoveryTime);
            yield return null;
        }
        isSecondAbilityRecovered = true;
    }
    private IEnumerator ThirdAbilityCoroutine()
    {
        float[] numbers = GameManager.instance.thirdAbilityDelegate?.Invoke(this,true);
        isThirdAbilityRecovered = false;
        StartCoroutine(RestoringThirdAbilityCorutine(numbers[1]));
        yield return new WaitForSeconds(numbers[0]);
        GameManager.instance.thirdAbilityDelegate?.Invoke(this,false);
    }
    private IEnumerator RestoringThirdAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            mainUIHandler.thirdAbilityImage.fillAmount = (time / recoveryTime);
            yield return null;
        }
        isThirdAbilityRecovered = true;
    }
    private IEnumerator FourthAbilityCoroutine()
    {
        float[] numbers = GameManager.instance.fourthAbilityDelegate?.Invoke(this,true);
        isFourthAbilityRecovered = false;
        StartCoroutine(RestoringFourthAbilityCorutine(numbers[1]));
        yield return new WaitForSeconds(numbers[0]);
        GameManager.instance.fourthAbilityDelegate?.Invoke(this,false);
    }
    private IEnumerator RestoringFourthAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            mainUIHandler.fourthAbilityImage.fillAmount = (time / recoveryTime);
            yield return null;
        }
        isFourthAbilityRecovered = true;
    }
    public void FirstAbility()
    {
        if(isFirstAbilityRecovered)
        {
            StartCoroutine(FirstAbilityCoroutine());
        }
    }
    public void SecondAbility()
    {
        if(isSecondAbilityRecovered)
        {
            StartCoroutine(SecondAbilityCoroutine());
        }
    }
    public void ThirdAbility()
    {
        if(isThirdAbilityRecovered)
        {
            StartCoroutine(ThirdAbilityCoroutine());
        }
    }
    public void FourthAbility()
    {
        if(isFourthAbilityRecovered)
        {
            StartCoroutine(FourthAbilityCoroutine());
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.deltaTime);
        if(isActiveDash)
        {
            rb.MovePosition(rb.position + ((Vector2)transform.up) * speed * Time.deltaTime);
        }
    }
}
