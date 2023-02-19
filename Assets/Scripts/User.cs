using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class User
{
    public string userName;
    private string p_email;
    public string email
    {
        get{return p_email;}
        set
        {
            if(isValidEmail(value))
            {
                p_email = value;
            }
        }
    }
    private string p_password;
    public string password
    {
        get{return p_password;}
        set
        {
            if(value.Length <= 6)
            {
                p_password = value;
            }
        }
    }
    public int amountMoney = 0;
    public List<int> purchasedSkins = new List<int>();
    public List<int> purchasedAbities = new List<int>();
    public int numberSelectedSkin = 0;
    public int money;
    private bool isValidEmail(string email)
    {
        var address = new System.Net.Mail.MailAddress(email);
        return address != null;
    }
}
