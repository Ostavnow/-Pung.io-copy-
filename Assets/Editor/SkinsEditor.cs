using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
[CustomEditor(typeof(Skins))]
public class SkinsEditor : Editor
{
    private ReorderableList list = null;
    private int indent = 10;
    private const int lengthSideSquare = 80;
    private Texture skinBodyTexture;
    private Texture skinHandTexture;
    private Skins skins = null;
    private MainUIHandler mainUIHandler;
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            mainUIHandler = FindObjectOfType<MainUIHandler>();
            skins = ((Skins)target);
            list = new ReorderableList(serializedObject,serializedObject.FindProperty("skins"),true,true,true,true);
            list.drawElementCallback = DrawElementCallback;
            list.elementHeightCallback = (index) => 
            {
                return EditorGUIUtility.singleLineHeight * 8 + indent * 4;
            };
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect,"Skins");
            };
            list.onAddCallback = list =>
            {
                mainUIHandler = FindObjectOfType<MainUIHandler>();
                mainUIHandler.AddSkinsToList(list,skins.prefabSkinCell);
            };
            list.onRemoveCallback = list => 
            {
                mainUIHandler = FindObjectOfType<MainUIHandler>();
                mainUIHandler.RemoveSkinToList(list);
            };
            }
        }
        
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
            if(index > 2)
            {
            SkinUIElements skinUIElement = mainUIHandler.skinUIElements[index - 3];
            Debug.Log(index);
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            // // The value of character characteristics
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"ATK");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("attackDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.x + rect.width / 6,EditorGUIUtility.singleLineHeight),"HP");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("health"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"STM");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("stamina"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"CRI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("criticalDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"AGI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("attackSpeed"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"DEF");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("protection"),GUIContent.none);

            //Sprites of the character's body and arms
                                    
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 6,rect.width / 6 * 3,18),
                element.FindPropertyRelative("spriteSkinBody"));

            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 7 + indent,rect.width / 6 * 3,18),
                element.FindPropertyRelative("spriteSkinHand"));

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 2 - 30 - indent,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,39,18),"Price");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,rect.width / 2,18),
            element.FindPropertyRelative("price"),GUIContent.none);
            UpdateSkinCharacteristicsUI(element,skinUIElement);
            }
            else
            {
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            // // The value of character characteristics
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"ATK");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("attackDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.x + rect.width / 6,EditorGUIUtility.singleLineHeight),"HP");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("health"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"STM");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("stamina"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"CRI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("criticalDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"AGI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent, rect.width / 6 - indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("attackSpeed"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 3 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),"DEF");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent,rect.width / 6 - indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("protection"),GUIContent.none);

            //Sprites of the character's body and arms
                                    
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 6,rect.width / 6 * 3,18),
                element.FindPropertyRelative("spriteSkinBody"));

            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 7 + indent,rect.width / 6 * 3,18),
                element.FindPropertyRelative("spriteSkinHand"));

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 2 - 30 - indent,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,39,18),"Price");
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 8 + indent * 2,rect.width / 2,18),
                element.FindPropertyRelative("price"),GUIContent.none);
            }
            skinBodyTexture = skinHandTexture = null;
                skinBodyTexture = ((Sprite)element?.FindPropertyRelative("spriteSkinBody")?.objectReferenceValue)?.texture;
                skinHandTexture = ((Sprite)element?.FindPropertyRelative("spriteSkinHand")?.objectReferenceValue)?.texture;
                Rect upperRect = new Rect(rect.x,rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,lengthSideSquare,lengthSideSquare);
                Rect lowerRect = new Rect(rect.x + lengthSideSquare,rect.y + EditorGUIUtility.singleLineHeight * 4 + indent * 3,lengthSideSquare / 2,lengthSideSquare / 2);
                if(skinBodyTexture != null)
                {
                    GUI.DrawTexture(upperRect,skinBodyTexture);
                }
                if(skinHandTexture != null)
                {
                    GUI.DrawTexture(lowerRect, skinHandTexture);
                }
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Serialize"))
        {
            GameManager.SerializeClassSkins(((Skins)serializedObject.targetObject).skins);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
        skins.prefabSkinCell = (GameObject) EditorGUILayout.ObjectField("Skin cell",(Object) skins.prefabSkinCell,typeof(GameObject),false);
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        }
    }
    private void UpdateSkinCharacteristicsUI(SerializedProperty element,SkinUIElements sh)
    {
        sh.attackDamageText.text = ReplacingTickADot(element.FindPropertyRelative("attackDamage").floatValue);
        sh.healthText.text = ReplacingTickADot(element.FindPropertyRelative("health").floatValue);
        sh.staminaText.text = ReplacingTickADot(element.FindPropertyRelative("stamina").floatValue);
        sh.criticalDamageText.text = ReplacingTickADot(element.FindPropertyRelative("criticalDamage").floatValue);
        sh.attackSpeedText.text = ReplacingTickADot(element.FindPropertyRelative("attackSpeed").floatValue);
        sh.protectionText.text = ReplacingTickADot(element.FindPropertyRelative("protection").floatValue);
        if(element?.FindPropertyRelative("spriteSkinBody")?.objectReferenceValue != null)
            {
                sh.skinBodyImage.sprite = ((Sprite)element?.FindPropertyRelative("spriteSkinBody").objectReferenceValue);
            }
            if(element?.FindPropertyRelative("spriteSkinHand")?.objectReferenceValue != null)
            {
                sh.skinLeftHandImage.sprite = sh.skinRightHandImage.sprite = ((Sprite)element?.FindPropertyRelative("spriteSkinHand").objectReferenceValue);
            }
        sh.priceText.text = element.FindPropertyRelative("price").intValue.ToString();
    }
    private string ReplacingTickADot(float number)
    {
        string valueString = number.ToString();
        return valueString = valueString.Replace(",",".");
    }
}
