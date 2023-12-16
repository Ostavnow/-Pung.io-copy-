using UnityEngine;
using TMPro;
namespace UI 
{
    public class SkinCellUI : MonoBehaviour
    {
        private Skins _skins;
        private GameObject _buyButton;
        private GameObject _checkMark;
        private GameObject _refundButton;
        private TMP_Text _refundPriceText;
        [HideInInspector] public int id;
        private readonly bool _skinIsPurchased;
        public bool SkinIsPurchased
        {
            private get { return _skinIsPurchased; }
            set
            {
                if(value)
                {
                    PurchasedSkin();
                }
                else
                {
                    NotPurchasedSkin();
                }
            }
        }
        private void Start()
        {
            _skins = FindObjectOfType<Skins>();
            _buyButton = transform.GetChild(2).gameObject;
            _checkMark = transform.GetChild(3).gameObject;
            _refundButton = transform.GetChild(4).gameObject;
            _refundPriceText = transform.GetChild(4).GetChild(0).GetComponent<TMP_Text>();    
        }
        private void PurchasedSkin()
        {
            _buyButton.SetActive(false);
            _checkMark.SetActive(true);
            _refundButton.SetActive(true);
            _refundPriceText.text = (_skins._listSkins[id]._price / 2).ToString();
        }
        private void NotPurchasedSkin()
        {
            _buyButton.SetActive(true);
            _checkMark.SetActive(false);
            _refundButton.SetActive(false);
            _refundPriceText.gameObject.SetActive(false);
        }
    }
}
