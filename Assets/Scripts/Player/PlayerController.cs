using System.Collections;
using UnityEngine;
using UI;
using System;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D _rb;
    protected CircleCollider2D _circleCollider2D;
    protected Vector2 _moveInput;
    [HideInInspector]
    public float _speed = 5f;
    private float _rotZ;
    [SerializeField]
    public GameObject _hand;
    public GameObject _shield;
    [HideInInspector]
    public Transform _handRightPoint;
    [HideInInspector]
    public Transform _handLeftPoint;
    [HideInInspector]
    public Player _player;
    protected PlayerUIHundler _playerUIHundler;
    public bool _isWillNextBlowBeRightSide;
    public event Action _hitEvent;
    [HideInInspector]
    public bool _isActivePunchSwarm;
    [HideInInspector]
    public bool _isActiveDash;
    public float _flightDistance = 2.5f;
    private bool _isFirstAbilityRecovered = true;
    private bool _isSecondAbilityRecovered = true;
    private bool _isThirdAbilityRecovered = true;
    private bool _isFourthAbilityRecovered = true;
    public Ability _firstAbility;
    public Ability _secondAbility;
    public Ability _thirdAbility;
    public Ability _fourthAbility;
    protected Timer _timer;
    public const float _timeAfterWhichBeNextBlowConst = 0.5f;
    public float _timeAfterWhichBeNextBlow = _timeAfterWhichBeNextBlowConst;
    void Start()
    {
        User _user = FindObjectOfType<DataManager>()._user;
        _firstAbility = typeof(AbilityDush); 
        _handRightPoint = transform.GetChild(0);
        _handLeftPoint = transform.GetChild(1);
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _playerUIHundler = _player._playerUIHundler;
        _timer = GetComponent<Timer>();
        _hitEvent = UsualHit;
    }

    public void Hit()
    {
        if(_timer.time <= 0)
        {
        _timer.time = _timeAfterWhichBeNextBlow;
            _hitEvent?.Invoke();
            StopCoroutine(_player._coroutineStaminaHundler);
            _player._coroutineStaminaHundler = StartCoroutine(_player.RegenerationStamina());
        }
        
    }
    public void HandCreate(Vector3 randomPosition,ref bool _isWillNextBlowBeRightSide)
    {
        GameObject currentHandGameObject;
        Hand currentHand;
        _player.Stamina--;
        Quaternion direction;
        if(_isWillNextBlowBeRightSide)
        {
            currentHandGameObject = Instantiate(_hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z - 20 - Random.Range(0,30)));
            direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z + Random.Range(5f,20)));
        }
        else
        {
            currentHandGameObject = Instantiate(_hand,randomPosition,Quaternion.Euler(0f,0f,transform.eulerAngles.z + 20 + Random.Range(0,30)));
            direction = Quaternion.Euler(new Vector3(0f,0f,transform.eulerAngles.z - Random.Range(5f,20)));
        }
        currentHand = currentHandGameObject.GetComponent<Hand>();
        currentHand._isRightHand = _isWillNextBlowBeRightSide;
        currentHand._direction = direction;
        _isWillNextBlowBeRightSide = !_isWillNextBlowBeRightSide;
        currentHand._BlowHand += _player.BlowHit;
        currentHand._killEnemy += _player.KillEnemy;
        currentHand._damage = _player.AttackDamage;
        currentHand._flightDistance = _flightDistance;
    }
    public void UsualHit()
    {
        Vector3 randomPosition;
        float handRadius = _hand.GetComponent<CircleCollider2D>().radius;
        Vector3 randomValue = new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f));
            if(_isWillNextBlowBeRightSide)
            {
                randomPosition = new Vector3(_handRightPoint.position.x + randomValue.x,_handRightPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,ref _isWillNextBlowBeRightSide);
            }
            else
            {
                randomPosition = new Vector3(_handLeftPoint.position.x + randomValue.x,_handLeftPoint.position.y + randomValue.y,0);
                HandCreate(randomPosition,ref _isWillNextBlowBeRightSide);
            }
    }

    public void FirstAbilityCoroutine()
    {
        if(_isFirstAbilityRecovered)
        {
            _isFirstAbilityRecovered = false;
            _firstAbility.AbilityActivation();
            StartCoroutine(RestoringFirstAbilityCorutine(_firstAbility._recoveryTime));
        }
    }
    private IEnumerator RestoringFirstAbilityCorutine(float recoveryTime)
    {
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            _playerUIHundler.FirstAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        _isFirstAbilityRecovered = true;
    }
    public void SecondAbilityCoroutine()
    {
        if(_isSecondAbilityRecovered)
        {    
            _isSecondAbilityRecovered = false;
            _secondAbility.AbilityActivation();
            StartCoroutine(RestoringSecondAbilityCorutine(_secondAbility._recoveryTime));
        }
    }
    private IEnumerator RestoringSecondAbilityCorutine(float recoveryTime)
    {
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            _playerUIHundler.SecondAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        _isSecondAbilityRecovered = true;
    }
    public void ThirdAbilityCoroutine()
    {
        if(_isThirdAbilityRecovered)
        {
            _isThirdAbilityRecovered = false;
            _thirdAbility.AbilityActivation();
            StartCoroutine(RestoringThirdAbilityCorutine(_thirdAbility._recoveryTime));
        }
    }
    private IEnumerator RestoringThirdAbilityCorutine(float recoveryTime)
    {
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            _playerUIHundler.ThirdAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        _isThirdAbilityRecovered = true;
    }
    public void FourthAbilityCoroutine()
    {
        if(_isFourthAbilityRecovered)
        {
            _isFourthAbilityRecovered = false;
            _fourthAbility.AbilityActivation();
            StartCoroutine(RestoringFourthAbilityCorutine(_fourthAbility._recoveryTime));
        }
    }
    private IEnumerator RestoringFourthAbilityCorutine(float recoveryTime)
    {
        float time = 0;
        while(time < recoveryTime)
        {
            time += Time.deltaTime;
            _playerUIHundler.FourthAbilityImageFillAmount(time / recoveryTime);
            yield return null;
        }
        _isFourthAbilityRecovered = true;
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveInput * _speed * Time.deltaTime);
        if(_isActiveDash)
        {
            _rb.MovePosition(_rb.position + ((Vector2)transform.up) * _speed * Time.deltaTime);
        }
    }
}
