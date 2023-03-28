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
    public List<int> purchasedSkins = new List<int>(){0,1,2};
    public List<int> purchasedAbilities = new List<int>(){0,1,2,3};
    public int[] seletedAbilities = new int[4]{0,1,2,3};
    public int numberSelectedSkin = 0;
}
