using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class User
{
    public int id;
    public string userName;
    public string email;
    public string password;
    public int amountMoney = 0;
    public List<int> purchasedSkins = new List<int>();
    public List<int> purchasedAbities = new List<int>();
    public int numberSelectedSkin = 0;
}
