using System.Collections;
using UnityEngine;

public abstract class Player : MonoBehaviour, IPlayerInfo
{
    public string _nickname;
    protected float _health = 200f;
    public PlayerUIHundler _playerUIHundler;
    protected float Health
    {
        get{return _health;}
        set
        {
            _health = value;
            if(_health < FullHealth & !(_isHealthRegenerationRunning))
            {
                _coroutineHealthHundler = StartCoroutine(RegenerationHealth());
            }
        }
    }

    private float _stamina = 100f;
    public float Stamina
    {
        get{return _stamina;}
        set
        {
            _stamina = value;
            if(_stamina < FullStamina & !(_isStaminaRegenerationRunning))
            {
                _coroutineStaminaHundler = StartCoroutine(RegenerationStamina());
            }
        }
    }
    public int money;
    [SerializeField]
    private int _level;
    protected int Level
    {
        get{return _level;}
        set
        { 
            _level = value;
            _playerUIHundler.LevelTextSet(_level);
        }
    }
    protected float _XPPercent;
    private float _attackDamage = 1;
    public float AttackDamage
    {
        get{return _attackDamage;}
        set
        {
            _attackDamage = value;
            System.Math.Round(_attackDamage,1);
            _playerUIHundler.AttackDamageTextSet(_attackDamage);
        }
    }
    private float _criticalDamage = 1;
    public float CriticalDamage
    {
        get{return _criticalDamage;}
        set
        {
            System.Math.Round(_criticalDamage,1);
            _criticalDamage = value;
            _playerUIHundler.CriticalDamageTextSet(_criticalDamage);
        }
    }
    private float _attackSpeed = 1;
    public float AttackSpeed
    {
        get{return _attackSpeed;}
        set
        {
            _attackSpeed = value;
            System.Math.Round(_attackSpeed,1);
            _playerUIHundler.AttackSpeedTextSet(_attackSpeed);
        }
    }
    private float _fullHealth = 200f;
    public float _updateFullHealth;
    public float FullHealth
    {
        get{return _fullHealth;}
        set
        {
            _updateFullHealth += (float)System.Math.Round(value - _fullHealth,1);
            _fullHealth = value;
            Health = Health;
            _playerUIHundler.HealthTextSet(Health);
        }
    }      
    private float _fullStamina = 100f;
    protected float _updateFullStamina;
    public float FullStamina
    {
        get{return _fullStamina;}
        set
        {
            _updateFullStamina += (float)System.Math.Round(value - _fullStamina,1);
            _fullStamina = value;
            Stamina = Stamina;
            _playerUIHundler.StaminaTextSet(Stamina);
        }
    }
    public float _multiplierAttackDamageImprovement = 1f;
    public float _healthImprovementMultiplier = 1f;
    public float _staminaImprovementMultiplier = 1f;
    public float _criticalDamageImprovementMultiplier = 1f;
    public float _attackSpeedImprovementMultiplier = 1f;
    public float _experienceImprovementMultiplier = 1f;
    protected float _nextLevel = 100f;
    private int _numberImprovents = 20;
    public int NumberImprovents
    {
        get{return _numberImprovents;}
        set
        {
            _numberImprovents = value;
            _playerUIHundler.NumberImproventsTextSet(_numberImprovents);
            if(_numberImprovents > 0)
            {
                _playerUIHundler.SkilsPanelSetActive(true);
            }
            else
            {
                _playerUIHundler.SkilsPanelSetActive(false);
            }
        }
    }
    protected bool _isStaminaRegenerationRunning;
    protected bool _isHealthRegenerationRunning;
    public bool _isCorutinaComboRunning;
    public Coroutine _coroutineStaminaHundler;
    public Coroutine _coroutineHealthHundler;
    public Coroutine _coroutineComboHundler;
    private int _comboValue;
    public int ComboValue
    {
        get{return _comboValue;}
        set
        {
            _comboValue = value;
                if(_comboValue != 0)
                {
                    if(!(_isCorutinaComboRunning))
                    {
                        _coroutineComboHundler = StartCoroutine(_playerUIHundler.ComboUIUpdate(this));
                    }
                    else
                    {
                        StopCoroutine(_coroutineComboHundler);
                        _coroutineComboHundler = StartCoroutine(_playerUIHundler.ComboUIUpdate(this));
                    }
                }
                else
                {
                    _playerUIHundler.ComboTextSetActive(false);
                }
        }
    }
    private Animator comboAnimator;
    private void Start()
    {
        _playerUIHundler = GetComponent<PlayerUIHundler>();
    }
    public abstract void BlowHit();
    public abstract void KillEnemy();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hand"))
        {
            Hand hand = other.GetComponent<Hand>();
            Health -= hand._damage;
            Destroy(hand.gameObject);
            StopCoroutine(_coroutineHealthHundler);
            _coroutineHealthHundler = StartCoroutine(RegenerationHealth());
            if(Health <= 0)
            {
                hand._killEnemy?.Invoke();
                Destroy(gameObject);
            }
        }
    }
    public void AddingExperience()
    {
        _XPPercent += _experienceImprovementMultiplier * (100 / (_nextLevel / 100));
        _playerUIHundler.XPStripeFillAmount(_XPPercent / 100);
        if(_XPPercent >= 100)
        {
            _level++;
            NumberImprovents++;
            _XPPercent = 0;
            _nextLevel += 500;
        }
    }
    
    protected IEnumerator RegenerationHealth()
    {
        _isHealthRegenerationRunning = true;
        _playerUIHundler.HealthStripeUpdate(Health,FullHealth);
        yield return new WaitForSeconds(3);
        while(Health < FullHealth)
        {
            Health += (FullHealth / 10f / 5f) * Time.deltaTime;
                _playerUIHundler.HealthStripeUpdate(Health,FullHealth);
            yield return null;
        }
        _isHealthRegenerationRunning = false;
    }
    public IEnumerator RegenerationStamina()
    {
        _isStaminaRegenerationRunning = true;
        _playerUIHundler.StaminaStripeUpdate(Stamina,FullStamina);
        yield return new WaitForSeconds(3);
        while(Stamina < FullStamina)
        {
            Stamina += (FullStamina / 5f) * Time.deltaTime;
            _playerUIHundler.StaminaStripeUpdate(Stamina,FullStamina);
            yield return null;
        }
        _isStaminaRegenerationRunning = false;
    }
    public float GetPowerScore() => _level * _healthImprovementMultiplier * _staminaImprovementMultiplier * _attackSpeedImprovementMultiplier
                                    * _criticalDamageImprovementMultiplier * _multiplierAttackDamageImprovement;
}