using UnityEngine;
using System.Collections.Generic;
using AbilitiesSystem;
[System.Serializable]
public class User
{
    public int _id;
    public string _userName;
    public string _email;
    public string _password;
    private int _coinBalance = 0;
    public int CoinBalance
    {
        get{return _coinBalance;}
        set
        {
            if(value < 0)
            {
                Debug.LogWarning("Недостаточно монет");   
            }
            else
            {
                _coinBalance = value;
            }
        }
    }
    public List<int> _purchasedSkins = new List<int>(){0,1,2};
    public List<int> _purchasedAbilities = new List<int>(){0,1,2,3};
    public AbilitiesEnum[] _seletedAbilities = new AbilitiesEnum[4] { 0, (AbilitiesEnum)1, (AbilitiesEnum)2, (AbilitiesEnum)3 };
    public int _numberSelectedSkin = 0;
}
