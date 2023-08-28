using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class PurchaseGameCurrency : MonoBehaviour
    {
        [SerializeField] private int _slotPrice;
        private User _user;
        private void Start()
        {
            _user = FindAnyObjectByType<DataManager>()._user;    
        }
        public void CoinReplenishment(int quantityCoin)
        {
            _user.CoinBalance += quantityCoin;
        }
    }
}