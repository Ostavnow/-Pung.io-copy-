using System;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [HideInInspector]
    public float _damage = 1f;
    [HideInInspector]
    private float _speed = 5f;
    [HideInInspector]
    public Quaternion _direction;
    [HideInInspector]
    private Vector3 _initialPosition;
    [HideInInspector]
    public bool _isRightHand;
    public int _criticalDamageChance;
    public Action _BlowHand;
    public Action _killEnemy;
    [SerializeField]
    private GameObject _coinPrefab;
    [SerializeField]
    private GameObject _textPrefab;
    private MainUIHandler _mainUIHandler;
    public float _flightDistance = 2.5f;
    private void Start() {
        _initialPosition = transform.position;
        _mainUIHandler = FindObjectOfType<MainUIHandler>();
    }
    void Update()
    {

        transform.position += transform.up * _speed * Time.deltaTime;
        if(_isRightHand)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,_direction,_speed * 3 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,_direction,_speed * 3 * Time.deltaTime);
        }
        if(Vector3.Distance(_initialPosition,transform.position) > _flightDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("punching bag"))
        {
            int randomRangeCriticalDamage = UnityEngine.Random.Range(0,1010);
            int randomRangeDamage = (int) UnityEngine.Random.Range(1f,_damage);
            int randomValue = UnityEngine.Random.Range(0,100);
            if(randomRangeCriticalDamage <= 10)
            {
                TMP_Text text = Instantiate(_textPrefab,transform.position,Quaternion.identity,_mainUIHandler._canvasTransform).GetComponent<TMP_Text>();
                text.text = _damage.ToString();
                text.color = new Color(1,1,0,1);
            }
            else
            {
                Instantiate(_textPrefab,transform.position,Quaternion.identity,_mainUIHandler._canvasTransform).GetComponent<TMP_Text>().text = randomRangeDamage.ToString();
            }
            if(randomValue < 1)
            {
                Instantiate(_coinPrefab,transform.position,_coinPrefab.transform.rotation);
            }
            _BlowHand?.Invoke();
            Destroy(gameObject);
        }  
    }
}
