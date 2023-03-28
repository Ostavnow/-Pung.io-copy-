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
public class MainUIHandler : MonoBehaviour
{
    [SerializeField]
    public List<SkinUIElements> skinUIElements = new List<SkinUIElements>();
    [SerializeField]
    public List<AbilityUIElements> abilityUIElements = new List<AbilityUIElements>();
    [HideInInspector]
    private Skins skins;
    private Abilities abilities;
    public List<int> donateSlots = new List<int>(){0,10000,50000,0,200000,500000};
    // Components in the Game window
    [HideInInspector] public Image healthStripe;
    [HideInInspector] public Image staminaStripe;
    [HideInInspector] public Image XPStripe;
    [HideInInspector] public Image firstAbilityImage;
    [HideInInspector] public Image secondAbilityImage;
    [HideInInspector] public Image thirdAbilityImage;
    [HideInInspector] public Image fourthAbilityImage;
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
    [HideInInspector] public TMP_Text moneyCounterTextMenu;
    [HideInInspector] public TMP_Text moneyCounterTextGame;
    [HideInInspector] public Transform loginPanel;
    [HideInInspector] public Transform registerPanel;
    private GameObject settingPanel;
    private RectTransform scrollbarBackground;
    private Scrollbar scrollbarSound;
    // Components in the menu window
    private TMP_InputField emailLoginInputField;
    private GameObject incorrectEmailLogin;
    private TMP_InputField passwordLoginInputField;
    private GameObject incorrectPasswordLogin;
    private TMP_InputField emailRegisterInputField;
    private GameObject incorrectEmailRegister;
    private TMP_InputField passwordRegisterInputField;
    private GameObject incorrectPasswordRegister;
    private TMP_InputField passwordConfirmRegisterInputField;
    private GameObject incorrectPasswordConfirmRegister;
    private Image imageButtonLoginPanel;
    private Image imageButtonRegisterPanel;
    private GameObject loginButton;
    private GameObject registerButton;
    private GameObject logoutButton;
    private TMP_InputField nicknameInputField;
    private TMP_Text idText;
    [SerializeField] private GameObject skinCell;
    [SerializeField] private GameObject abilityCell;
    [HideInInspector] public Transform background;
    [HideInInspector] public Transform canvasTransform;
    private Transform listSkins;
    private Transform listAbilities;
    private GameObject refundPanel;
    private Transform characters;
    private Transform abilitiesList;
    public Transform firstAbilitySelected;
    public Transform secondAbilitySelected;
    public Transform thirdAbilitySelected;
    public Transform fourthAbilitySelected;
    private User user
    {
        get{return GameManager.instance?.user;}
    }
    private int idSelectedSkin;
    private TMP_Text vipShopMoneyCounterText;
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
            listAbilities = GameObject.Find("Canvas").transform.GetChild(7).GetChild(0).GetChild(1).GetChild(0).transform;
        }
    }
    #endif
    private void Awake()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        if(SceneManager.GetActiveScene().name == "Game")
        {
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
            moneyCounterTextGame = canvasTransform.GetChild(9).GetChild(0).GetComponent<TMP_Text>();
            scrollbarBackground = canvasTransform.GetChild(13).GetChild(1).GetChild(0).GetComponent<RectTransform>();
            scrollbarSound = canvasTransform.GetChild(13).GetChild(1).GetComponent<Scrollbar>();
            settingPanel = canvasTransform.GetChild(13).gameObject;
            firstAbilityImage = canvasTransform.GetChild(4).GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>();
            secondAbilityImage = canvasTransform.GetChild(4).GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>();
            thirdAbilityImage = canvasTransform.GetChild(4).GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>();
            fourthAbilityImage = canvasTransform.GetChild(4).GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>();
        }
        else if(SceneManager.GetActiveScene().name == "Menu")
        {
            background = canvasTransform.GetChild(7);
            loginPanel = canvasTransform.GetChild(8);
            emailLoginInputField = loginPanel.GetChild(0).GetComponent<TMP_InputField>();
            incorrectEmailLogin = loginPanel.GetChild(1).gameObject;
            passwordLoginInputField = loginPanel.GetChild(2).GetComponent<TMP_InputField>();
            incorrectPasswordLogin = loginPanel.GetChild(3).gameObject;
            imageButtonLoginPanel = loginPanel.GetChild(9).GetComponent<Image>();
            registerPanel = canvasTransform.GetChild(9);
            emailRegisterInputField = registerPanel.GetChild(2).GetComponent<TMP_InputField>();
            incorrectEmailRegister = registerPanel.GetChild(4).gameObject;
            passwordRegisterInputField = registerPanel.GetChild(5).GetComponent<TMP_InputField>();
            incorrectPasswordRegister = registerPanel.GetChild(7).gameObject;
            passwordConfirmRegisterInputField = registerPanel.GetChild(8).GetComponent<TMP_InputField>();
            incorrectPasswordConfirmRegister = registerPanel.GetChild(10).gameObject;
            imageButtonRegisterPanel = registerPanel.GetChild(11).GetComponent<Image>();
            loginButton = canvasTransform.GetChild(3).GetChild(3).gameObject;
            registerButton = canvasTransform.GetChild(3).GetChild(4).gameObject;
            logoutButton = canvasTransform.GetChild(3).GetChild(5).gameObject;
            moneyCounterTextMenu = canvasTransform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            nicknameInputField = canvasTransform.GetChild(3).GetChild(6).GetChild(0).GetComponent<TMP_InputField>();
            idText = canvasTransform.GetChild(3).GetChild(6).GetChild(1).GetComponent<TMP_Text>();
            characters = canvasTransform.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0);
            characters = canvasTransform.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0);
            abilitiesList = canvasTransform.GetChild(5).GetChild(1).GetChild(1).GetChild(0);
            firstAbilitySelected = canvasTransform.GetChild(4).GetChild(0).GetChild(0);
            secondAbilitySelected = canvasTransform.GetChild(4).GetChild(0).GetChild(1);
            thirdAbilitySelected = canvasTransform.GetChild(4).GetChild(0).GetChild(2);
            fourthAbilitySelected = canvasTransform.GetChild(4).GetChild(0).GetChild(3);
        }
        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            skins = FindObjectOfType<Skins>();
            abilities = FindObjectOfType<Abilities>();
            refundPanel = canvasTransform.GetChild(8).gameObject;
            listAbilities = GameObject.Find("Canvas").transform.GetChild(7).GetChild(0).GetChild(1).GetChild(0).transform;
            listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        }
        else if(SceneManager.GetActiveScene().name == "Vip shop")
        {
            vipShopMoneyCounterText = canvasTransform.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
            vipShopMoneyCounterText.text = user.amountMoney.ToString();
        }
    }
    private void Start()

    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            MenuUpdate();
            canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(0);});
            canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(1);});
            canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(2);});
            if(user.purchasedSkins.Count > 4)
            {
                canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(3).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(3);});
            }
            if(user.purchasedSkins.Count > 5)
            {
                canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(4).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(4);});
            }
            if(user.purchasedSkins.Count == 6)
            {
                canvasTransform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(5).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(5);});
            }
        }
        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            ShopUpdate();
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            Debug.Log(user.seletedAbilities[0]);
            Debug.Log(user.seletedAbilities[1]);
            Debug.Log(user.seletedAbilities[2]);
            Debug.Log(user.seletedAbilities[3]);
            firstAbilityImage.sprite = GameManager.DeserializeAbility(user.seletedAbilities[0]).spriteAbility;
            secondAbilityImage.sprite = GameManager.DeserializeAbility(user.seletedAbilities[1]).spriteAbility;
            thirdAbilityImage.sprite = GameManager.DeserializeAbility(user.seletedAbilities[2]).spriteAbility;
            fourthAbilityImage.sprite = GameManager.DeserializeAbility(user.seletedAbilities[3]).spriteAbility;
        }
    }
    //Methods for working with the Skins class
    #if UNITY_EDITOR
    public void AddSkinsToList(ReorderableList skins,GameObject prefabSkinCell)
    {
        Debug.Log(skins.serializedProperty);
        int index = skins.serializedProperty.arraySize;
        skins.serializedProperty.arraySize++;
        index = skins.serializedProperty.arraySize;
        Debug.Log(index);
        skins.index = index;
        if(index > 3)
        {
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + index) == null)
        {
            GameObject currentSkinCell = Instantiate(prefabSkinCell,Vector3.zero,Quaternion.identity,listSkins);
            Button buyButton = currentSkinCell.transform.GetChild(2).GetComponent<Button>();
            UnityAction<int> actionBuyButton = new UnityAction<int>(BuySkin);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            Button refundButton = currentSkinCell.transform.GetChild(4).GetComponent<Button>();
            UnityAction<int> actionRefundButton = new UnityAction<int>(RefundSkin);
            UnityEventTools.AddIntPersistentListener(refundButton.onClick,actionRefundButton,(index - 1));
            currentSkinCell.name = "Skin " + (index);
        }       
                SkinUIElements skinUIElement = new SkinUIElements(
                    listSkins.GetChild(index - 4).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(0).GetChild(3).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(0).GetChild(4).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(0).GetChild(5).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 4).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(index - 4).GetChild(1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(index - 4).GetChild(1).GetChild(0).GetComponent<Image>());
                skinUIElements.Add(skinUIElement);
        }
    }
    public void RemoveSkinToList(ReorderableList skins)
    {
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + skins.serializedProperty.arraySize) != null)
        {
            DestroyImmediate(listSkins.GetChild(skins.serializedProperty.arraySize - 4).gameObject);
        }
        skins.serializedProperty.arraySize--;
        skinUIElements.RemoveAt(skinUIElements.Count - 1);
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
            GameObject currentSkinCell = Instantiate(prefabAbilityCell,Vector3.zero,Quaternion.identity,listAbilities);
            Button buyButton = currentSkinCell.transform.GetChild(1).GetComponent<Button>();
            UnityAction<int> actionBuyButton = new UnityAction<int>(BuyAbility);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            currentSkinCell.name = "ability " + (index);
        }       
                AbilityUIElements abilityUIElement = new AbilityUIElements(
                    listAbilities.GetChild(index - 5).GetChild(1).GetChild(0).GetComponent<Image>(),
                    listAbilities.GetChild(index - 5).GetChild(1).GetChild(0).GetComponent<TMP_Text>());
                abilityUIElements.Add(abilityUIElement);
        }
    }
    public void RemoveAbilityToList(ReorderableList abilities)
    {
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Abilities/ability " + abilities.serializedProperty.arraySize) != null)
        {
            DestroyImmediate(listAbilities.GetChild(abilities.serializedProperty.arraySize - 5).gameObject);
        }
        abilities.serializedProperty.arraySize--;
        abilityUIElements.RemoveAt(abilityUIElements.Count - 1);
    }
    #endif
    public void RegisterButton()
    {
        background.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(true);
    }
    public void LoginButton()
    {
        background.gameObject.SetActive(true);
        loginPanel.gameObject.SetActive(true);
    }
    public void BackLoginButton()
    {
        background.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(false);
    }
    public void BackRegisterButton()
    {
        background.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(false);
    }
    public void RegisterUser()
    {
        if(isValidEmail(emailRegisterInputField.text))
        {
            if(GameManager.CheckingExistingEmail(emailRegisterInputField.text))
            {
                incorrectEmailRegister.SetActive(false);
            }
            else
            {
            incorrectEmailRegister.SetActive(true);
            incorrectEmailRegister.GetComponent<TMP_Text>().text = "There is already a registered user with such an email";
            return;
            }
        }
        else
        {
            incorrectEmailRegister.SetActive(true);
            incorrectEmailRegister.GetComponent<TMP_Text>().text = "incorrect email";
            return;
        }
        if(passwordRegisterInputField.text.Length >= 6)
        {
            incorrectPasswordRegister.SetActive(false);
        }
        else
        {
            Debug.Log("Пароль мнеьше 6 символов");
            incorrectPasswordRegister.SetActive(true);
            return;
        }
        if(passwordConfirmRegisterInputField.text == passwordRegisterInputField.text)
        {
            incorrectPasswordRegister.SetActive(false);
        }
        else
        {
            incorrectPasswordConfirmRegister.SetActive(true);
            return;
        }
            User user = new User();
            user.email = emailRegisterInputField.text;
            user.password = passwordRegisterInputField.text;
            user.userName = nicknameInputField.text;

            GameManager.instance.SaveUserDatabase(user);
            BackRegisterButton();
            MenuUpdate();
    }
    public void LoginUser()
    {
        string line = GameManager.AccountDataSearch(emailLoginInputField.text);
            if(line != null)
            {
                string passwordDatabase = line.Split('"',15)[13];
                if(passwordDatabase == passwordLoginInputField.text)
                {
                    User user = JsonUtility.FromJson<User>(line);
                    GameManager.instance.user = user;
                    incorrectPasswordLogin.SetActive(false);
                    incorrectEmailLogin.SetActive(false);
                    BackLoginButton();
                    MenuUpdate();
                    Debug.Log("Всё хорошо");
                    return;
                }
                else
                {
                    Debug.Log("пароль неверный");
                    incorrectPasswordLogin.SetActive(true);
                    return;
                }
            }
            else
            {
                incorrectEmailLogin.SetActive(true);
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
        GameManager.instance.user = new User();

        MenuUpdate();
    }
    public void MenuUpdate()
    {
        moneyCounterTextMenu.text = GameManager.instance.user.amountMoney.ToString();
        ShowingPurchasedSkins();
        ShowingPurchasedAbilities();
        int i;
        for(i = 0; i < user.purchasedSkins.Count;i++)
        {
            if(user.purchasedSkins[i] == user.numberSelectedSkin)
            {
                characters.GetChild(i).GetChild(2).gameObject.SetActive(false);
                break;
            }
            if(i == user.purchasedSkins.Count - 1)
            {
                user.numberSelectedSkin = 0;
                Debug.Log("user.purchasedSkins.Count " + user.purchasedSkins.Count);
                i = 0;
                characters.GetChild(GameManager.instance.user.numberSelectedSkin).GetChild(2).gameObject.SetActive(false);
                break;
            }
        }
        Debug.Log(i);
        SelectedSkin(i);
        if(GameManager.instance?.user.email != null)
        {
            loginButton.SetActive(false);
            registerButton.SetActive(false);
            logoutButton.SetActive(true);
            idText.text = "ID:" + GameManager.instance.user.id.ToString();
        }
        else
        {
            loginButton.SetActive(true);
            registerButton.SetActive(true);
            logoutButton.SetActive(false);
            idText.text = "ID:0";
        }
    }
    public void ShopUpdate()
    {
        canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = GameManager.instance.user.amountMoney.ToString();
        for(int i = 3;i < user.purchasedSkins.Count;i++)
        {
            listSkins.GetChild(user.purchasedSkins[i] - 3).GetChild(2).gameObject.SetActive(false);
            listSkins.GetChild(user.purchasedSkins[i] - 3).GetChild(3).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[i] - 3).GetChild(4).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[i] - 3).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = (skins.skins[user.purchasedSkins[i]].price / 2).ToString();
        }
        for(int i = 4;i < user.purchasedAbilities.Count;i++)
        {
            Debug.Log("Выключенна кнопка" + listAbilities.GetChild(user.purchasedAbilities[i] - 4).GetChild(1));
            listAbilities.GetChild(user.purchasedAbilities[i] - 4).GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ScrolldarSound()
    {
        scrollbarBackground.anchorMax = new Vector2(scrollbarSound.value,1);
    }
    public void ExitGameButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SettingButton()
    {
        settingPanel.SetActive(true);
    }
    public void SettingBackButton()
    {
        settingPanel.SetActive(false);
    }
    public void BuySkin(int id)
    {
        if(user.amountMoney >= skins.skins[id].price)
        {
            user.amountMoney -= skins.skins[id].price;
            canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = user.amountMoney.ToString();
            user.purchasedSkins.Add(id);
            user.purchasedSkins.Sort();
            GameManager.instance.SaveUserProgress();
            listSkins.GetChild(user.purchasedSkins[id - 3]).GetChild(2).gameObject.SetActive(false);
            listSkins.GetChild(user.purchasedSkins[id - 3]).GetChild(3).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[id - 3]).GetChild(4).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[id - 3]).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = (skins.skins[id].price / 2).ToString();
        }

    }
    public void BuyAbility(int id)
    {
        if(user.amountMoney >= abilities.abilities[id].price)
        {
            user.amountMoney -= abilities.abilities[id].price;
            canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = user.amountMoney.ToString();
            user.purchasedAbilities.Add(id);
            user.purchasedAbilities.Sort();
            GameManager.instance.SaveUserProgress();
            listAbilities.GetChild(user.purchasedSkins[id - 4]).GetChild(1).gameObject.SetActive(false);
        }

    }
    public void RefundSkin(int id)
    {
        idSelectedSkin = id;
        refundPanel.SetActive(true);
    }
    public void SelectedSkin(int id)
    {
        Debug.Log(id);
        for(int i = 0;i < user.purchasedSkins.Count;i++)
        {
            if(user.purchasedSkins[i] == user.numberSelectedSkin)
            {
                 characters.GetChild(i).GetChild(2).gameObject.SetActive(true);
            }
        }
        Debug.Log(user.purchasedSkins[id]);
        user.numberSelectedSkin = user.purchasedSkins[id];
        GameManager.instance.skin = GameManager.DeserializeSkin(user.purchasedSkins[id]);
        Debug.Log(id);
        characters.GetChild(id).GetChild(2).gameObject.SetActive(false);
    }
    public void SelectedAbility(int id)
    {
        for(int i = 0;i < user.purchasedAbilities.Count;i++)
        {
        characters.GetChild(GameManager.instance.user.numberSelectedSkin).GetChild(2).gameObject.SetActive(true);
        user.numberSelectedSkin = id;
        GameManager.instance.skin = GameManager.DeserializeSkin(id);
        Debug.Log(id);
        characters.GetChild(GameManager.instance.user.numberSelectedSkin).GetChild(2).gameObject.SetActive(false);
        }
    }
    public void ConfirmationSkinRefund()
    {
        user.amountMoney += skins.skins[idSelectedSkin].price / 2;
        canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = user.amountMoney.ToString();
        user.purchasedSkins.Remove(idSelectedSkin);
        GameManager.instance.SaveUserProgress();
        listSkins.GetChild(idSelectedSkin - 3).GetChild(2).gameObject.SetActive(true);
        listSkins.GetChild(idSelectedSkin - 3).GetChild(3).gameObject.SetActive(false);
        listSkins.GetChild(idSelectedSkin - 3).GetChild(4).gameObject.SetActive(false);
        listSkins.GetChild(idSelectedSkin - 3).GetChild(4).gameObject.SetActive(false);
        refundPanel.SetActive(false);
    }
    public void CancelRefund()
    {
        refundPanel.SetActive(false);
    }
    public void PurchaseGameCurrency(int idSlot)
    {
        user.amountMoney += donateSlots[idSlot];
        vipShopMoneyCounterText.text = user.amountMoney.ToString();
    }

    private void ShowingPurchasedSkins()
    {
        for(int i = 0;i<user.purchasedSkins.Count;i++)
        {
            GameObject skinCellCurrent = Instantiate(skinCell,transform.position,Quaternion.identity,characters);
            Skin skin = GameManager.DeserializeSkin(user.purchasedSkins[i]);
            TMP_Text attackDamage = skinCellCurrent.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text health = skinCellCurrent.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text stamina = skinCellCurrent.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text criticalDamage = skinCellCurrent.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text attackSpeed = skinCellCurrent.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text protection = skinCellCurrent.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<TMP_Text>();
            Image skinBody = skinCellCurrent.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            Image skinRightHand = skinCellCurrent.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            Image skinLeftHand = skinCellCurrent.transform.GetChild(1).GetChild(2).GetComponent<Image>();
            attackDamage.text = skin.attackDamage.ToString();
            health.text = skin.health.ToString();
            stamina.text = skin.stamina.ToString();
            criticalDamage.text = skin.criticalDamage.ToString();
            attackSpeed.text = skin.attackSpeed.ToString();
            protection.text = skin.protection.ToString();
            skinBody.sprite = skin.spriteSkinBody;
            skinRightHand.sprite = skin.spriteSkinHand;
            skinLeftHand.sprite = skin.spriteSkinHand;
        }
    }
    private void ShowingPurchasedAbilities()
    {
        for(int i = 0;i < user.purchasedAbilities.Count;i++)
        {
            int purchasedAbility = user.purchasedAbilities[i];
            if(purchasedAbility != user.seletedAbilities[0] && purchasedAbility != user.seletedAbilities[1] && purchasedAbility != user.seletedAbilities[2] && purchasedAbility != user.seletedAbilities[3])
            {
            GameObject abilityCellCurrent = Instantiate(abilityCell,transform.position,Quaternion.identity,abilitiesList);
            Ability ability = GameManager.DeserializeAbility(user.purchasedAbilities[i]);
            Image abilityImage = abilityCellCurrent.transform.GetChild(0).GetComponent<Image>();
            abilityCellCurrent.GetComponent<AbilitySelectUI>().abilityEnum = ability.abilityType;
            abilityImage.sprite = ability.spriteAbility;
            abilityCellCurrent.GetComponent<AbilitySelectUI>().abilityEnum = ability.abilityType;
            }
            
        }
        for(int i = 0;i < user.seletedAbilities.Length;i++)
        {
            Ability ability = GameManager.DeserializeAbility(user.seletedAbilities[i]);
            Transform abilityUI = canvasTransform.GetChild(4).GetChild(0).GetChild(i);
            abilityUI.GetChild(0).GetComponent<Image>().sprite = ability.spriteAbility;
            abilityUI.GetComponent<AbilitySelectUI>().abilityEnum = ability.abilityType;
            abilityUI.GetComponent<AbilitySelectUI>().numberSelectedAbility = i;
        }
    }
}
[Serializable]
public struct SkinUIElements
{
        [SerializeField]
        public TMP_Text attackDamageText;
        [SerializeField]
        public TMP_Text healthText;
        [SerializeField]
        public TMP_Text staminaText;
        [SerializeField]
        public TMP_Text criticalDamageText;
        [SerializeField]
        public TMP_Text attackSpeedText;
        [SerializeField]
        public TMP_Text protectionText;
        [SerializeField]
        public TMP_Text priceText;
        [SerializeField]
        public Image skinBodyImage;
        [SerializeField]
        public Image skinRightHandImage;
        [SerializeField]
        public Image skinLeftHandImage;
        public SkinUIElements(TMP_Text attackDamageText,TMP_Text healthText,
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
[Serializable]
public struct AbilityUIElements
{
    [SerializeField]
    public Image imageAbility;
    [SerializeField]
    public TMP_Text priceText;
    public AbilityUIElements(Image imageAbility,TMP_Text priceText)
    {
        this.imageAbility = imageAbility;
        this.priceText = priceText;
    }
}