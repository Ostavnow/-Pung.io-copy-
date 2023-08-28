using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEditor.Events;
using UI;
[CustomEditor(typeof(Skins))]
public class SkinsEditor : Editor
{
    private readonly List<SkinUIElements> _skinUIElements = new List<SkinUIElements>();
    private ReorderableList _list = null;
    private int _indent = 10;
    private const int lengthSideSquare = 80;
    private Texture _skinBodyTexture;
    private Texture _skinHandTexture;
    private Skins _skins = null;
    private SkinUI _skinUI;
    private Transform _listSkins;
    private GameObject _refundPanel;

    // disable the ability to delete GameObjects in Scene view
    protected virtual void OnSceneGUI()
    {
        InterceptKeyboardDelete();
    }

    // disable the ability to delete GameObjects in Hierarchy view
    protected virtual void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        InterceptKeyboardDelete();
    }


    // intercept keyboard delete event
    private void InterceptKeyboardDelete()
    {
        var e = Event.current;
        if (e.keyCode == KeyCode.Delete)
        {
            //e.Use(); // warning
            e.type = EventType.Used;
        }
    }
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            _refundPanel = GameObject.Find("Canvas").transform.GetChild(8).gameObject;
            _skins = ((Skins)target);
            _skinUI = _skins.GetComponent<SkinUI>();
            _listSkins = _skins.transform;
            _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_skins"), true, true, true, true)
            {
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = (index) => EditorGUIUtility.singleLineHeight * 8 + _indent * 4,
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Skins"),
                onAddCallback = list => AddSkinsToList(list, _skins._prefabSkinCell),
                onRemoveCallback = list => RemoveSkinToList(list)
            };
        }
        }
    protected virtual void OnDisable()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
    }
    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            if(index < 3)
            {
            SkinUIElements skinUIElement = _skinUIElements[index - 3];
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
    public void AddSkinsToList(ReorderableList skins,GameObject prefabSkinCell)
    {
        int index = skins.serializedProperty.arraySize;
        skins.serializedProperty.arraySize++;
        index = skins.serializedProperty.arraySize;
        skins.index = index;
        if(index > 3)
        {
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + index) == null)
        {
            GameObject currentSkinCell = Instantiate(prefabSkinCell,Vector3.zero,Quaternion.identity,_listSkins);
            Button buyButton = currentSkinCell.transform.GetChild(2).GetComponent<Button>();
            UnityAction<int> actionBuyButton = new UnityAction<int>(_skinUI.BuySkin);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            Button refundButton = currentSkinCell.transform.GetChild(4).GetComponent<Button>();
            UnityAction<int> actionRefundButton = new UnityAction<int>(_skinUI.RefundSkin);
            UnityEventTools.AddIntPersistentListener(refundButton.onClick,actionRefundButton,(index - 1));
            SkinCellUI skinCellUI = currentSkinCell.AddComponent<SkinCellUI>();
            skinCellUI.id = index - 1;
            currentSkinCell.name = "Skin " + (index);
        }       
                SkinUIElements skinUIElement = new SkinUIElements(
                    _listSkins.GetChild(index - 4).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(0).GetChild(3).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(0).GetChild(4).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(0).GetChild(5).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    _listSkins.GetChild(index - 4).GetChild(1).GetComponent<Image>(),
                    _listSkins.GetChild(index - 4).GetChild(1).GetChild(1).GetComponent<Image>(),
                    _listSkins.GetChild(index - 4).GetChild(1).GetChild(0).GetComponent<Image>());
                _skinUIElements.Add(skinUIElement);
        }
    }
    public void RemoveSkinToList(ReorderableList skins)
    {
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + skins.serializedProperty.arraySize) != null)
        {
            DestroyImmediate(_listSkins.GetChild(skins.serializedProperty.arraySize - 4).gameObject);
        }
        skins.serializedProperty.arraySize--;
        _skinUIElements.RemoveAt(_skinUIElements.Count - 1);
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Serialize"))
        {
            DataManager.SerializeClassSkins(((Skins)serializedObject.targetObject)._listSkins);
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