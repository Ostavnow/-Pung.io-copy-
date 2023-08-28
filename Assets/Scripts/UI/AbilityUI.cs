using UnityEngine;
using TMPro;
public class AbilityUI : MonoBehaviour
{
    private User _user;
    private ISaveProgress _dataManager;
    private Transform _listAbilities;
    private GameObject _refundPanel;
    private Abilities _abilities;
    private TMP_Text _coinBalanceText;
    private int _idSelectedAbility;
    private void Start()
    {
        _abilities = FindObjectOfType<Abilities>();
        _coinBalanceText = GameObject.FindGameObjectWithTag("coin balance").GetComponent<TMP_Text>();
        _dataManager = FindObjectOfType<DataManager>();
        _listAbilities = transform;
        _refundPanel = GameObject.FindGameObjectWithTag("refund panel");
    }
    public void UpdateListPurchasedAbilities()
        {
            _coinBalanceText.text = _user.CoinBalance.ToString();
            for(int i = 3;i < _user._purchasedSkins.Count;i++)
            {
                _listAbilities.GetChild(_user._purchasedAbilities[i] - 4).GetChild(1).gameObject.SetActive(false);
            }
        }
    public void BuyAbility(int id)
    {
        if(_user.CoinBalance >= _abilities._abilities[id]._price)
        {
            _user.CoinBalance -= _abilities._abilities[id]._price;
            _coinBalanceText.text = _user.CoinBalance.ToString();
            _user._purchasedAbilities.Add(id);
            _user._purchasedAbilities.Sort();
            _dataManager.SaveProgress();
            _listAbilities.GetChild(_user._purchasedSkins[id - 4]).GetChild(1).gameObject.SetActive(false);
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
            _user.CoinBalance += _abilities._abilities[_idSelectedAbility]._price / 2;
            _coinBalanceText.text = _user.CoinBalance.ToString();
            _user._purchasedSkins.Remove(_idSelectedAbility);
            _dataManager.SaveProgress();
            _listAbilities.GetChild(_user._purchasedSkins[_idSelectedAbility - 4]).GetChild(1).gameObject.SetActive(true);
        }
}
