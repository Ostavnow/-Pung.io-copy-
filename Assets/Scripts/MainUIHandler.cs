using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainUIHandler : MonoBehaviour
{
    public List<SkinCharacteristicsUI> skinCharacteristicsUI = new List<SkinCharacteristicsUI>();
    #if UNITY_EDITOR
    public static SerializedObject serializedObject;
    #endif
    public static MainUIHandler mainUIHandler;
    [HideInInspector]
    public Skins c_Skins;
    [HideInInspector] public Image healthStripe;
    [HideInInspector] public Image staminaStripe;
    [HideInInspector] public Image XPStripe;
    [HideInInspector] public TMP_Text textOfHealthStrip;
    [HideInInspector] public TMP_Text textOfStaminaStrip;
    [HideInInspector] public TMP_Text attackDamageText;
    [HideInInspector] public TMP_Text healthText;
    [HideInInspector] public TMP_Text staminaText;
    [HideInInspector] public TMP_Text criticalDamageText;
    [HideInInspector] public TMP_Text attackSpeedText;
    [HideInInspector] public TMP_Text multiplierAttackDamageImprovementText;
    [HideInInspector] public TMP_Text healthImprovementMultiplierText;
    [HideInInspector] public TMP_Text staminaImprovementMultiplierText;
    [HideInInspector] public TMP_Text criticalDamageImprovementMultiplierText;
    [HideInInspector] public TMP_Text attackSpeedImprovementMultiplierText;
    [HideInInspector] public TMP_Text numberImproventsText;
    [HideInInspector] public TMP_Text levelText;
    [HideInInspector] public TMP_Text comboText;
    [HideInInspector] public Transform canvasTransform;
    #if UNITY_EDITOR
    public void OnValidate()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            serializedObject = new SerializedObject(this);
            c_Skins = FindObjectOfType<Skins>();
            Debug.Log(serializedObject);
        }
    }
    #endif
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
        canvasTransform = GameObject.Find("Canvas").transform;
        Debug.Log(canvasTransform);
        healthStripe = canvasTransform.GetChild(4).GetChild(1).GetChild(1).GetComponent<Image>();
        staminaStripe = canvasTransform.GetChild(4).GetChild(2).GetChild(1).GetComponent<Image>();
        textOfHealthStrip = canvasTransform.GetChild(4).GetChild(1).GetChild(2).GetComponent<TMP_Text>();
        textOfStaminaStrip = canvasTransform.GetChild(4).GetChild(2).GetChild(2).GetComponent<TMP_Text>();
        XPStripe = canvasTransform.GetChild(4).GetChild(5).GetChild(1).GetComponent<Image>();
        levelText = canvasTransform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        numberImproventsText = canvasTransform.GetChild(6).GetChild(6).GetChild(0).GetComponent<TMP_Text>();
        comboText = canvasTransform.GetChild(10).GetChild(0).GetComponent<TMP_Text>();
        multiplierAttackDamageImprovementText = canvasTransform.transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        healthImprovementMultiplierText = canvasTransform.transform.GetChild(6).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
        staminaImprovementMultiplierText = canvasTransform.transform.GetChild(6).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        criticalDamageImprovementMultiplierText = canvasTransform.transform.GetChild(6).GetChild(3).GetChild(1).GetComponent<TMP_Text>();
        attackSpeedImprovementMultiplierText = canvasTransform.transform.GetChild(6).GetChild(4).GetChild(1).GetComponent<TMP_Text>();
        attackDamageText = canvasTransform.GetChild(6).GetChild(0).GetChild(2).GetComponent<TMP_Text>();
        healthText = canvasTransform.transform.GetChild(6).GetChild(1).GetChild(2).GetComponent<TMP_Text>();
        staminaText = canvasTransform.transform.GetChild(6).GetChild(2).GetChild(2).GetComponent<TMP_Text>();
        criticalDamageText = canvasTransform.GetChild(6).GetChild(3).GetChild(2).GetComponent<TMP_Text>();
        attackSpeedText = canvasTransform.GetChild(6).GetChild(4).GetChild(2).GetComponent<TMP_Text>();
        }
    }
    #if UNITY_EDITOR
    public void UIAddSkinsToList(IList skins,GameObject prefabSkinCell)
    {
        Transform listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + skins.Count) == null)
        {
            c_Skins.skins.Add(new Skin());
            GameObject currentSkinCell = Instantiate(prefabSkinCell,Vector3.zero,Quaternion.identity,listSkins);
            currentSkinCell.name = "Skin " + (skins.Count);
            Debug.Log(skins.Count);
        }       
                Skin skin = (Skin) skins[skins.Count - 1];
                skin.skinIndex = skins.Count - 1;
                SkinCharacteristicsUI skinCharacteristicUI = new SkinCharacteristicsUI(
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(3).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(4).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(5).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(1).GetChild(0).GetComponent<Image>());
                skinCharacteristicsUI.Add(skinCharacteristicUI);
                serializedObject = new SerializedObject(mainUIHandler);
                Debug.Log("Создалась ячейка скина");
    }
    public void UIRemoveSkinToList(ReorderableList skins)
    {
        Transform listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        Debug.Log(skins.list.Count);
        if(GameObject.Find("Canvas/skins panel/Background/scrollbar/Scroll Area/Skins/Skin " + skins.list.Count) != null)
        {
            Destroy(listSkins.GetChild(skins.list.Count));
        }
        c_Skins.skins.Remove(c_Skins.skins[c_Skins.skins.Count - 1]);
        // if(skinCharacteristicsUI.Count)
        Debug.Log(skinCharacteristicsUI.Count);
        skinCharacteristicsUI.Remove(skinCharacteristicsUI[skinCharacteristicsUI.Count - 1]);
    }
    #endif
}
[Serializable]
public struct SkinCharacteristicsUI
{
        [SerializeField]
        private TMP_Text attackDamageText;
        [SerializeField]
        private TMP_Text healthText;
        [SerializeField]
        private TMP_Text staminaText;
        [SerializeField]
        private TMP_Text criticalDamageText;
        [SerializeField]
        private TMP_Text attackSpeedText;
        [SerializeField]
        private TMP_Text protectionText;
        [SerializeField]
        private TMP_Text priceText;
        [SerializeField]
        private Image skinBodyImage;
        [SerializeField]
        private Image skinRightHandImage;
        [SerializeField]
        private Image skinLeftHandImage;
        public SkinCharacteristicsUI(TMP_Text attackDamageText,TMP_Text healthText,
                              TMP_Text staminaText,TMP_Text criticalDamageText,
                              TMP_Text attackSpeedText,TMP_Text protectionText,
                              TMP_Text priceText,Image skinBodyImage,
                              Image skinRightHandImage,Image skinLeftHandImage)
                            {
                                this.attackDamageText = attackDamageText;
                                this.healthText = healthText;
                                this.staminaText = staminaText;
                                this.criticalDamageText = criticalDamageText;
                                this.attackSpeedText = attackSpeedText;
                                this.protectionText = protectionText;
                                this.priceText = priceText;
                                this.skinBodyImage = skinBodyImage;
                                this.skinRightHandImage = skinRightHandImage;
                                this.skinLeftHandImage = skinLeftHandImage;
                            }
}
