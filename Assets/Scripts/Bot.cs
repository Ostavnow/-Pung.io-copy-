using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private BotLogic typeBot;
    public int level;
    public string nickname;
    private float p_health = 200f;
    private float health
    {
        get{return p_health;}
        set
        {
            p_health = value;
            if(p_health < fullHealth & !(isHealthRegenerationRunning))
            {
                coroutineHealthHundler = StartCoroutine(RegenerationHealth());
            }
        }
    }

    private float p_stamina = 100f;
    public float stamina
    {
        get{return p_stamina;}
        set
        {
            p_stamina = value;
            if(p_stamina < fullStamina & !(isStaminaRegenerationRunning))
            {
                coroutineStaminaHundler = StartCoroutine(RegenerationStamina());
            }
        }
    }
    public int money;
    private float XPPercent;
    private float p_attackDamage = 1;
    private float upgradedAttackDamage;
    public float attackDamage
    {
        get{return p_attackDamage;}
        set
        {
            upgradedAttackDamage += value - p_attackDamage;
            System.Math.Round(upgradedAttackDamage,1);
            p_attackDamage = value;
            if(upgradedAttackDamage == ((int)upgradedAttackDamage))
            {
            }
            else
            {
                upgradedAttackDamage *= 10;
                upgradedAttackDamage = (int)upgradedAttackDamage;
                upgradedAttackDamage = upgradedAttackDamage / 10;
            }
        }
    }
    private float p_criticalDamage = 1;
    private float upgradedCriticalDamage;
    public float criticalDamage
    {
        get{return p_criticalDamage;}
        set
        {
            upgradedCriticalDamage += value - p_criticalDamage;
            System.Math.Round(upgradedCriticalDamage,1);
            p_criticalDamage = value;
            
        }
    }
    private float p_attackSpeed = 1;
    private float upgradedAttackSpeed;
    public float attackSpeed
    {
        get{return p_attackSpeed;}
        set
        {
            upgradedAttackSpeed += value - p_attackSpeed;
            System.Math.Round(upgradedAttackSpeed,1);
            p_attackSpeed = value;
            
        }
    }
    private float p_fullHealth = 200f;
    private float upgradedHealth;
    public float fullHealth
    {
        get{return p_fullHealth;}
        set
        {
            upgradedHealth += value - fullHealth;
            System.Math.Round(upgradedHealth,1);
            p_fullHealth = value;
            health = health;
        }
    }      
    private float p_fullStamina = 100f;
    private float upgradedStamina;
    public float fullStamina
    {
        get{return p_fullStamina;}
        set
        {
            upgradedStamina += value - p_fullStamina;
            System.Math.Round(upgradedStamina,1);
            p_fullStamina = value;
            stamina = stamina;
        }
    }
    public float multiplierAttackDamageImprovement = 1f;
    public float healthImprovementMultiplier = 1f;
    public float staminaImprovementMultiplier = 1f;
    public float criticalDamageImprovementMultiplier = 1f;
    public float attackSpeedImprovementMultiplier = 1f;
    private float nextLevel = 100f;
    public float experienceImprovementMultiplier = 1f;
    public float numberImprovents = 20f;
    private bool isStaminaRegenerationRunning;
    private bool isHealthRegenerationRunning;
    private bool isCorutinaComboRunning;
    public Coroutine coroutineStaminaHundler;
    public Coroutine coroutineHealthHundler;
    public Coroutine coroutineComboHundler;
    [SerializeField]
    private float timeAfterWhichBeNextBlow = 5f;
    private const float timeAfterWhichBeNextBlowConst = 0.5f;
    public Abilities.AbilityDelegate firstAbilityDelegate;
    public Abilities.AbilityDelegate secondAbilityDelegate;
    public Abilities.AbilityDelegate thirdAbilityDelegate;
    public Abilities.AbilityDelegate fourthAbilityDelegate;

    void Start()
    {
        
    }
    private IEnumerator RegenerationHealth()
    {
        isHealthRegenerationRunning = true;
        yield return new WaitForSeconds(3);
        while(health < fullHealth)
        {
            health += (fullHealth / 10f / 5f) * Time.deltaTime;
            yield return null;
        }
        isHealthRegenerationRunning = false;
    }
    public IEnumerator RegenerationStamina()
    {
        isStaminaRegenerationRunning = true;
        yield return new WaitForSeconds(3);
        while(stamina < fullStamina)
        {
            stamina += (fullStamina / 5f) * Time.deltaTime;
            yield return null;
        }
        isStaminaRegenerationRunning = false;
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
    IEnumerator UpdateBotLogic()
    {
        while(true)
        {
            if(typeBot.botStates == BotLogic.BotStates.Pumping)
                typeBot.Pumping();
            if(typeBot.botStates == BotLogic.BotStates.Attack)
                typeBot.Attack();
            if(typeBot.botStates == BotLogic.BotStates.Flight)
                typeBot.Flight();
            if(typeBot.botStates == BotLogic.BotStates.Despair)
                typeBot.Despair();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
