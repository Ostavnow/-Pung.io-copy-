using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SkinsEditorData : MonoBehaviour
{
    [HideInInspector]
    public List<SkinUIElements> _skinUIElements = new List<SkinUIElements>();
    [HideInInspector]
    public Transform _listSkinsTransform;
    [HideInInspector]
    public GameObject _prefabSkinCell;
}
[System.Serializable]
public struct SkinUIElements
{
        [SerializeField]
        public TMP_Text _attackDamageText;
        [SerializeField]
        public TMP_Text _healthText;
        [SerializeField]
        public TMP_Text _staminaText;
        [SerializeField]
        public TMP_Text _criticalDamageText;
        [SerializeField]
        public TMP_Text _attackSpeedText;
        [SerializeField]
        public TMP_Text _protectionText;
        [SerializeField]
        public TMP_Text _priceText;
        [SerializeField]
        public Image _skinBodyImage;
        [SerializeField]
        public Image _skinRightHandImage;
        [SerializeField]
        public Image _skinLeftHandImage;
        public SkinUIElements(TMP_Text attackDamageText,TMP_Text healthText,
                              TMP_Text staminaText,TMP_Text criticalDamageText,
                              TMP_Text attackSpeedText,TMP_Text protectionText,
                              TMP_Text priceText,Image skinBodyImage,
                              Image skinRightHandImage,Image skinLeftHandImage)
                            {
                                _attackDamageText = attackDamageText;
                                _healthText = healthText;
                                _staminaText = staminaText;
                                _criticalDamageText = criticalDamageText;
                                _attackSpeedText = attackSpeedText;
                                _protectionText = protectionText;
                                _priceText = priceText;
                                _skinBodyImage = skinBodyImage;
                                _skinRightHandImage = skinRightHandImage;
                                _skinLeftHandImage = skinLeftHandImage;
                            }
}