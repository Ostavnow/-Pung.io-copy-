using UnityEngine;
using UnityEngine.UI;
public class AbilitiesSelectebleUI : MonoBehaviour
{
    private User _user;
    [SerializeField] private GameObject _prefabAbilityCell;
    private DataManager _dataManager;
    private Transform _listAbilities;
    private Transform _listSelectedAbilities;
    void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _user = _dataManager._user;
        _listAbilities = transform;
        _listSelectedAbilities = GameObject.Find("Canvas").transform.GetChild(4).GetChild(0);
    }
    private void ShowingPurchasedAbilities()
    {
        for(int i = 0;i < _user._purchasedAbilities.Count;i++)
        {
            int purchasedAbility = _user._purchasedAbilities[i];
            if(purchasedAbility != (int)_user._seletedAbilities[0] && 
            purchasedAbility != (int)_user._seletedAbilities[1] && 
            purchasedAbility != (int)_user._seletedAbilities[2] && 
            purchasedAbility != (int)_user._seletedAbilities[3])
            {
            GameObject abilityCellCurrent = Instantiate(_prefabAbilityCell,transform.position,Quaternion.identity,_listAbilities);
            Ability ability = _dataManager.DeserializeAbility(_user._purchasedAbilities[i]);
            Image abilityImage = abilityCellCurrent.transform.GetChild(0).GetComponent<Image>();
            abilityCellCurrent.GetComponent<AbilitySelectUI>()._abilityEnum = ability._abilityType;
            abilityImage.sprite = ability._spriteAbility;
            abilityCellCurrent.GetComponent<AbilitySelectUI>()._abilityEnum = ability._abilityType;
            }
            
        }
        for(int i = 0; i < _user._seletedAbilities.Length;i++)
        {
            Ability ability = _dataManager.DeserializeAbility((int)_user._seletedAbilities[i]);
            Transform abilityUI = _listSelectedAbilities.GetChild(i);
            abilityUI.GetChild(0).GetComponent<Image>().sprite = ability._spriteAbility;
            abilityUI.GetComponent<AbilitySelectUI>()._abilityEnum = ability._abilityType;
            abilityUI.GetComponent<AbilitySelectUI>()._numberSelectedAbility = i;
        }
    }
}
