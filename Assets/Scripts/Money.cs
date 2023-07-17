using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float _power = 1;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector3(Random.Range(-0.5f,0.5f) * _power,Random.Range(0f,0.5f) * _power,0),ForceMode2D.Impulse);
        Destroy(gameObject,5f);
    }
}
