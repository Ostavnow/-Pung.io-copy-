using UnityEngine;

public class BotCanvas : MonoBehaviour
{
    private Transform canvasTransform;
    private void Start()
    {
        canvasTransform = transform.GetChild(3);
    }
    private void LateUpdate() 
    {
        canvasTransform.eulerAngles = new Vector3(0,0,-transform.rotation.z);
    }
}