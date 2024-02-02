using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEditor.Events;
using UI;
using AbilitiesSystem;
using System.Reflection;
using System;
using System.Linq;
using Codice.Client.Common.GameUI;
[CustomEditor(typeof(Abilities))]
public class AbilityEditor : Editor
{
    private ReorderableList _list = null;
    private readonly int _indent = 10;
    private const int _lengthSideSquare = 80;
    private Texture _abilityTexture;
    private Abilities _abilities = null;
    private DataManager _dataManager;
    private AbilityUI _abilityUI;
    private AbilityEditorData _abilityEditorData;
    private void OnEnable()
    {
            _dataManager = FindObjectOfType<DataManager>();
            _abilities = (Abilities)target;
            _abilityEditorData = _abilities.GetComponent<AbilityEditorData>();
            _abilityUI = _abilities.GetComponent<AbilityUI>();
            _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_listAbilities"), true, true, true, true)
            {
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = (index) => EditorGUIUtility.singleLineHeight * 4 + _indent * 4,
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Abilities"),
                onAddCallback = list => AddAbilityToList(list, _abilityEditorData._prefabAbilityCell),
                onRemoveCallback = list => RemoveAbilityToList(list)
            };
            UpdateAbilityList(_list);
    }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            if(index > 3)
            {
            AbilityUIElements skinCharacteristicsUI = _abilityEditorData._abilityUIElements[index - 4];
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Ability " + (index + 1))));
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
            EditorGUI.LabelField(new Rect(rect.x,rect.y,rect.width,EditorGUIUtility.singleLineHeight),new GUIContent(("Ability " + (index + 1))));
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
            DataManager.SerializeClassAbility(((Abilities)serializedObject.targetObject)._listAbilities);
        }
        // _abilityEditorData._listAbilitiesTransform = (Transform) EditorGUILayout.ObjectField("list abilities",(Object) _abilityEditorData._listAbilitiesTransform,typeof(Transform),true);
        _abilityEditorData._prefabAbilityCell = (GameObject) EditorGUILayout.ObjectField("Ability cell", _abilityEditorData._prefabAbilityCell, typeof(GameObject),false);
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
    public void AddAbilityToList(ReorderableList abilities,GameObject prefabAbilityCell)
    {
        int index;
        abilities.serializedProperty.arraySize++;
        index = abilities.serializedProperty.arraySize;
        Debug.Log(index);
        abilities.index = index;
        if(index > 4)
        {
        if(GameObject.Find("Canvas/abilities panel/Background/Scroll Area/Abilities/ability " + index) == null)
        {
            GameObject currentAbilityCell = Instantiate(prefabAbilityCell,Vector3.zero,Quaternion.identity,_abilityEditorData._listAbilitiesTransform);
            Button buyButton = currentAbilityCell.transform.GetChild(1).GetComponent<Button>();
            UnityAction<int> actionBuyButton = new UnityAction<int>(_abilityUI.BuyAbility);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            currentAbilityCell.name = "ability " + (index);
        }       
                AbilityUIElements abilityUIElement = new AbilityUIElements(
                    _abilityEditorData._listAbilitiesTransform.GetChild(index - 5).GetChild(0).GetComponent<Image>(),
                    _abilityEditorData._listAbilitiesTransform.GetChild(index - 5).GetChild(1).GetChild(0).GetComponent<TMP_Text>());
                _abilityEditorData._abilityUIElements.Add(abilityUIElement);  
        }
    }
    public void RemoveAbilityToList(ReorderableList abilities)
    {
        if(GameObject.Find("Canvas/abilities panel/Background/Scroll Area/Abilities/ability " + abilities.serializedProperty.arraySize) != null)
        {
            DestroyImmediate(_abilityEditorData._listAbilitiesTransform.GetChild(abilities.serializedProperty.arraySize - 5).gameObject);
        }
        abilities.serializedProperty.arraySize--;
        _abilityEditorData._abilityUIElements.RemoveAt(_abilityEditorData._abilityUIElements.Count - 1);
    }
    private void UpdateSkinCharacteristicsUI(SerializedProperty element,AbilityUIElements sh)
    {
        if(element?.FindPropertyRelative("_spriteAbility")?.objectReferenceValue != null)
            {
                sh._imageAbility.sprite = (Sprite)element?.FindPropertyRelative("_spriteAbility").objectReferenceValue;
            }
        sh._priceText.text = element.FindPropertyRelative("_price").intValue.ToString();
    }
    private List<Ability> FindingAbilityClasses()
    {
        List<Ability> _listAbilities = new List<Ability>();
        List<Type> classes = Assembly.GetAssembly(typeof(Abilities)).GetTypes().ToList<Type>();
        foreach (Type clas in classes)
        {
            if(clas.BaseType == typeof(Ability))
            {
                object ability = Activator.CreateInstance(clas);
                _listAbilities.Add((Ability)ability);
            }
        }
        return _listAbilities;
    }
    private void UpdateAbilityList(ReorderableList changeableListOfAbilities)
    {
        List<Ability> newListAbility = FindingAbilityClasses();
        Debug.Log(newListAbility.Count);
        Debug.Log(changeableListOfAbilities.serializedProperty.arraySize);
        if(changeableListOfAbilities.serializedProperty.arraySize == 0)
        {
            Abilities abilities = new Abilities();
            new SerializedObject(abilities).CopyFromSerializedProperty(changeableListOfAbilities.serializedProperty);
        }
        SerializedProperty sp = changeableListOfAbilities.serializedProperty; // копировать, чтобы мы не перечислили оригинал
        if(sp.isArray) {
            int arrayLength = 0;
            sp.Next(true); // skip generic field
            sp.Next(true); // advance to array size field
            // Get the array size
            arrayLength = sp.intValue;

            sp.Next(true); // advance to first array index

            // Write values to list
            List<Ability> values = new List<Ability>(arrayLength);
            int lastIndex = arrayLength - 1;
            for(int currentListItem = 0;currentListItem < arrayLength;currentListItem++) {
                if(newListAbility.IndexOf((Ability)sp.managedReferenceValue)  == -1)
                {
                changeableListOfAbilities.serializedProperty.arraySize++;
                GoToLastElementArray();
                sp.managedReferenceValue = newListAbility[currentListItem];
                sp.Reset();
                currentListItem = 0;
                }
                if(currentListItem < lastIndex)
                {
                    sp.Next(false); // advance without drilling into children  
                }
                
            }
// Объект, назначенный полю с атрибутом serializereference.
            // iterate over the list displaying the contents
            for(int i = 0; i < values.Count; i++) {
                EditorGUILayout.LabelField(i + " = " + values);
            }
            void GoToLastElementArray()
            {
            sp.Reset();
            for(int i = 0; i < 3; i++)
            {
                sp.Next(true);
            }
            for(int i = 0; i < lastIndex;i++)
            {
                sp.Next(false);
            }
            }
        }
        
    }
}