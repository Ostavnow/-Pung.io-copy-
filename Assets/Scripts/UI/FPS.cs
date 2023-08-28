using UnityEngine;
using TMPro;
namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class FPS : MonoBehaviour
    {
        TMP_Text text;
        void Start()
        {
            text = GetComponent<TMP_Text>();
        }
        void Update()
        {
            text.text = "fps: " + (1f / Time.deltaTime).ToString();
        }
    }
}
