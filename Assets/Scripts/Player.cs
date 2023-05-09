using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPlayerData
{
    public string nickname;
    private float p_health = 200f;
    protected float health
    {
        get{return p_health;}
        set
        {
            p_health = value;
            HealthStripeUpdate();
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
            StaminaStripeUpdate();
            if(p_stamina < fullStamina & !(isStaminaRegenerationRunning))
            {
                coroutineStaminaHundler = StartCoroutine(RegenerationStamina());
            }
        }
    }
    public int money;
    private int p_level;
    public int level
    {
        get{return p_level;}
        set
        {
            p_level = value;
            mainUIHandler.levelText.text = p_level.ToString();
        }
    }
    protected float XPPercent;
    private float p_attackDamage = 1;
    public float attackDamage
    {
        get{return p_attackDamage;}
        set
        {
            p_attackDamage = value;
            System.Math.Round(p_attackDamage,1);
            if(p_attackDamage == ((int)p_attackDamage))
            {
                mainUIHandler.attackDamageText.text = p_attackDamage.ToString() + ".0";
            }
            else
            {
                p_attackDamage *= 10;
                p_attackDamage = (int)p_attackDamage;
                p_attackDamage = p_attackDamage / 10;
                mainUIHandler.attackDamageText.text = p_attackDamage.ToString();  
            }
        }
    }
    private float p_criticalDamage = 1;
    public float criticalDamage
    {
        get{return p_criticalDamage;}
        set
        {
            System.Math.Round(p_criticalDamage,1);
            p_criticalDamage = value;
            if(p_criticalDamage == ((int)p_criticalDamage))
            {
                mainUIHandler.criticalDamageText.text = p_criticalDamage.ToString() + ".0";
            }
            else
            {
                mainUIHandler.criticalDamageText.text = p_criticalDamage.ToString();  
            }
        }
    }
    private float p_attackSpeed = 1;
    public float attackSpeed
    {
        get{return p_attackSpeed;}
        set
        {
            p_attackSpeed = value;
            System.Math.Round(p_attackSpeed,1);
            if(p_attackSpeed == ((int)p_attackSpeed))
            {
                mainUIHandler.attackSpeedText.text = p_attackSpeed.ToString() + ".0";
            }
            else
            {
                mainUIHandler.attackSpeedText.text = p_attackSpeed.ToString();  
            }
        }
    }
    private float p_fullHealth = 200f;
    protected float updateFullHealth;
    public float fullHealth
    {
        get{return p_fullHealth;}
        set
        {
            updateFullHealth += value - p_fullHealth;
            p_fullHealth = value;
            System.Math.Round(p_fullHealth,1);
            System.Math.Round(updateFullHealth,1);
            HealthStripeUpdate();
            health = health;
            if(updateFullHealth == ((int)updateFullHealth))
            {
                mainUIHandler.healthText.text = updateFullHealth.ToString() + ".0";
            }
            else
            {
                mainUIHandler.healthText.text = updateFullHealth.ToString();  
            }
        }
    }      
    private float p_fullStamina = 100f;
    protected float updateFullStamina;
    public float fullStamina
    {
        get{return p_fullStamina;}
        set
        {
            updateFullStamina += value - p_fullStamina;
            System.Math.Round(p_fullStamina,1);
            System.Math.Round(updateFullStamina,1);
            StaminaStripeUpdate();
            p_fullStamina = value;
            stamina = stamina;
            if(updateFullStamina == ((int)updateFullStamina))
            {
                mainUIHandler.staminaText.text = updateFullStamina.ToString() + ".0";
            }
            else
            {
                mainUIHandler.staminaText.text = updateFullStamina.ToString();  
            }
        }
    }
    public float multiplierAttackDamageImprovement = 1f;
    public float healthImprovementMultiplier = 1f;
    public float staminaImprovementMultiplier = 1f;
    public float criticalDamageImprovementMultiplier = 1f;
    public float attackSpeedImprovementMultiplier = 1f;
    protected float nextLevel = 100f;
    private float p_numberImprovents = 20f;
    public float experienceImprovementMultiplier = 1f;
    public float numberImprovents
    {
        get{return p_numberImprovents;}
        set
        {
            p_numberImprovents = value;
            mainUIHandler.numberImproventsText.text = p_numberImprovents.ToString();
            if(numberImprovents > 0)
            {
                skilsPanel.SetActive(true);
            }
            else
            {
                skilsPanel.SetActive(false);
            }
        }
    }
    protected bool isStaminaRegenerationRunning;
    protected bool isHealthRegenerationRunning;
    protected bool isCorutinaComboRunning;
    public Coroutine coroutineStaminaHundler;
    public Coroutine coroutineHealthHundler;
    public Coroutine coroutineComboHundler;
    private GameObject skilsPanel;
    private GameObject comboGameObject;
    private int p_comboValue;
    private MainUIHandler mainUIHandler;
    private int comboValue
    {
        get{return p_comboValue;}
        set
        {
            p_comboValue = value;
            if(p_comboValue != 0)
            {
                if(!(isCorutinaComboRunning))
                {
                   coroutineComboHundler = StartCoroutine(Combo());
                }
                else
                {
                    StopCoroutine(coroutineComboHundler);
                    coroutineComboHundler = StartCoroutine(Combo());
                }
            }
            else
            {
                comboGameObject.SetActive(false);
            }
        }
    }
    private Animator comboAnimator;
    void Start()
    {
        mainUIHandler = FindObjectOfType<MainUIHandler>();
        skilsPanel = GameObject.Find("Canvas").transform.GetChild(6).gameObject;
        comboGameObject = GameObject.Find("Canvas").transform.GetChild(10).gameObject;
        comboAnimator = comboGameObject.GetComponent<Animator>();
        mainUIHandler.moneyCounterTextGame.text = GameManager.instance.user.amountMoney.ToString();
        PlayerController playerController = GetComponent<PlayerController>();
        Skin skin = GameManager.DeserializeSkin(GameManager.instance.user.numberSelectedSkin);
        GetComponent<SpriteRenderer>().sprite = skin.spriteSkinBody;
        playerController.hand.GetComponent<SpriteRenderer>().sprite = skin.spriteSkinHand;
        multiplierAttackDamageImprovement = skin.attackDamage;
        healthImprovementMultiplier = skin.health;
        staminaImprovementMultiplier = skin.stamina;
        criticalDamageImprovementMultiplier = skin.criticalDamage;
        attackSpeedImprovementMultiplier = skin.attackSpeed;
        mainUIHandler.multiplierAttackDamageImprovementText.text = attackSpeedImprovementMultiplier.ToString();
        mainUIHandler.healthImprovementMultiplierText.text = healthImprovementMultiplier.ToString();
        mainUIHandler.staminaImprovementMultiplierText.text = staminaImprovementMultiplier.ToString();
        mainUIHandler.criticalDamageImprovementMultiplierText.text = criticalDamageImprovementMultiplier.ToString();
        mainUIHandler.attackSpeedImprovementMultiplierText.text = attackSpeedImprovementMultiplier.ToString();
    }
    void Update()
    {
        
    }
    public void BlowHit()
    {
        comboValue++;
        Level();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hand"))
        {
            Hand hand = GetComponent<Hand>();
            health -= hand.damage;
            if(health <= 0)
            {
                hand.killEnemy?.Invoke();
            }
        }
        else if(other.CompareTag("Test"))
        {
            comboValue = 0;
            health -= 10;
            StopCoroutine(coroutineHealthHundler);
            coroutineHealthHundler = StartCoroutine(RegenerationHealth());
        }
    }
    public void Level()
    {
        XPPercent += experienceImprovementMultiplier * (100 / (nextLevel / 100));
        mainUIHandler.XPStripe.fillAmount = XPPercent / 100;
        if(XPPercent >= 100)
        {
            level++;
            numberImprovents++;
            XPPercent = 0;
            nextLevel += 500;
        }
    }
    public void KillEnemy()
    {
        // Debug.Log("Мы убили игрока");
    }
    protected IEnumerator RegenerationHealth()
    {
        isHealthRegenerationRunning = true;
        mainUIHandler.healthStripe.fillAmount = health / fullHealth;
        yield return new WaitForSeconds(3);
        while(health < fullHealth)
        {
            health += (fullHealth / 10f / 5f) * Time.deltaTime;
            mainUIHandler.textOfHealthStrip.text = "HP" + ((int)health).ToString() + "/" + fullHealth;
            mainUIHandler.healthStripe.fillAmount = health / fullHealth;
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
            mainUIHandler.textOfStaminaStrip.text = "STM" + ((int)stamina).ToString() + "/" + fullStamina;
            mainUIHandler.staminaStripe.fillAmount = stamina / fullStamina;
            yield return null;
        }
        isStaminaRegenerationRunning = false;
    }
    public IEnumerator Combo()
    {
        isCorutinaComboRunning = true;
        mainUIHandler.comboText.text = "x" + comboValue.ToString();
        comboAnimator.Play("Combo");
        comboAnimator.SetTrigger("Combo");
        comboGameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        comboGameObject.SetActive(false);
        isCorutinaComboRunning = false;
    }
    private void HealthStripeUpdate()
    {
        mainUIHandler.textOfHealthStrip.text = "HP" + ((int)health).ToString() + "/" + fullHealth;
        mainUIHandler.healthStripe.fillAmount = health / fullHealth;
    }
    private void StaminaStripeUpdate()
    {
        mainUIHandler.textOfStaminaStrip.text = "STM" + ((int)stamina).ToString() + "/" + fullStamina;
        mainUIHandler.staminaStripe.fillAmount = stamina / fullStamina;
    }
    public float GetPowerScore() => level * healthImprovementMultiplier * staminaImprovementMultiplier * attackSpeedImprovementMultiplier
                                    * criticalDamageImprovementMultiplier * multiplierAttackDamageImprovement;
}
