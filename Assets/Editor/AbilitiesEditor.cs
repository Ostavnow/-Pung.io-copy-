using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
[CustomEditor(typeof(Abilities))]
public class AbilityEditor : Editor
{
    private ReorderableList _list = null;
    private int _indent = 10;
    private const int _lengthSideSquare = 80;
    private Texture _abilityTexture;
    private Abilities _abilities = null;
    private MainUIHandler _mainUIHandler;
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            _mainUIHandler = FindObjectOfType<MainUIHandler>();
            _abilities = ((Abilities)target);
            _list = new ReorderableList(serializedObject,serializedObject.FindProperty("_abilities"),true,true,true,true);
            _list.drawElementCallback = DrawElementCallback;
            _list.elementHeightCallback = (index) => 
            {
                return EditorGUIUtility.singleLineHeight * 4 + _indent * 4;
            };
            _list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect,"Abilities");
            };
            _list.onAddCallback = list =>
            {
                _mainUIHandler = FindObjectOfType<MainUIHandler>();
                _mainUIHandler.AddAbilityToList(list,_abilities._prefabAbilityCell);
            };
            _list.onRemoveCallback = list => 
            {
                _mainUIHandler = FindObjectOfType<MainUIHandler>();
                _mainUIHandler.RemoveAbilityToList(list);
            };
            }
        }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            if(index > 3)
            {
            AbilityUIElements skinCharacteristicsUI = _mainUIHandler._abilityUIElements[index - 4];
            Debug.Log(index);
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 3 - _indent,EditorGUIUtility.singleLineHeight),"Sprite ability");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight + 5, rect.width / 3, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_spriteAbility"),GUIContent.none);
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 2 + _indent,rect.width / 3 - _indent,EditorGUIUtility.singleLineHeight),"price");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 2 + _indent,rect.width / 3,18),
                element.FindPropertyRelative("_price"),GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 4,39,18),"ability");
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4,rect.width / 3,18),
                element.FindPropertyRelative("_abilityType"),GUIContent.none);
            UpdateSkinCharacteristicsUI(element,skinCharacteristicsUI);
            }
            else
            {
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 3 - _indent,EditorGUIUtility.singleLineHeight),"Sprite ability");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight + 5, rect.width / 3, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("_spriteAbility"),GUIContent.none);
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 2 + _indent,rect.width / 3 - _indent,EditorGUIUtility.singleLineHeight),"price");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 2 + _indent,rect.width / 3,18),
                element.FindPropertyRelative("_price"),GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 4,39,18),"ability");
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4,rect.width / 3,18),
                element.FindPropertyRelative("_abilityType"),GUIContent.none);
            }
            _abilityTexture = null;
            if(element?.FindPropertyRelative("_spriteAbility").objectReferenceValue != null)
            {
                _abilityTexture = ((Sprite)element?.FindPropertyRelative("_spriteAbility").objectReferenceValue).texture;
            }
                Rect upperRect = new Rect(rect.x,rect.y + EditorGUIUtility.singleLineHeight + 5,_lengthSideSquare,_lengthSideSquare);
                if(_abilityTexture != null)
                {
                    GUI.DrawTexture(upperRect,_abilityTexture);
                }
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Serialize"))
        {
            GameManager.SerializeClassAbility(((Abilities)serializedObject.targetObject)._abilities);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
        _abilities._prefabAbilityCell = (GameObject) EditorGUILayout.ObjectField("Ability cell",(Object) _abilities._prefabAbilityCell,typeof(GameObject),false);
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        }
    }
    private void UpdateSkinCharacteristicsUI(SerializedProperty element,AbilityUIElements sh)
    {
        if(element?.FindPropertyRelative("_spriteAbility")?.objectReferenceValue != null)
            {
                sh._imageAbility.sprite = ((Sprite)element?.FindPropertyRelative("_spriteAbility").objectReferenceValue);
            }
        sh._priceText.text = element.FindPropertyRelative("_price").intValue.ToString();
    }
}
