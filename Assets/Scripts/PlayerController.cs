using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CircleCollider2D circleCollider2D;
    protected Vector2 moveInput;
    [HideInInspector]
    public float speed = 5f;
    private float rotZ;
    [SerializeField]
    public GameObject hand;
    public GameObject shield;
    [HideInInspector]
    public Transform handRightPoint;
    [HideInInspector]
    public Transform handLeftPoint;
    [HideInInspector]
    public Player player;
    protected PlayerUIHundler playerUIHundler;
    private bool isWillNextBlowBeRightSide;
    private const float timeAfterWhichBeNextBlowConst = 0.5f;
    public float timeAfterWhichBeNextBlow = timeAfterWhichBeNextBlowConst;
    [HideInInspector]
    public bool isActivePunchSwarm;
    [HideInInspector]
    public bool isActiveDash;
    public float flightDistance = 2.5f;
    private bool isFirstAbilityRecovered = true;
    private bool isSecondAbilityRecovered = true;
    private bool isThirdAbilityRecovered = true;
    private bool isFourthAbilityRecovered = true;
    protected Timer timer;
    void Start()
    {
        handRightPoint = transform.GetChild(0);
        handLeftPoint = transform.GetChild(1);
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerUIHundler = player._playerUIHundler;
        timer = GetComponent<Timer>();
    }

    public void Hit()
    {
        if(timer.time <= 0)
        {
        timer.time = timeAfterWhichBeNextBlow;
        if(isActivePunchSwarm)
        {
            PunchSwarm();
        }
        else
        {
            UsualHit();
        }
            StopCoroutine(player._coroutineStaminaHundler);
            player._coroutineStaminaHundler = StartCoroutine(player.RegenerationStamina());
        }
        
    }
    public void HandCreate(Vector3 randomPosition,ref bool isWillNextBlowBeRightSide)
    {
        GameObject currentHandGameObject;
        Hand currentHand;
        player.Stamina--;
        Quaternion direction;
        if(isWillNextBlowBeRightSide)
        {
            currentHandGameObject = Instantiate(hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z - 20 - Random.Range(0,30)));
            direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z + Random.Range(5f,20)));
        }
        else
        {
            currentHandGameObject = Instantiate(hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z + 20 + Random.Range(0,30)));
            direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z - Random.Range(5f,20)));
        }
        currentHand = currentHandGameObject.GetComponent<Hand>();
        currentHand._isRightHand = isWillNextBlowBeRightSide;
        currentHand._direction = direction;
        isWillNextBlowBeRightSide = !isWillNextBlowBeRightSide;
        currentHand._BlowHand += player.BlowHit;
        currentHand._killEnemy += player.KillEnemy;
        currentHand._damage = player.AttackDamage;
        currentHand._flightDistance = flightDistance;
    }
    private void UsualHit()
    {
        Vector3 randomPosition;
        float handRadius = hand.GetComponent<CircleCollider2D>().radius;
        Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
            if(isWillNextBlowBeRightSide)
            {
                randomPosition = new Vector3(handRightPoint.position.x + randomValue.x,handRightPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,ref isWillNextBlowBeRightSide);
            }
            else
            {
                randomPosition = new Vector3(handLeftPoint.position.x + randomValue.x,handLeftPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,ref isWillNextBlowBeRightSide);
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
                HandCreate(new Vector3(Random.Range(randomPosition.x - 1,randomPosition.x + 1),Random.Range(randomPosition.y - 1,randomPosition.y + 1),0),ref isWillNextBlowBeRightSide);
            }
        }
        else
        {
            for(int i = 0;i < 3;i++)
            {
                randomPosition = new Vector3(handLeftPoint.position.x + randomValue.x,handLeftPoint.position.y + randomValue.y,0);
                HandCreate(new Vector3(Random.Range(randomPosition.x - 1,randomPosition.x + 1),Random.Range(randomPosition.y - 1,randomPosition.y + 1),0),ref isWillNextBlowBeRightSide);
            }
        }
    }
    public void ImproveAttackDamage()
    {
        if(player.NumberImprovents > 0)
        {
            player.NumberImprovents--;
            player.AttackDamage += (float)System.Math.Round(player._multiplierAttackDamageImprovement * 1,1);
        }
    }
    public void ImproveHealth()
    {
        if(player.NumberImprovents > 0)
        {
        player.NumberImprovents--;
        player.FullHealth += (float)System.Math.Round(player._healthImprovementMultiplier * 1,1);
        }
    }
    public void ImproveStamina()
    {
        if(player.NumberImprovents > 0)
        {
        player.NumberImprovents--;
        player.FullStamina += (float)System.Math.Round(player._staminaImprovementMultiplier * 1,1);
        }
    }
    public void ImproveCriticalDamage()
    { 
        if(player.NumberImprovents > 0)
        {
        player.NumberImprovents--;
        player.CriticalDamage += (float)System.Math.Round(player._criticalDamageImprovementMultiplier * 1,1);
        }
    }
    public void ImproveAttackSpeed()
    {
        if(player.NumberImprovents > 0 & player.AttackSpeed < 350)
        {
        player.NumberImprovents--;
        player.AttackSpeed += (float)System.Math.Round(player._attackSpeedImprovementMultiplier * 1);
        timeAfterWhichBeNextBlow = timeAfterWhichBeNextBlowConst / Mathf.Pow(1.00915f,player.AttackSpeed);
        if(timeAfterWhichBeNextBlow < 0.02f)
        {
            timeAfterWhichBeNextBlow = 0.02f;
        }
        }
    }
    private IEnumerator FirstAbilityCoroutine()
    {
        Abilities.Closures closures = GameManager._instance._firstAbilityDelegate?.Invoke(this,true);
        float[] numbers = closures?.Invoke();
        isFirstAbilityRecovered = false;
        float timeActive = numbers[0];
        StartCoroutine(RestoringFirstAbilityCorutine(numbers[1]));
        while(timeActive > 0)
        {
            timeActive -= Time.deltaTime;
            closures?.Invoke();
            yield return null;
        }
        GameManager._instance._firstAbilityDelegate?.Invoke(this,false)?.Invoke();
    }
    private IEnumerator RestoringFirstAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            playerUIHundler.FirstAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        isFirstAbilityRecovered = true;
    }
    private IEnumerator SecondAbilityCoroutine()
    {
        Abilities.Closures closures = GameManager._instance._secondAbilityDelegate?.Invoke(this,true);
        float[] numbers = closures?.Invoke();
        isFirstAbilityRecovered = false;
        float timeActive = numbers[0];
        StartCoroutine(RestoringSecondAbilityCorutine(numbers[1]));
        while(timeActive > 0)
        {
            timeActive -= Time.deltaTime;
            closures?.Invoke();
            yield return null;
        }
        GameManager._instance._secondAbilityDelegate?.Invoke(this,false)?.Invoke();
    }
    private IEnumerator RestoringSecondAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            playerUIHundler.SecondAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        isSecondAbilityRecovered = true;
    }
    private IEnumerator ThirdAbilityCoroutine()
    {
        Abilities.Closures closures = GameManager._instance._thirdAbilityDelegate?.Invoke(this,true);
        float[] numbers = closures?.Invoke();
        isFirstAbilityRecovered = false;
        float timeActive = numbers[0];
        StartCoroutine(RestoringThirdAbilityCorutine(numbers[1]));
        while(timeActive > 0)
        {
            timeActive -= Time.deltaTime;
            closures?.Invoke();
            yield return null;
        }
        GameManager._instance._thirdAbilityDelegate?.Invoke(this,false)?.Invoke();
    }
    private IEnumerator RestoringThirdAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            playerUIHundler.ThirdAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        isThirdAbilityRecovered = true;
    }
    private IEnumerator FourthAbilityCoroutine()
    {
        Abilities.Closures closures = GameManager._instance._fourthAbilityDelegate?.Invoke(this,true);
        float[] numbers = closures?.Invoke();
        isFirstAbilityRecovered = false;
        float timeActive = numbers[0];
        StartCoroutine(RestoringFourthAbilityCorutine(numbers[1]));
        while(timeActive > 0)
        {
            timeActive -= Time.deltaTime;
            Debug.Log("Первая способность была активированна");
            closures?.Invoke();
            yield return null;
        }
        GameManager._instance._fourthAbilityDelegate?.Invoke(this,false)?.Invoke();
    }
    private IEnumerator RestoringFourthAbilityCorutine(float recoveryTime)
    {
        float recoveryTimeAbility = recoveryTime;
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            playerUIHundler.FourthAbilityImageFillAmount(time / recoveryTime);
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
