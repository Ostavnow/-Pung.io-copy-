using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UserAuthentication : MonoBehaviour
{
    [SerializeField] private Transform _registerPanel;
    [SerializeField] private Transform _loginPanel;
    [SerializeField] private TMP_InputField _emailLoginInputField;
    [SerializeField] private GameObject _incorrectEmailLogin;
    [SerializeField] private TMP_InputField _passwordLoginInputField;
    [SerializeField] private GameObject _incorrectPasswordLogin;
    [SerializeField] private TMP_InputField _emailRegisterInputField;
    [SerializeField] private GameObject _incorrectEmailRegister;
    [SerializeField] private TMP_InputField _passwordRegisterInputField;
    [SerializeField] private GameObject _incorrectPasswordRegister;
    [SerializeField] private TMP_InputField _passwordConfirmRegisterInputField;
    [SerializeField] private GameObject _incorrectPasswordConfirmRegister;
    [SerializeField] private Image _imageButtonLoginPanel;
    [SerializeField] private Image _imageButtonRegisterPanel;
    [SerializeField] private TMP_InputField _nicknameInputField;
    [SerializeField] private Transform _background;
    [SerializeField] private GameObject _loginButton;
    [SerializeField] private GameObject _registerButton;
    [SerializeField] private GameObject _logoutButton;
    [SerializeField] private TMP_Text _idText;
    
    [SerializeField] private DataManager _dataManager;
    void Start()
    {
        AuthorizationVerification();
        _dataManager = FindObjectOfType<DataManager>();
    }

    private void AuthorizationVerification()
    {
        if(_dataManager._user._email != null)
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
            if(_dataManager.CheckingExistingEmail(_emailRegisterInputField.text))
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

            _dataManager.SaveUserDatabase(user);
            BackRegisterButton();
            // MenuUpdate();
    }
    public void LoginUser()
    {
        string line = _dataManager.AccountDataSearch(_emailLoginInputField.text);
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
                    // MenuUpdate();
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
        // MenuUpdate();
    }
}
