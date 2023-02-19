using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string nickname;
    private float p_health = 200f;
    private float health
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
    private float XPPercent;
    private float p_attackDamage = 1f;
    private float upgradedAttackDamage;
    public float attackDamage
    {
        get{return p_attackDamage;}
        set
        {
            upgradedAttackDamage += value - p_attackDamage;
            p_attackDamage = value;
            if(upgradedAttackDamage == ((int)upgradedAttackDamage))
            {
                mainUIHandler.attackDamageText.text = upgradedAttackDamage.ToString() + ".0";
            }
            else
            {
                mainUIHandler.attackDamageText.text = upgradedAttackDamage.ToString();  
            }
        }
    }
    private float p_criticalDamage = 1f;
    private float upgradedCriticalDamage;
    public float criticalDamage
    {
        get{return p_criticalDamage;}
        set
        {
            upgradedCriticalDamage += value - p_criticalDamage;
            p_criticalDamage = value;
            if(upgradedCriticalDamage == ((int)upgradedCriticalDamage))
            {
                mainUIHandler.criticalDamageText.text = upgradedCriticalDamage.ToString() + ".0";
            }
            else
            {
                mainUIHandler.criticalDamageText.text = upgradedCriticalDamage.ToString();  
            }
        }
    }
    private float p_attackSpeed = 1f;
    private float upgradedAttackSpeed;
    public float attackSpeed
    {
        get{return p_attackSpeed;}
        set
        {
            upgradedAttackSpeed += value - p_attackSpeed;
            p_attackSpeed = value;
            if(value == ((int)value))
            {
                mainUIHandler.attackSpeedText.text = upgradedAttackSpeed.ToString() + ".0";
            }
            else
            {
                mainUIHandler.attackSpeedText.text = upgradedAttackSpeed.ToString();  
            }
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
            HealthStripeUpdate();
            p_fullHealth = value;
            health = health;
            if(upgradedHealth == ((int)upgradedHealth))
            {
                mainUIHandler.healthText.text = upgradedHealth.ToString() + ".0";
            }
            else
            {
                mainUIHandler.healthText.text = upgradedHealth.ToString();  
            }
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
            StaminaStripeUpdate();
            p_fullStamina = value;
            stamina = stamina;
            if(upgradedStamina == ((int)upgradedStamina))
            {
                mainUIHandler.staminaText.text = upgradedStamina.ToString() + ".0";
            }
            else
            {
                mainUIHandler.staminaText.text = upgradedStamina.ToString();  
            }
        }
    }
    public float multiplierAttackDamageImprovement = 1f;
    public float healthImprovementMultiplier = 1f;
    public float staminaImprovementMultiplier = 1f;
    public float criticalDamageImprovementMultiplier = 1f;
    public float attackSpeedImprovementMultiplier = 1f;
    private float nextLevel = 100f;
    private float p_numberImprovents = 20f;
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
    private bool isStaminaRegenerationRunning;
    private bool isHealthRegenerationRunning;
    private bool isCorutinaComboRunning;
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
        XPPercent += (100 / (nextLevel / 100));
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
    private IEnumerator RegenerationHealth()
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
}
