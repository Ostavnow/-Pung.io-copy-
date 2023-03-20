using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Events;
using UnityEditor.Events;

[Serializable]
public class MainUIHandler : MonoBehaviour
{
    [SerializeField]
    public List<SkinCharacteristicsUI> skinCharacteristicsUI = new List<SkinCharacteristicsUI>();
    [HideInInspector]
    public Skins skins;
    // Components in the Game window
    [HideInInspector] public Image healthStripe;
    [HideInInspector] public Image staminaStripe;
    [HideInInspector] public Image XPStripe;
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
    [HideInInspector] public Transform background;
    [HideInInspector] public Transform canvasTransform;
    private Transform listSkins;
    private GameObject refundPanel;
    private int idSelectedSkin;
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
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
        }
        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            skins = FindObjectOfType<Skins>();
            refundPanel = canvasTransform.GetChild(8).gameObject;
            listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        }
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            MenuUpdate();  
        }
        else if(SceneManager.GetActiveScene().name == "Shop")
        {
            ShopUpdate();
        }
    }
    #if UNITY_EDITOR
    public void UIAddSkinsToList(ReorderableList skins,GameObject prefabSkinCell)
    {
        Debug.Log(skins.serializedProperty);
        int index = skins.serializedProperty.arraySize;
        skins.serializedProperty.arraySize++;
        index = skins.serializedProperty.arraySize;
        skins.index = index;
        Transform listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + index) == null)
        {
            GameObject currentSkinCell = Instantiate(prefabSkinCell,Vector3.zero,Quaternion.identity,listSkins);
            Button buyButton = currentSkinCell.transform.GetChild(2).GetComponent<Button>();
            UnityAction<int> actionBuyButton = new UnityAction<int>(BuySkin);
            UnityEventTools.AddIntPersistentListener(buyButton.onClick,actionBuyButton,(index - 1));
            currentSkinCell.name = "Skin " + (index);
        }       
                SkinCharacteristicsUI skinCharacteristicUI = new SkinCharacteristicsUI(
                    listSkins.GetChild(index - 1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(0).GetChild(3).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(0).GetChild(4).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(0).GetChild(5).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(index - 1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(index - 1).GetChild(1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(index - 1).GetChild(1).GetChild(0).GetComponent<Image>());
                skinCharacteristicsUI.Add(skinCharacteristicUI);
    }
    public void UIRemoveSkinToList(ReorderableList skins)
    {
        Transform listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + skins.serializedProperty.arraySize) != null)
        {
            DestroyImmediate(listSkins.GetChild(skins.serializedProperty.arraySize - 1).gameObject);
        }
        skins.serializedProperty.arraySize--;
        skinCharacteristicsUI.Remove(skinCharacteristicsUI[skinCharacteristicsUI.Count - 1]);
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
        ShovingCurrentSkins();
        if(GameManager.instance?.user.userName != "")
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
        User user = GameManager.instance.user;
        for(int i = 0;i < user.purchasedSkins.Count;i++)
        {
            Button refundButton = listSkins.GetChild(user.purchasedSkins[i]).GetChild(4).GetComponent<Button>();
            UnityAction<int> actionRefundButton = new UnityAction<int>(SelectedSkin);
            UnityEventTools.AddIntPersistentListener(refundButton.onClick,actionRefundButton,(user.purchasedSkins[i]));
            listSkins.GetChild(user.purchasedSkins[i]).GetChild(2).gameObject.SetActive(false);
            listSkins.GetChild(user.purchasedSkins[i]).GetChild(3).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[i]).GetChild(4).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[i]).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = (skins.skins[user.purchasedSkins[i]].price / 2).ToString();
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
        Debug.Log(id);
        Debug.Log(skins.skins[id].price);
        User user = GameManager.instance.user;
        if(GameManager.instance.user.amountMoney >= skins.skins[id].price)
        {
            user.amountMoney -= skins.skins[id].price;
            canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = GameManager.instance.user.amountMoney.ToString();
            user.purchasedSkins.Add(id);
            GameManager.instance.SaveUserProgress();
            Button refundButton = listSkins.GetChild(user.purchasedSkins[id]).GetChild(4).GetComponent<Button>();
            UnityAction<int> actionRefundButton = new UnityAction<int>(SelectedSkin);
            UnityEventTools.AddIntPersistentListener(refundButton.onClick,actionRefundButton,(user.purchasedSkins[id]));
            listSkins.GetChild(user.purchasedSkins[id]).GetChild(2).gameObject.SetActive(false);
            listSkins.GetChild(user.purchasedSkins[id]).GetChild(3).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[id]).GetChild(4).gameObject.SetActive(true);
            listSkins.GetChild(user.purchasedSkins[id]).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = (skins.skins[user.purchasedSkins[id]].price / 2).ToString();
        }
    }
    public void SelectedSkin(int id)
    {
        idSelectedSkin = id;
        refundPanel.SetActive(true);
    }
    public void RefundSkin()
    {
        GameManager.instance.user.amountMoney += skins.skins[idSelectedSkin].price / 2;
        canvasTransform.GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = GameManager.instance.user.amountMoney.ToString();
        GameManager.instance.user.purchasedSkins.Remove(idSelectedSkin);
        GameManager.instance.SaveUserProgress();
        listSkins.GetChild(idSelectedSkin).GetChild(2).gameObject.SetActive(true);
        listSkins.GetChild(idSelectedSkin).GetChild(3).gameObject.SetActive(false);
        listSkins.GetChild(idSelectedSkin).GetChild(4).gameObject.SetActive(false);
        refundPanel.SetActive(false);
    }
    public void CancelRefund()
    {
        refundPanel.SetActive(false);
    }

    private void ShovingCurrentSkins()
    {
        Transform characters = GameObject.Find("Canvas").transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0);
        User user = GameManager.instance.user;
        for(int i = 0;i<user.purchasedSkins.Count;i++)
        {
            GameObject skinCellCurrent = Instantiate(skinCell,transform.position,Quaternion.identity,characters);
            Skin skin = GameManager.DeserializeSkin(user.purchasedSkins[i]);
            Debug.Log(skin.price);
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
}
[Serializable]
public struct SkinCharacteristicsUI
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
        public SkinCharacteristicsUI(TMP_Text attackDamageText,TMP_Text healthText,
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
