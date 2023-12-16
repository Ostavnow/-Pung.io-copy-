using UnityEngine;
using TMPro;
using AbilitiesSystem;
namespace UI
{
    public class AbilityUI : MonoBehaviour
    {
        private User _user;
        private ISaveProgress _dataManager; 
        private Abilities _abilities;
        private TMP_Text _coinBalanceText;
        private int _idSelectedAbility;
        [SerializeField] private Transform _listAbilitiesTransform;
        [SerializeField] private GameObject _refundPanel;
        private void Start()
        {
            _abilities = FindObjectOfType<Abilities>();
            _coinBalanceText = GameObject.FindGameObjectWithTag("coin balance").GetComponent<TMP_Text>();
            _dataManager = FindObjectOfType<DataManager>();
            _user = FindObjectOfType<DataManager>()._user;
        }
        public void UpdateListPurchasedAbilities()
            {
                _coinBalanceText.text = _user.CoinBalance.ToString();
                for(int i = 3;i < _user._purchasedSkins.Count;i++)
                {
                    _listAbilitiesTransform.GetChild(_user._purchasedAbilities[i] - 4).GetChild(1).gameObject.SetActive(false);
                }
            }
        public void BuyAbility(int id)
        {
            if(_user.CoinBalance >= _abilities._listAbilities[id]._price)
            {
                _user.CoinBalance -= _abilities._listAbilities[id]._price;
                _coinBalanceText.text = _user.CoinBalance.ToString();
                _user._purchasedAbilities.Add(id);
                _user._purchasedAbilities.Sort();
                _dataManager.SaveProgress();
                _listAbilitiesTransform.GetChild(_user._purchasedSkins[id - 4]).GetChild(1).gameObject.SetActive(false);
            }
        }
        public void RefundAbility(int id)
        {
            _idSelectedAbility = id;
            _refundPanel.SetActive(true);
        }
        public void CancelRefund()
        {
            _refundPanel.SetActive(false);
        }
        public void ConfirmationSkinRefund()
            {
                _user.CoinBalance += _abilities._listAbilities[_idSelectedAbility]._price / 2;
                _coinBalanceText.text = _user.CoinBalance.ToString();
                _user._purchasedSkins.Remove(_idSelectedAbility);
                _dataManager.SaveProgress();
                _listAbilitiesTransform.GetChild(_user._purchasedSkins[_idSelectedAbility - 4]).GetChild(1).gameObject.SetActive(true);
            }
    }
}