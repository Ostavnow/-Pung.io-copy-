using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AbilitySelectUI : MonoBehaviour , IDragHandler , IPointerDownHandler , IPointerUpHandler
{
    private static GameObject currentAbility;
    [SerializeField] GraphicRaycaster raycaster;
    [SerializeField] EventSystem eventSystem;
    private PointerEventData pointerEventData;
    
    private void Start()
    {
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }
    public void OnPointerDown(PointerEventData data)
    {
        currentAbility = GameObject.Instantiate(gameObject,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 35,Input.mousePosition.y + 35,10)),transform.rotation,FindObjectOfType<Canvas>().transform);
        currentAbility.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData data)
    {
        currentAbility.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 35,Input.mousePosition.y + 35,10));
        Debug.Log(currentAbility.GetComponent<Image>().raycastTarget);
    }
    public void OnPointerUp(PointerEventData data)
    {   
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData,results);
        if(results.Count > 1 && results[1].gameObject.CompareTag("ability"))
        {
            Debug.Log("Мы кликнули на " + results[1].gameObject.name);
            results[1].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = currentAbility.transform.GetChild(0).GetComponent<Image>().sprite;
        }
        Destroy(currentAbility);
    }
}
