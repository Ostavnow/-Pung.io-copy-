using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private Rigidbody2D rb;
    public float power = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(Random.Range(-0.5f,0.5f) * power,Random.Range(0f,0.5f) * power,0),ForceMode2D.Impulse);
        Destroy(gameObject,5f);
    }
}
