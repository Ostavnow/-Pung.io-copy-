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
    private ReorderableList _list = null;
    private int _indent = 10;
    private const int lengthSideSquare = 80;
    private Texture _skinBodyTexture;
    private Texture _skinHandTexture;
    private Skins _skins = null;
    private MainUIHandler _mainUIHandler;
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            _mainUIHandler = FindObjectOfType<MainUIHandler>();
            _skins = ((Skins)target);
            _list = new ReorderableList(serializedObject,serializedObject.FindProperty("_skins"),true,true,true,true);
            _list.drawElementCallback = DrawElementCallback;
            _list.elementHeightCallback = (index) => 
            {
                return EditorGUIUtility.singleLineHeight * 8 + _indent * 4;
            };
            _list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect,"Skins");
            };
            _list.onAddCallback = list =>
            {
                _mainUIHandler = FindObjectOfType<MainUIHandler>();
                _mainUIHandler.AddSkinsToList(list,_skins._prefabSkinCell);
            };
            _list.onRemoveCallback = list => 
            {
                _mainUIHandler = FindObjectOfType<MainUIHandler>();
                _mainUIHandler.RemoveSkinToList(list);
            };
            }
        }
        
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            if(index > 2)
            {
            SkinUIElements skinUIElement = _mainUIHandler._skinUIElements[index - 3];
            Debug.Log(index);
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            // // The value of character characteristics
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"ATK");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_attackDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.x + rect.width / 6,EditorGUIUtility.singleLineHeight),"HP");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_health"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"STM");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_stamina"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 3 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"CRI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent, rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_criticalDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 3 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"AGI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent, rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_attackSpeed"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 3 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"DEF");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_protection"),GUIContent.none);

            //Sprites of the character's body and arms
                                    
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 6,rect.width / 6 * 3,18),
                element.FindPropertyRelative("_spriteSkinBody"));

            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 7 + _indent,rect.width / 6 * 3,18),
                element.FindPropertyRelative("_spriteSkinHand"));

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 2 - 30 - _indent,rect.y + EditorGUIUtility.singleLineHeight * 8 + _indent * 2,39,18),"Price");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 8 + _indent * 2,rect.width / 2,18),
            element.FindPropertyRelative("_price"),GUIContent.none);
            UpdateSkinCharacteristicsUI(element,skinUIElement);
            }
            else
            {
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            // // The value of character characteristics
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"ATK");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_attackDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.x + rect.width / 6,EditorGUIUtility.singleLineHeight),"HP");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_health"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"STM");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5, rect.y + EditorGUIUtility.singleLineHeight * 2 + 5, rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_stamina"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 3 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"CRI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 3,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent, rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_criticalDamage"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 3 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"AGI");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent, rect.width / 6 - _indent, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_attackSpeed"),GUIContent.none);

            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 3 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),"DEF");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 5,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent,rect.width / 6 - _indent,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_protection"),GUIContent.none);

            //Sprites of the character's body and arms
                                    
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 6,rect.width / 6 * 3,18),
                element.FindPropertyRelative("_spriteSkinBody"));

            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 7 + _indent,rect.width / 6 * 3,18),
                element.FindPropertyRelative("_spriteSkinHand"));

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 2 - 30 - _indent,rect.y + EditorGUIUtility.singleLineHeight * 8 + _indent * 2,39,18),"Price");
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 2,rect.y + EditorGUIUtility.singleLineHeight * 8 + _indent * 2,rect.width / 2,18),
                element.FindPropertyRelative("_price"),GUIContent.none);
            }
            _skinBodyTexture = _skinHandTexture = null;
                _skinBodyTexture = ((Sprite)element?.FindPropertyRelative("_spriteSkinBody")?.objectReferenceValue)?.texture;
                _skinHandTexture = ((Sprite)element?.FindPropertyRelative("_spriteSkinHand")?.objectReferenceValue)?.texture;
                Rect upperRect = new Rect(rect.x,rect.y + EditorGUIUtility.singleLineHeight * 2 + 5,lengthSideSquare,lengthSideSquare);
                Rect lowerRect = new Rect(rect.x + lengthSideSquare,rect.y + EditorGUIUtility.singleLineHeight * 4 + _indent * 3,lengthSideSquare / 2,lengthSideSquare / 2);
                if(_skinBodyTexture != null)
                {
                    GUI.DrawTexture(upperRect,_skinBodyTexture);
                }
                if(_skinHandTexture != null)
                {
                    GUI.DrawTexture(lowerRect, _skinHandTexture);
                }
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Serialize"))
        {
            GameManager.SerializeClassSkins(((Skins)serializedObject.targetObject)._skins);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
        _skins._prefabSkinCell = (GameObject) EditorGUILayout.ObjectField("Skin cell",(Object) _skins._prefabSkinCell,typeof(GameObject),false);
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        }
    }
    private void UpdateSkinCharacteristicsUI(SerializedProperty element,SkinUIElements sh)
    {
        sh._attackDamageText.text = ReplacingTickADot(element.FindPropertyRelative("_attackDamage").floatValue);
        sh._healthText.text = ReplacingTickADot(element.FindPropertyRelative("_health").floatValue);
        sh._staminaText.text = ReplacingTickADot(element.FindPropertyRelative("_stamina").floatValue);
        sh._criticalDamageText.text = ReplacingTickADot(element.FindPropertyRelative("_criticalDamage").floatValue);
        sh._attackSpeedText.text = ReplacingTickADot(element.FindPropertyRelative("_attackSpeed").floatValue);
        sh._protectionText.text = ReplacingTickADot(element.FindPropertyRelative("_protection").floatValue);
        if(element?.FindPropertyRelative("_spriteSkinBody")?.objectReferenceValue != null)
            {
                sh._skinBodyImage.sprite = ((Sprite)element?.FindPropertyRelative("_spriteSkinBody").objectReferenceValue);
            }
            if(element?.FindPropertyRelative("_spriteSkinHand")?.objectReferenceValue != null)
            {
                sh._skinLeftHandImage.sprite = sh._skinRightHandImage.sprite = ((Sprite)element?.FindPropertyRelative("_spriteSkinHand").objectReferenceValue);
            }
        sh._priceText.text = element.FindPropertyRelative("_price").intValue.ToString();
    }
    private string ReplacingTickADot(float number)
    {
        string valueString = number.ToString();
        return valueString = valueString.Replace(",",".");
    }
}
