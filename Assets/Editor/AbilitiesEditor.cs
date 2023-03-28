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
    private ReorderableList list = null;
    private int indent = 10;
    private const int lengthSideSquare = 80;
    private Texture abilityTexture;
    private Abilities abilities = null;
    private MainUIHandler mainUIHandler;
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            mainUIHandler = FindObjectOfType<MainUIHandler>();
            abilities = ((Abilities)target);
            list = new ReorderableList(serializedObject,serializedObject.FindProperty("abilities"),true,true,true,true);
            list.drawElementCallback = DrawElementCallback;
            list.elementHeightCallback = (index) => 
            {
                return EditorGUIUtility.singleLineHeight * 4 + indent * 4;
            };
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect,"Abilities");
            };
            list.onAddCallback = list =>
            {
                mainUIHandler = FindObjectOfType<MainUIHandler>();
                mainUIHandler.AddAbilityToList(list,abilities.prefabAbilityCell);
            };
            list.onRemoveCallback = list => 
            {
                mainUIHandler = FindObjectOfType<MainUIHandler>();
                mainUIHandler.RemoveAbilityToList(list);
            };
            }
        }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
            if(index > 3)
            {
            AbilityUIElements skinCharacteristicsUI = mainUIHandler.abilityUIElements[index - 4];
            Debug.Log(index);
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 3 - indent,EditorGUIUtility.singleLineHeight),"Sprite ability");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight + 5, rect.width / 3, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("spriteAbility"),GUIContent.none);
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 2 + indent,rect.width / 3 - indent,EditorGUIUtility.singleLineHeight),"price");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 2 + indent,rect.width / 3,18),
                element.FindPropertyRelative("price"),GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 4,39,18),"ability");
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4,rect.width / 3,18),
                element.FindPropertyRelative("abilityType"),GUIContent.none);
            UpdateSkinCharacteristicsUI(element,skinCharacteristicsUI);
            }
            else
            {
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Skin " + (index + 1))));
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight + 5,rect.width / 3 - indent,EditorGUIUtility.singleLineHeight),"Sprite ability");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4, rect.y + EditorGUIUtility.singleLineHeight + 5, rect.width / 3, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("spriteAbility"),GUIContent.none);
            EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 2 + indent,rect.width / 3 - indent,EditorGUIUtility.singleLineHeight),"price");
            EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 2 + indent,rect.width / 3,18),
                element.FindPropertyRelative("price"),GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + rect.width / 6 * 2.5f,rect.y + EditorGUIUtility.singleLineHeight * 4,39,18),"ability");
                EditorGUI.PropertyField(new Rect(rect.x + rect.width / 6 * 4,rect.y + EditorGUIUtility.singleLineHeight * 4,rect.width / 3,18),
                element.FindPropertyRelative("abilityType"),GUIContent.none);
            }
            abilityTexture = null;
            if(element?.FindPropertyRelative("spriteAbility").objectReferenceValue != null)
            {
                abilityTexture = ((Sprite)element?.FindPropertyRelative("spriteAbility").objectReferenceValue).texture;
            }
                Rect upperRect = new Rect(rect.x,rect.y + EditorGUIUtility.singleLineHeight + 5,lengthSideSquare,lengthSideSquare);
                if(abilityTexture != null)
                {
                    GUI.DrawTexture(upperRect,abilityTexture);
                }
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Serialize"))
        {
            GameManager.SerializeClassAbility(((Abilities)serializedObject.targetObject).abilities);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
        abilities.prefabAbilityCell = (GameObject) EditorGUILayout.ObjectField("Skin cell",(Object) abilities.prefabAbilityCell,typeof(GameObject),false);
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        }
    }
    private void UpdateSkinCharacteristicsUI(SerializedProperty element,AbilityUIElements sh)
    {
        if(element?.FindPropertyRelative("spriteAbility")?.objectReferenceValue != null)
            {
                sh.imageAbility.sprite = ((Sprite)element?.FindPropertyRelative("spriteAbility").objectReferenceValue);
            }
        sh.priceText.text = element.FindPropertyRelative("price").intValue.ToString();
    }
    private string ReplacingTickADot(float number)
    {
        string valueString = number.ToString();
        return valueString = valueString.Replace(",",".");
    }
}
