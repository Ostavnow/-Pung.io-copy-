using TMPro;
using UnityEngine;
namespace UI
{
    public class TextEffect : MonoBehaviour
    {
        TMP_Text _text;
        private Rigidbody2D _rb;
        private float _power = 2.5f;
        void Start()
        {
            _text = GetComponent<TMP_Text>();
            _rb = GetComponent<Rigidbody2D>();
            _rb.AddForce(new Vector3(Random.Range(-1f,1f) * _power,_power,0),ForceMode2D.Impulse);
            _text.fontSize = Random.Range(38,72);
            Destroy(gameObject,0.5f);
        }
        void Update()
        {
            
        }
    }
}
