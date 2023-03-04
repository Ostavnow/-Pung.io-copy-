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
public class MainUIHandler : MonoBehaviour
{
    public List<SkinCharacteristicsUI> skinCharacteristicsUI = new List<SkinCharacteristicsUI>();
    #if UNITY_EDITOR
    public static SerializedObject serializedObject;
    #endif
    [HideInInspector]
    public Skins c_Skins;
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
    [HideInInspector] public Transform Background;
    [HideInInspector] public Transform canvasTransform;
    #if UNITY_EDITOR
    public void OnValidate()
    {
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            serializedObject = new SerializedObject(this);
            c_Skins = FindObjectOfType<Skins>();
            Debug.Log(serializedObject);
        }
    }
    #endif
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            canvasTransform = GameObject.Find("Canvas").transform;
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
            canvasTransform = GameObject.Find("Canvas").transform;
            Background = canvasTransform.GetChild(7);
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
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            MenuUpdate();           
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            // moneyCounterTextGame.text = GameManager.instance.user.amountMoney.ToString();
        }
    }
    #if UNITY_EDITOR
    public void UIAddSkinsToList(IList skins,GameObject prefabSkinCell)
    {
        Transform listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        if(GameObject.Find("Canvas/skins panel/Background/Scroll Area/Skins/Skin " + skins.Count) == null)
        {
            Skins.skins.Add(new Skin());
            GameObject currentSkinCell = Instantiate(prefabSkinCell,Vector3.zero,Quaternion.identity,listSkins);
            currentSkinCell.name = "Skin " + (skins.Count);
            Debug.Log(skins.Count);
        }       
                Skin skin = (Skin) skins[skins.Count - 1];
                skin.skinIndex = skins.Count - 1;
                SkinCharacteristicsUI skinCharacteristicUI = new SkinCharacteristicsUI(
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(3).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(4).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(0).GetChild(5).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(2).GetChild(0).GetComponent<TMP_Text>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(1).GetChild(1).GetComponent<Image>(),
                    listSkins.GetChild(skins.Count - 1).GetChild(1).GetChild(0).GetComponent<Image>());
                skinCharacteristicsUI.Add(skinCharacteristicUI);
                serializedObject = new SerializedObject(this);
                Debug.Log("Создалась ячейка скина");
    }
    public void UIRemoveSkinToList(ReorderableList skins)
    {
        Transform listSkins = GameObject.Find("Canvas").transform.GetChild(6).GetChild(0).GetChild(1).GetChild(0).transform;
        Debug.Log(skins.list.Count);
        if(GameObject.Find("Canvas/skins panel/Background/scrollbar/Scroll Area/Skins/Skin " + skins.list.Count) != null)
        {
            Destroy(listSkins.GetChild(skins.list.Count));
        }
        Skins.skins.Remove(Skins.skins[Skins.skins.Count - 1]);
        // if(skinCharacteristicsUI.Count)
        Debug.Log(skinCharacteristicsUI.Count);
        skinCharacteristicsUI.Remove(skinCharacteristicsUI[skinCharacteristicsUI.Count - 1]);
    }
    #endif
    public void RegisterButton()
    {
        Background.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(true);
    }
    public void LoginButton()
    {
        Background.gameObject.SetActive(true);
        loginPanel.gameObject.SetActive(true);
    }
    public void BackLoginButton()
    {
        Background.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(false);
    }
    public void BackRegisterButton()
    {
        Background.gameObject.SetActive(false);
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
        GameManager.instance.user = null;
        MenuUpdate();
    }
    public void MenuUpdate()
    {
        if(GameManager.instance?.user != null)
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
}
[Serializable]
public struct SkinCharacteristicsUI
{
        [SerializeField]
        private TMP_Text attackDamageText;
        [SerializeField]
        private TMP_Text healthText;
        [SerializeField]
        private TMP_Text staminaText;
        [SerializeField]
        private TMP_Text criticalDamageText;
        [SerializeField]
        private TMP_Text attackSpeedText;
        [SerializeField]
        private TMP_Text protectionText;
        [SerializeField]
        private TMP_Text priceText;
        [SerializeField]
        private Image skinBodyImage;
        [SerializeField]
        private Image skinRightHandImage;
        [SerializeField]
        private Image skinLeftHandImage;
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
