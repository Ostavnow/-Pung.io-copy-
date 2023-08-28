using UnityEngine;
using TMPro;
namespace UI
{
    public class SkinUI : MonoBehaviour
    {
        private User _user;
        private Skins _skins;
        private TMP_Text _coinBalanceText;
        private readonly Transform _listSkins;
        private ISaveProgress _dataManager;
        private int _idSelectedSkin;
        [SerializeField] private readonly GameObject _refundPanel;
        private void Start()
        {
            _dataManager = FindObjectOfType<DataManager>();
            _user = FindObjectOfType<DataManager>()._user;
            _skins = GetComponent<Skins>();
            _coinBalanceText = GameObject.FindGameObjectWithTag("coin balance").GetComponent<TMP_Text>(); 
            UpdateListPurchasedSkins();
        }
        public void UpdateListPurchasedSkins()
        {
            _coinBalanceText.text = _user.CoinBalance.ToString();
            for(int i = 3;i < _user._purchasedSkins.Count;i++)
            {
                _listSkins.GetChild(_user._purchasedSkins[i] - 3).GetComponent<SkinCellUI>().SkinIsPurchased = true;
            }
        }
        public void BuySkin(int id)
        {
            if(_user.CoinBalance >= _skins._listSkins[id]._price)
            {
                _user.CoinBalance -= _skins._listSkins[id]._price;
                _coinBalanceText.text = _user.CoinBalance.ToString();
                _user._purchasedSkins.Add(id);
                _user._purchasedSkins.Sort();
                _dataManager.SaveProgress();
                _listSkins.GetChild(_user._purchasedSkins[id - 3]).GetComponent<SkinCellUI>().SkinIsPurchased = true;
            }

        }
        public void RefundSkin(int id)
        {
            _idSelectedSkin = id;
            _refundPanel.SetActive(true);
        }
        public void CancelRefund()
        {
            _refundPanel.SetActive(false);
        }
        public void ConfirmationSkinRefund()
        {
            _user.CoinBalance += _skins._listSkins[_idSelectedSkin]._price / 2;
            _coinBalanceText.text = _user.CoinBalance.ToString();
            _user._purchasedSkins.Remove(_idSelectedSkin);
            _dataManager.SaveProgress();
            _listSkins.GetChild(_user._purchasedSkins[_idSelectedSkin - 3]).GetComponent<SkinCellUI>().SkinIsPurchased = false;
            _refundPanel.SetActive(false);
        }
    }
}