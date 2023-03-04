using System;
using TMPro;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Quaternion direction;
    [HideInInspector]
    public Vector3 initialPosition;
    [HideInInspector]
    public bool isRightHand;
    public int criticalDamageChance;
    public Action BlowHand;
    public Action killEnemy;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject textPrefab;
    private MainUIHandler mainUIHandler;
    private void Start() {
        mainUIHandler = FindObjectOfType<MainUIHandler>();
    }
    void Update()
    {

        transform.position += transform.up * speed * Time.deltaTime;
        if(isRightHand)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,direction,speed * 3 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,direction,speed * 3 * Time.deltaTime);
        }
        if(Vector3.Distance(initialPosition,transform.position) > 2.5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("punching bag"))
        {
            int randomRangeCriticalDamage = UnityEngine.Random.Range(0,1010);
            int randomRangeDamage = (int) UnityEngine.Random.Range(1f,damage);
            int randomValue = UnityEngine.Random.Range(0,100);
            if(randomRangeCriticalDamage <= 10)
            {
                TMP_Text text = Instantiate(textPrefab,transform.position,Quaternion.identity,mainUIHandler.canvasTransform).GetComponent<TMP_Text>();
                text.text = damage.ToString();
                text.color = new Color(1,1,0,1);
            }
            else
            {
                Instantiate(textPrefab,transform.position,Quaternion.identity,mainUIHandler.canvasTransform).GetComponent<TMP_Text>().text = randomRangeDamage.ToString();
            }
            if(randomValue < 1)
            {
                GameManager.instance.user.amountMoney++;
                mainUIHandler.moneyCounterTextGame.text = GameManager.instance.user.amountMoney.ToString();
                GameManager.instance.SaveUserProgress();
                Instantiate(coinPrefab,transform.position,coinPrefab.transform.rotation);
            }
            BlowHand?.Invoke();
            Destroy(gameObject);
        }        
    }
}
