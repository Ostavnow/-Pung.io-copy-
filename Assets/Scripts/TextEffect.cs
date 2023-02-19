using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    TMP_Text text;
    private Rigidbody2D rb;
    private float power = 2.5f;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(Random.Range(-1f,1f) * power,power,0),ForceMode2D.Impulse);
        text.fontSize = Random.Range(38,72);
        Destroy(gameObject,0.5f);
    }
    void Update()
    {
        
    }
}
