using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Events;

[Serializable]
public sealed class MainUIHandler : MonoBehaviour
{
    [SerializeField]
    public List<SkinUIElements> _skinUIElements = new List<SkinUIElements>();
    [SerializeField]
    public List<AbilityUIElements> _abilityUIElements = new List<AbilityUIElements>();
    [HideInInspector]
    private Skins _skins;
    private Abilities _abilities;
    public List<int> _donateSlots = new List<int>(){0,10000,50000,0,200000,500000};
    // Components in the Game window
    [HideInInspector] public Image _healthStripe;
    [HideInInspector] public Image _staminaStripe;
    [HideInInspector] public Image _XPStripe;
    [HideInInspector] public Image _firstAbilityImage;
    [HideInInspector] public Image _secondAbilityImage;
    [HideInInspector] public Image _thirdAbilityImage;
    [HideInInspector] public Image _fourthAbilityImage;
    [HideInInspector] public TMP_Text _textOfHealthStrip;
    [HideInInspector] public TMP_Text _textOfStaminaStrip;
    [HideInInspector] public TMP_Text _attackDamageText;
    [HideInInspector] public TMP_Text _healthText;
    [HideInInspector] public TMP_Text _staminaText;
    [HideInInspector] public TMP_Text _criticalDamageText;
    [HideInInspector] public TMP_Text _attackSpeedText;
    [HideInInspector] public TMP_Text _multiplierAttackDamageImprovementText;
    [HideInInspector] public TMP_Text _healthImprovementMultiplierText;
    [HideInInspector] public TMP_Text _staminaImprovementMultiplierText;
    [HideInInspector] public TMP_Text _criticalDamageImprovementMultiplierText;
    [HideInInspector] public TMP_Text _attackSpeedImprovementMultiplierText;
    [HideInInspector] public TMP_Text _numberImproventsText;
    [HideInInspector] public TMP_Text _levelText;
    [HideInInspector] public TMP_Text _comboText;
    [HideInInspector] public TMP_Text _moneyCounterTextMenu;
    [HideInInspector] public TMP_Text _moneyCounterTextGame;
    [HideInInspector] public GameObject _skilsPanel;
    [HideInInspector] public GameObject _comboGameObject; 
    private GameObject _settingPanel;
    private RectTransform _scrollbarBackground;
    private Scrollbar _scrollbarSound;
    // Components in the menu window
    private Transform _loginPanel;
    private Transform _registerPanel;
    private TMP_InputField _emailLoginInputField;
    private GameObject _incorrectEmailLogin;
    private TMP_InputField _passwordLoginInputField;
    private GameObject _incorrectPasswordLogin;
    private TMP_InputField _emailRegisterInputField;
    private GameObject _incorrectEmailRegister;
    private TMP_InputField _passwordRegisterInputField;
    private GameObject _incorrectPasswordRegister;
    private TMP_InputField _passwordConfirmRegisterInputField;
    private GameObject _incorrectPasswordConfirmRegister;
    private Image _imageButtonLoginPanel;
    private Image _imageButtonRegisterPanel;
    private GameObject _loginButton;
    private GameObject _registerButton;
    private GameObject _logoutButton;
    private TMP_InputField _nicknameInputField;
    private TMP_Text _idText;
    [SerializeField] private GameObject _skinCell;
    [SerializeField] private GameObject _abilityCell;
    [HideInInspector] public Transform _background;
    [HideInInspector] public Transform _canvasTransform;
    private Transform _listSkins;
    private Transform _listAbilities;
    private GameObject _refundPanel;
    private Transform _characters;
    private Transform _abilitiesList;
    public Transform _firstAbilitySelected;
    public Transform _secondAbilitySelected;
    public Transform _thirdAbilitySelected;
    public Transform _fourthAbilitySelected;
    private User User
    {
        get{return GameManager._instance?._user;}
    }
    private int idSelectedSkin;
    private TMP_Text vipShopMoneyCounterText;
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            _listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
            _listAbilities = GameObject.Find("Canvas").transform.GetChild(7).GetChild(0).GetChild(1).GetChild(0).transform;
        }
    }
    #endif
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            _canvasTransform = GameObject.FindGameObjectWithTag("player canvas").transform;
            _healthStripe = _canvasTransform.GetChild(4).GetChild(1).GetChild(1).GetComponent<Image>();
            _staminaStripe = _canvasTransform.GetChild(4).GetChild(2).GetChild(1).GetComponent<Image>();
            _textOfHealthStrip = _canvasTransform.GetChild(4).GetChild(1).GetChild(2).GetComponent<TMP_Text>();
            _textOfStaminaStrip = _canvasTransform.GetChild(4).GetChild(2).GetChild(2).GetComponent<TMP_Text>();
            _XPStripe = _canvasTransform.GetChild(4).GetChild(5).GetChild(1).GetComponent<Image>();
            _levelText = _canvasTransform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            _numberImproventsText = _canvasTransform.GetChild(6).GetChild(5).GetChild(0).GetComponent<TMP_Text>();
            _comboText = _canvasTransform.GetChild(10).GetChild(0).GetComponent<TMP_Text>();
            _multiplierAttackDamageImprovementText = _canvasTransform.transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            _healthImprovementMultiplierText = _canvasTransform.transform.GetChild(6).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            _staminaImprovementMultiplierText = _canvasTransform.transform.GetChild(6).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
            _criticalDamageImprovementMultiplierText = _canvasTransform.transform.GetChild(6).GetChild(3).GetChild(1).GetComponent<TMP_Text>();
            _attackSpeedImprovementMultiplierText = _canvasTransform.transform.GetChild(6).GetChild(4).GetChild(1).GetComponent<TMP_Text>();
            _attackDamageText = _canvasTransform.GetChild(6).GetChild(0).GetChild(2).GetComponent<TMP_Text>();
            _healthText = _canvasTransform.transform.GetChild(6).GetChild(1).GetChild(2).GetComponent<TMP_Text>();
            _staminaText = _canvasTransform.transform.GetChild(6).GetChild(2).GetChild(2).GetComponent<TMP_Text>();
            _criticalDamageText = _canvasTransform.GetChild(6).GetChild(3).GetChild(2).GetComponent<TMP_Text>();
            _attackSpeedText = _canvasTransform.GetChild(6).GetChild(4).GetChild(2).GetComponent<TMP_Text>();
            _moneyCounterTextGame = _canvasTransform.GetChild(9).GetChild(0).GetComponent<TMP_Text>();
            _scrollbarBackground = _canvasTransform.GetChild(13).GetChild(1).GetChild(0).GetComponent<RectTransform>();
            _scrollbarSound = _canvasTransform.GetChild(13).GetChild(1).GetComponent<Scrollbar>();
            _settingPanel = _canvasTransform.GetChild(13).gameObject;
            Debug.Log(_settingPanel);
            _firstAbilityImage = _canvasTransform.GetChild(4).GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>();
            _secondAbilityImage = _canvasTransform.GetChild(4).GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>();
            _thirdAbilityImage = _canvasTransform.GetChild(4).GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>();
            _fourthAbilityImage = _canvasTransform.GetChild(4).GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>();
            _skilsPanel = GameObject.FindGameObjectWithTag("player canvas").transform.GetChild(6).gameObject;
            _comboGameObject = GameObject.FindGameObjectWithTag("player canvas").transform.GetChild(10).gameObject;
            UserPlayer player = FindObjectOfType<UserPlayer>();
            _multiplierAttackDamageImprovementText.text = player._attackSpeedImprovementMultiplier.ToString();
            _healthImprovementMultiplierText.text = player._healthImprovementMultiplier.ToString();
            _staminaImprovementMultiplierText.text = player._staminaImprovementMultiplier.ToString();
            _criticalDamageImprovementMultiplierText.text = player._criticalDamageImprovementMultiplier.ToString();
            _attackSpeedImprovementMultiplierText.text = player._attackSpeedImprovementMultiplier.ToString();
        }
        else if(SceneManager.GetActiveScene().name == "Menu")
        {
            _canvasTransform = GameObject.Find("Canvas").transform;
            _background = _canvasTransform.GetChild(7);
            _loginPanel = _canvasTransform.GetChild(8);
            _emailLoginInputField = _loginPanel.GetChild(0).GetComponent<TMP_InputField>();
            _incorrectEmailLogin = _loginPanel.GetChild(1).gameObject;
            _passwordLoginInputField = _loginPanel.GetChild(2).GetComponent<TMP_InputField>();
            _incorrectPasswordLogin = _loginPanel.GetChild(3).gameObject;
            _imageButtonLoginPanel = _loginPanel.GetChild(9).GetComponent<Image>();
            _registerPanel = _canvasTransform.GetChild(9);
            _emailRegisterInputField = _registerPanel.GetChild(2).GetComponent<TMP_InputField>();
            _incorrectEmailRegister = _registerPanel.GetChild(4).gameObject;
            _passwordRegisterInputField = _registerPanel.GetChild(5).GetComponent<TMP_InputField>();
            _incorrectPasswordRegister = _registerPanel.GetChild(7).gameObject;
            _passwordConfirmRegisterInputField = _registerPanel.GetChild(8).GetComponent<TMP_InputField>();
            _incorrectPasswordConfirmRegister = _registerPanel.GetChild(10).gameObject;
            _imageButtonRegisterPanel = _registerPanel.GetChild(11).GetComponent<Image>();
            _loginButton = _canvasTransform.GetChild(3).GetChild(3).gameObject;
            _registerButton = _canvasTransform.GetChild(3).GetChild(4).gameObject;
            _logoutButton = _canvasTransform.GetChild(3).GetChild(5).gameObject;
            _moneyCounterTextMenu = _canvasTransform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            _nicknameInputField = _canvasTransform.GetChild(3).GetChild(6).GetChild(0).GetComponent<TMP_InputField>();
            _idText = _canvasTransform.GetChild(3).GetChild(6).GetChild(1).GetComponent<TMP_Text>();
            _characters = _canvasTransform.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0);
            _characters = _canvasTransform.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0);
            _abilitiesList = _canvasTransform.GetChild(5).GetChild(1).GetChild(1).GetChild(0);
            _firstAbilitySelected = _canvasTransform.GetChild(4).GetChild(0).GetChild(0);
            _secondAbilitySelected = _canvasTransform.GetChild(4).GetChild(0).GetChild(1);
            _thirdAbilitySelected = _canvasTransform.GetChild(4).GetChild(0).GetChild(2);
            _fourthAbilitySelected = _canvasTransform.GetChild(4).GetChild(0).GetChild(3);
        }
        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            _canvasTransform = GameObject.Find("Canvas").transform;
            _skins = FindObjectOfType<Skins>();
            _abilities = FindObjectOfType<Abilities>();
            _refundPanel = _canvasTransform.GetChild(8).gameObject;
            _listAbilities = GameObject.Find("Canvas").transform.GetChild(7).GetChild(0).GetChild(1).GetChild(0).transform;
            _listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        }
        else if(SceneManager.GetActiveScene().name == "Vip shop")
        {
            _canvasTransform = GameObject.Find("Canvas").transform;
            vipShopMoneyCounterText = _canvasTransform.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
            vipShopMoneyCounterText.text = User._amountMoney.ToString();
        }
    }
    private void Start()

    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            MenuUpdate();
            _canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(0);});
            _canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(1);});
            _canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(2);});
            if(User._purchasedSkins.Count > 4)
            {
                _canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(3).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(3);});
            }
            if(User._purchasedSkins.Count > 5)
            {
                _canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(4).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(4);});
            }
            if(User._purchasedSkins.Count == 6)
            {
                _canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(5).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(5);});
            }
        }
        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            ShopUpdate();
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            Debug.Log(User._seletedAbilities[0]);
            Debug.Log(User._seletedAbilities[1]);
            Debug.Log(User._seletedAbilities[2]);
            Debug.Log(User._seletedAbilities[3]);
            _firstAbilityImage.sprite = GameManager.DeserializeAbility(User._seletedAbilities[0])._spriteAbility;
            _secondAbilityImage.sprite = GameManager.DeserializeAbility(User._seletedAbilities[1])._spriteAbility;
            _thirdAbilityImage.sprite = GameManager.DeserializeAbility(User._seletedAbilities[2])._spriteAbility;
            _fourthAbilityImage.sprite = GameManager.DeserializeAbility(User._seletedAbilities[3])._spriteAbility;
            _moneyCounterTextGame.text = GameManager._instance._user._amountMoney.ToString();
        }
    }
    //Methods for working with the Skins class
    #if UNITY_EDITOR
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
            UnityAction<int> actionBuyButton = new UnityAction<int>(BuySkin);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            Button refundButton = currentSkinCell.transform.GetChild(4).GetComponent<Button>();
            UnityAction<int> actionRefundButton = new UnityAction<int>(RefundSkin);
            UnityEventTools.AddIntPersistentListener(refundButton.onClick,actionRefundButton,(index - 1));
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
    public void AddAbilityToList(ReorderableList abilities,GameObject prefabAbilityCell)
    {
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
            UnityAction<int> actionBuyButton = new UnityAction<int>(BuyAbility);
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
    #endif
    public void RegisterButton()
    {
        _background.gameObject.SetActive(true);
        _registerPanel.gameObject.SetActive(true);
    }
    public void LoginButton()
    {
        _background.gameObject.SetActive(true);
        _loginPanel.gameObject.SetActive(true);
    }
    public void BackLoginButton()
    {
        _background.gameObject.SetActive(false);
        _loginPanel.gameObject.SetActive(false);
    }
    public void BackRegisterButton()
    {
        _background.gameObject.SetActive(false);
        _registerPanel.gameObject.SetActive(false);
    }
    public void RegisterUser()
    {
        if(isValidEmail(_emailRegisterInputField.text))
        {
            if(GameManager.CheckingExistingEmail(_emailRegisterInputField.text))
            {
                _incorrectEmailRegister.SetActive(false);
            }
            else
            {
            _incorrectEmailRegister.SetActive(true);
            _incorrectEmailRegister.GetComponent<TMP_Text>().text = "There is already a registered user with such an email";
            return;
            }
        }
        else
        {
            _incorrectEmailRegister.SetActive(true);
            _incorrectEmailRegister.GetComponent<TMP_Text>().text = "incorrect email";
            return;
        }
        if(_passwordRegisterInputField.text.Length >= 6)
        {
            _incorrectPasswordRegister.SetActive(false);
        }
        else
        {
            Debug.Log("Пароль мнеьше 6 символов");
            _incorrectPasswordRegister.SetActive(true);
            return;
        }
        if(_passwordConfirmRegisterInputField.text == _passwordRegisterInputField.text)
        {
            _incorrectPasswordRegister.SetActive(false);
        }
        else
        {
            _incorrectPasswordConfirmRegister.SetActive(true);
            return;
        }
            User user = new User();
            user._email = _emailRegisterInputField.text;
            user._password = _passwordRegisterInputField.text;
            user._userName = _nicknameInputField.text;

            GameManager._instance.SaveUserDatabase(user);
            BackRegisterButton();
            MenuUpdate();
    }
    public void LoginUser()
    {
        string line = GameManager.AccountDataSearch(_emailLoginInputField.text);
            if(line != null)
            {
                string passwordDatabase = line.Split('"',15)[13];
                if(passwordDatabase == _passwordLoginInputField.text)
                {
                    User user = JsonUtility.FromJson<User>(line);
                    GameManager._instance._user = user;
                    _incorrectPasswordLogin.SetActive(false);
                    _incorrectEmailLogin.SetActive(false);
                    BackLoginButton();
                    MenuUpdate();
                    Debug.Log("Всё хорошо");
                    return;
                }
                else
                {
                    Debug.Log("пароль неверный");
                    _incorrectPasswordLogin.SetActive(true);
                    return;
                }
            }
            else
            {
                _incorrectEmailLogin.SetActive(true);
            }
    }

    private bool isValidEmail(string email)
    {
        try
        {
            var address = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void LogoutButton()
    {
        GameManager._instance._user = new User();
        MenuUpdate();
    }
    public void MenuUpdate()
    {
        _moneyCounterTextMenu.text = GameManager._instance._user._amountMoney.ToString();
        ShowingPurchasedSkins();
        ShowingPurchasedAbilities();
        int i;
        for(i = 0; i < User._purchasedSkins.Count;i++)
        {
            if(User._purchasedSkins[i] == User._numberSelectedSkin)
            {
                _characters.GetChild(i).GetChild(2).gameObject.SetActive(false);
                break;
            }
            if(i == User._purchasedSkins.Count - 1)
            {
                User._numberSelectedSkin = 0;
                Debug.Log("user.purchasedSkins.Count " + User._purchasedSkins.Count);
                i = 0;
                _characters.GetChild(GameManager._instance._user._numberSelectedSkin).GetChild(2).gameObject.SetActive(false);
                break;
            }
        }
        Debug.Log(i);
        SelectedSkin(i);
        if(GameManager._instance?._user._email != null)
        {
            _loginButton.SetActive(false);
            _registerButton.SetActive(false);
            _logoutButton.SetActive(true);
            _idText.text = "ID:" + GameManager._instance._user._id.ToString();
        }
        else
        {
            _loginButton.SetActive(true);
            _registerButton.SetActive(true);
            _logoutButton.SetActive(false);
            _idText.text = "ID:0";
        }
    }
    public void ShopUpdate()
    {
        _canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = GameManager._instance._user._amountMoney.ToString();
        for(int i = 3;i < User._purchasedSkins.Count;i++)
        {
            _listSkins.GetChild(User._purchasedSkins[i] - 3).GetChild(2).gameObject.SetActive(false);
            _listSkins.GetChild(User._purchasedSkins[i] - 3).GetChild(3).gameObject.SetActive(true);
            _listSkins.GetChild(User._purchasedSkins[i] - 3).GetChild(4).gameObject.SetActive(true);
            _listSkins.GetChild(User._purchasedSkins[i] - 3).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = (_skins._skins[User._purchasedSkins[i]]._price / 2).ToString();
        }
        for(int i = 4;i < User._purchasedAbilities.Count;i++)
        {
            Debug.Log("Выключенна кнопка" + _listAbilities.GetChild(User._purchasedAbilities[i] - 4).GetChild(1));
            _listAbilities.GetChild(User._purchasedAbilities[i] - 4).GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ScrolldarSound()
    {
        _scrollbarBackground.anchorMax = new Vector2(_scrollbarSound.value,1);
    }
    public void ExitGameButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SettingButton()
    {
        _settingPanel.SetActive(true);
    }
    public void SettingBackButton()
    {
        _settingPanel.SetActive(false);
    }
    public void BuySkin(int id)
    {
        if(User._amountMoney >= _skins._skins[id]._price)
        {
            User._amountMoney -= _skins._skins[id]._price;
            _canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = User._amountMoney.ToString();
            User._purchasedSkins.Add(id);
            User._purchasedSkins.Sort();
            GameManager._instance.SaveUserProgress();
            _listSkins.GetChild(User._purchasedSkins[id - 3]).GetChild(2).gameObject.SetActive(false);
            _listSkins.GetChild(User._purchasedSkins[id - 3]).GetChild(3).gameObject.SetActive(true);
            _listSkins.GetChild(User._purchasedSkins[id - 3]).GetChild(4).gameObject.SetActive(true);
            _listSkins.GetChild(User._purchasedSkins[id - 3]).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = (_skins._skins[id]._price / 2).ToString();
        }

    }
    public void BuyAbility(int id)
    {
        if(User._amountMoney >= _abilities._abilities[id]._price)
        {
            User._amountMoney -= _abilities._abilities[id]._price;
            _canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = User._amountMoney.ToString();
            User._purchasedAbilities.Add(id);
            User._purchasedAbilities.Sort();
            GameManager._instance.SaveUserProgress();
            _listAbilities.GetChild(User._purchasedSkins[id - 4]).GetChild(1).gameObject.SetActive(false);
        }

    }
    public void RefundSkin(int id)
    {
        idSelectedSkin = id;
        _refundPanel.SetActive(true);
    }
    public void SelectedSkin(int id)
    {
        Debug.Log(id);
        for(int i = 0;i < User._purchasedSkins.Count;i++)
        {
            if(User._purchasedSkins[i] == User._numberSelectedSkin)
            {
                _characters.GetChild(i).GetChild(2).gameObject.SetActive(true);
            }
        }
        Debug.Log(User._purchasedSkins[id]);
        User._numberSelectedSkin = User._purchasedSkins[id];
        GameManager._instance._skin = GameManager.DeserializeSkin(User._purchasedSkins[id]);
        Debug.Log(id);
        _characters.GetChild(id).GetChild(2).gameObject.SetActive(false);
    }
    public void SelectedAbility(int id)
    {
        for(int i = 0;i < User._purchasedAbilities.Count;i++)
        {
        _characters.GetChild(GameManager._instance._user._numberSelectedSkin).GetChild(2).gameObject.SetActive(true);
        User._numberSelectedSkin = id;
        GameManager._instance._skin = GameManager.DeserializeSkin(id);
        Debug.Log(id);
        _characters.GetChild(GameManager._instance._user._numberSelectedSkin).GetChild(2).gameObject.SetActive(false);
        }
    }
    public void ConfirmationSkinRefund()
    {
        User._amountMoney += _skins._skins[idSelectedSkin]._price / 2;
        _canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = User._amountMoney.ToString();
        User._purchasedSkins.Remove(idSelectedSkin);
        GameManager._instance.SaveUserProgress();
        _listSkins.GetChild(idSelectedSkin - 3).GetChild(2).gameObject.SetActive(true);
        _listSkins.GetChild(idSelectedSkin - 3).GetChild(3).gameObject.SetActive(false);
        _listSkins.GetChild(idSelectedSkin - 3).GetChild(4).gameObject.SetActive(false);
        _listSkins.GetChild(idSelectedSkin - 3).GetChild(4).gameObject.SetActive(false);
        _refundPanel.SetActive(false);
    }
    public void CancelRefund()
    {
        _refundPanel.SetActive(false);
    }
    public void PurchaseGameCurrency(int idSlot)
    {
        User._amountMoney += _donateSlots[idSlot];
        vipShopMoneyCounterText.text = User._amountMoney.ToString();
    }

    private void ShowingPurchasedSkins()
    {
        for(int i = 0;i< User._purchasedSkins.Count;i++)
        {
            GameObject skinCellCurrent = Instantiate(_skinCell,transform.position,Quaternion.identity,_characters);
            Skin skin = GameManager.DeserializeSkin(User._purchasedSkins[i]);
            TMP_Text attackDamage = skinCellCurrent.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text health = skinCellCurrent.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text stamina = skinCellCurrent.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text criticalDamage = skinCellCurrent.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text attackSpeed = skinCellCurrent.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text protection = skinCellCurrent.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<TMP_Text>();
            Image skinBody = skinCellCurrent.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            Image skinRightHand = skinCellCurrent.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            Image skinLeftHand = skinCellCurrent.transform.GetChild(1).GetChild(2).GetComponent<Image>();
            attackDamage.text = skin._attackDamage.ToString();
            health.text = skin._health.ToString();
            stamina.text = skin._stamina.ToString();
            criticalDamage.text = skin._criticalDamage.ToString();
            attackSpeed.text = skin._attackSpeed.ToString();
            protection.text = skin._protection.ToString();
            skinBody.sprite = skin._spriteSkinBody;
            skinRightHand.sprite = skin._spriteSkinHand;
            skinLeftHand.sprite = skin._spriteSkinHand;
        }
    }
    private void ShowingPurchasedAbilities()
    {
        for(int i = 0;i < User._purchasedAbilities.Count;i++)
        {
            int purchasedAbility = User._purchasedAbilities[i];
            if(purchasedAbility != User._seletedAbilities[0] && purchasedAbility != User._seletedAbilities[1] && purchasedAbility != User._seletedAbilities[2] && purchasedAbility != User._seletedAbilities[3])
            {
            GameObject abilityCellCurrent = Instantiate(_abilityCell,transform.position,Quaternion.identity,_abilitiesList);
            Ability ability = GameManager.DeserializeAbility(User._purchasedAbilities[i]);
            Image abilityImage = abilityCellCurrent.transform.GetChild(0).GetComponent<Image>();
            abilityCellCurrent.GetComponent<AbilitySelectUI>()._abilityEnum = ability._abilityType;
            abilityImage.sprite = ability._spriteAbility;
            abilityCellCurrent.GetComponent<AbilitySelectUI>()._abilityEnum = ability._abilityType;
            }
            
        }
        for(int i = 0;i < User._seletedAbilities.Length;i++)
        {
            Ability ability = GameManager.DeserializeAbility(User._seletedAbilities[i]);
            Transform abilityUI = _canvasTransform.GetChild(4).GetChild(0).GetChild(i);
            abilityUI.GetChild(0).GetComponent<Image>().sprite = ability._spriteAbility;
            abilityUI.GetComponent<AbilitySelectUI>()._abilityEnum = ability._abilityType;
            abilityUI.GetComponent<AbilitySelectUI>()._numberSelectedAbility = i;
        }
    }
}
[Serializable]
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
[Serializable]
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