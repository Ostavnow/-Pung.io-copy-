using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
[CustomEditor(typeof(Abilities))]
public class AbilityEditor : Editor
{
    [SerializeField] private List<AbilityUIElements> _abilityUIElements = new List<AbilityUIElements>();
    private ReorderableList _list = null;
    private readonly int _indent = 10;
    private const int _lengthSideSquare = 80;
    private Texture _abilityTexture;
    private Abilities _abilities = null;
    private DataManager _dataManager;
    private AbilityUI _abilityUI;
    private Transform _listAbilities;
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            _dataManager = FindObjectOfType<DataManager>();
            _abilities = (Abilities)target;
            _listAbilities = _abilities.transform;
            _abilityUI = _abilities.GetComponent<AbilityUI>();
            _listAbilities = _abilities.transform;
            _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_abilities"), true, true, true, true)
            {
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = (index) => EditorGUIUtility.singleLineHeight * 4 + _indent * 4,
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Abilities"),
                onAddCallback = list => AddAbilityToList(list, _abilities._prefabAbilityCell),
                onRemoveCallback = list => RemoveAbilityToList(list)
            };
        }
        }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            if(index > 3)
            {
            AbilityUIElements skinCharacteristicsUI = _abilityUIElements[index - 4];
            Debug.Log(index);
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
            DataManager.SerializeClassAbility(((Abilities)serializedObject.targetObject)._abilities);
        }
                _abilities._prefabAbilityCell = (GameObject) EditorGUILayout.ObjectField("Ability cell", _abilities._prefabAbilityCell, typeof(GameObject),false);
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
    public void AddAbilityToList(ReorderableList abilities,GameObject prefabAbilityCell)
    {
        Debug.Log(abilities.serializedProperty.arraySize);
        int index = abilities.serializedProperty.arraySize;
        abilities.serializedProperty.arraySize++;
        index = abilities.serializedProperty.arraySize;
        Debug.Log(index);
        abilities.index = index;
        if(index > 4)
        {
        if(GameObject.Find("Canvas/abilities panel/Background/Scroll Area/Abilities/ability " + index) == null)
        {
            GameObject currentSkinCell = Instantiate(prefabAbilityCell,Vector3.zero,Quaternion.identity,_listAbilities);
            Button buyButton = currentSkinCell.transform.GetChild(1).GetComponent<Button>();
            UnityAction<int> actionBuyButton = new UnityAction<int>(_abilityUI.BuyAbility);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            currentSkinCell.name = "ability " + (index);
        }       
                AbilityUIElements abilityUIElement = new AbilityUIElements(
                    _listAbilities.GetChild(index - 5).GetChild(0).GetComponent<Image>(),
                    _listAbilities.GetChild(index - 5).GetChild(1).GetChild(0).GetComponent<TMP_Text>());
                _abilityUIElements.Add(abilityUIElement);
        }
    }
    public void RemoveAbilityToList(ReorderableList abilities)
    {
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Abilities/ability " + abilities.serializedProperty.arraySize) != null)
        {
            DestroyImmediate(_listAbilities.GetChild(abilities.serializedProperty.arraySize - 5).gameObject);
        }
        abilities.serializedProperty.arraySize--;
        _abilityUIElements.RemoveAt(_abilityUIElements.Count - 1);
    }
    private void UpdateSkinCharacteristicsUI(SerializedProperty element,AbilityUIElements sh)
    {
        if(element?.FindPropertyRelative("_spriteAbility")?.objectReferenceValue != null)
            {
                sh._imageAbility.sprite = (Sprite)element?.FindPropertyRelative("_spriteAbility").objectReferenceValue;
            }
        sh._priceText.text = element.FindPropertyRelative("_price").intValue.ToString();
    }
}
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
