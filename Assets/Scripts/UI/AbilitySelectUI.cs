using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AbilitiesSystem;
public class AbilitySelectUI : MonoBehaviour , IDragHandler , IPointerDownHandler , IPointerUpHandler
{
    private static GameObject _currentAbility;
    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;
    private PointerEventData _pointerEventData;
    public AbilitiesEnum _abilityEnum;
    public int _numberSelectedAbility;
    private void Start()
    {
        _raycaster = FindObjectOfType<GraphicRaycaster>();
        _eventSystem = FindObjectOfType<EventSystem>();
    }
    public void OnPointerDown(PointerEventData data)
    {
        _currentAbility = GameObject.Instantiate(gameObject,Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 35,Input.mousePosition.y + 35,10)),transform.rotation,FindObjectOfType<Canvas>().transform);
        _currentAbility.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData data)
    {
        _currentAbility.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 35,Input.mousePosition.y + 35,10));
        Debug.Log(_currentAbility.GetComponent<Image>().raycastTarget);
    }
    public void OnPointerUp(PointerEventData data)
    {   
        _pointerEventData = new PointerEventData(_eventSystem);
        _pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData,results);
        GameObject abilityObject = results[1].gameObject;
        if(results.Count > 1 && abilityObject.CompareTag("ability"))
        {
            AbilitySelectUI abilitySelectUI = abilityObject.GetComponent<AbilitySelectUI>();
            AbilitiesEnum abilitiesEnum1 = abilitySelectUI._abilityEnum;
            abilitySelectUI._abilityEnum = _abilityEnum;
            _abilityEnum = abilitiesEnum1;
            Sprite sprite = abilityObject.transform.GetChild(0).GetComponent<Image>().sprite; 
            abilityObject.transform.GetChild(0).GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
            transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            GameManager._instance._user._seletedAbilities[abilitySelectUI._numberSelectedAbility] = (abilitySelectUI._abilityEnum);
        }
        Destroy(_currentAbility);
    }
}
