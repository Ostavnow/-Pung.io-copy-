using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AbilitySelectUI : MonoBehaviour , IDragHandler , IPointerDownHandler , IPointerUpHandler
{
    private static GameObject currentAbility;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;
    public AbilitiesEnum abilityEnum;
    public int numberSelectedAbility;
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
        GameObject abilityObject = results[1].gameObject;
        if(results.Count > 1 && abilityObject.CompareTag("ability"))
        {
            AbilitySelectUI abilitySelectUI = abilityObject.GetComponent<AbilitySelectUI>();
            AbilitiesEnum abilitiesEnum1 = abilitySelectUI.abilityEnum;
            abilitySelectUI.abilityEnum = abilityEnum;
            abilityEnum = abilitiesEnum1;
            Sprite sprite = abilityObject.transform.GetChild(0).GetComponent<Image>().sprite; 
            abilityObject.transform.GetChild(0).GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
            transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            GameManager.instance.user.seletedAbilities[abilitySelectUI.numberSelectedAbility] = ((int)abilitySelectUI.abilityEnum);
        }
        Destroy(currentAbility);
    }
}
