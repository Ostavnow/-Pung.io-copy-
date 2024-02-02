using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
namespace AbilitiesSystem
{
    public class AbilityEditorData : MonoBehaviour
    {
        [SerializeField] public List<AbilityUIElements> _abilityUIElements = new List<AbilityUIElements>();
        [HideInInspector]
        public Transform _listAbilitiesTransform;
        [HideInInspector]
        public GameObject _prefabAbilityCell;
    }
    [System.Serializable]
    public struct AbilityUIElements
    {
        [SerializeField]
        public Image _imageAbility;
        [SerializeField]
        public TMP_Text _priceText;
        public AbilityUIElements(Image imageAbility,TMP_Text priceText)
        {
            _imageAbility = imageAbility;
            _priceText = priceText;
        }
    }
}