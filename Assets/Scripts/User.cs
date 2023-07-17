using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class User
{
    public int _id;
    public string _userName;
    public string _email;
    public string _password;
    public int _amountMoney = 0;
    public List<int> _purchasedSkins = new List<int>(){0,1,2};
    public List<int> _purchasedAbilities = new List<int>(){0,1,2,3};
    public int[] _seletedAbilities = new int[4]{0,1,2,3};
    public int _numberSelectedSkin = 0;
}
