using UnityEngine;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR_WIN
using UnityEditor.SceneManagement;
using UnityEditor;
#endif
using TMPro;
using UnityEngine.UI;
[Serializable]
public class Skin
{
    public int skinIndex;
    private float p_attackDamage = 1f;
    public float attackDamage
    {
        get{return p_attackDamage;}
        set
        {
            p_attackDamage = value;
            #if UNITY_EDITOR
            TMP_Text attackDamageText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("attackDamageText").objectReferenceValue);
            attackDamageText.text = ReplacingTickADot(value);
            #endif
        }
    }
    private float p_health = 1f;
    public float health
    {
        get{return p_health;}
        set
        {
            p_health = value;
            #if UNITY_EDITOR
            TMP_Text healthText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("healthText").objectReferenceValue);
            healthText.text = ReplacingTickADot(value);
            #endif
        }
    }
    private float p_stamina = 1f;
    public float stamina
    {
        get{return p_stamina;}
        set
        {
            p_stamina = value;
            #if UNITY_EDITOR
            TMP_Text staminaText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("staminaText").objectReferenceValue);
            staminaText.text = ReplacingTickADot(value);
            #endif
        }
    }

    private float p_criticalDamage = 1f;
    public float criticalDamage
    {
        get{return p_criticalDamage;}
        set
        {
            p_criticalDamage = value;
            #if UNITY_EDITOR
            TMP_Text criticalDamageText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("criticalDamageText").objectReferenceValue);
            criticalDamageText.text = ReplacingTickADot(value);
            #endif
        }
    }

    private float p_attackSpeed = 1f;
    public float attackSpeed
    {
        get{return p_attackSpeed;}
        set
        {
            p_attackSpeed = value;
            #if UNITY_EDITOR
            TMP_Text attackSpeedText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("attackSpeedText").objectReferenceValue);
            attackSpeedText.text = ReplacingTickADot(value);
            #endif
        }
    }

    private float p_protection = 1f;
    public float protection
    {
        get{return p_protection;}
        set
        {
            p_protection = value;
            #if UNITY_EDITOR
            TMP_Text protectionText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("protectionText").objectReferenceValue);
            protectionText.text = ReplacingTickADot(value);
            #endif
        }
    }

    private int p_price = 100;
    public int price
    {
        get{return p_price;}
        set
        {
            p_price = value;
            #if UNITY_EDITOR
            TMP_Text priceText = ((TMP_Text)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("priceText").objectReferenceValue);
            priceText.text = ReplacingTickADot(value);
            #endif
        }
    }

    private Sprite p_spriteSkinBody;
    public Sprite spriteSkinBody
    {
        get{return p_spriteSkinBody;}
        set
        {
            p_spriteSkinBody = value;
            #if UNITY_EDITOR
            Image skinBodyImage = ((Image)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("skinBodyImage").objectReferenceValue);
            skinBodyImage.sprite = value;
            #endif
        }
    }

    private Sprite p_spriteSkinHand;
    public Sprite spriteSkinHand
    {
        get{return p_spriteSkinHand;}
        set
        {
            p_spriteSkinHand = value;
            #if UNITY_EDITOR
            Image skinRightHandImage = ((Image)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("skinRightHandImage").objectReferenceValue);
            Image skinLeftHandImage = ((Image)MainUIHandler.serializedObject.FindProperty("skinCharacteristicsUI").GetArrayElementAtIndex(skinIndex).FindPropertyRelative("skinLeftHandImage").objectReferenceValue);
            skinLeftHandImage.sprite = skinRightHandImage.sprite = value;
            #endif
        }
    }
    private string ReplacingTickADot(float number)
    {
        string valueString = number.ToString();
        return valueString = valueString.Replace(",",".");
    }
    #if UNITY_EDITOR
    public static void SetObjectDitry(GameObject obj) {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(obj.scene);
        }
    #endif
}
