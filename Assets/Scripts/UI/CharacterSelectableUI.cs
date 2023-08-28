using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharacterSelectableUI : MonoBehaviour
{
    private User _user;
    [SerializeField] private GameObject _prefabSkinCell;
    private DataManager _dataManager;
    private Transform _listSkins;
    void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _user = _dataManager._user;
        _listSkins = transform;
        _listSkins.GetChild(_user._numberSelectedSkin).GetChild(2).gameObject.SetActive(false);
    }
    public void SelectedSkin(int id)
    {
        _listSkins.GetChild(_user._numberSelectedSkin).GetChild(2).gameObject.SetActive(true);
        _user._numberSelectedSkin = _user._purchasedSkins[id];
        _listSkins.GetChild(id).GetChild(2).gameObject.SetActive(false);
    }
    public void ShowingPurchasedSkins()
    {
        for(int i = 0;i< _user._purchasedSkins.Count;i++)
        {
            GameObject skinCellCurrent = Instantiate(_prefabSkinCell,transform.position,Quaternion.identity,_listSkins);
            Skin skin = _dataManager.DeserializeSkin(_user._purchasedSkins[i]);
            TMP_Text attackDamage = skinCellCurrent.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text health = skinCellCurrent.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text stamina = skinCellCurrent.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text criticalDamage = skinCellCurrent.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text attackSpeed = skinCellCurrent.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<TMP_Text>();
            TMP_Text protection = skinCellCurrent.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<TMP_Text>();
            Image skinBody = skinCellCurrent.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            Image skinRightHand = skinCellCurrent.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            Image skinLeftHand = skinCellCurrent.transform.GetChild(1).GetChild(2).GetComponent<Image>();
            attackDamage.text = skin._attackDamage.ToString();
            health.text = skin._health.ToString();
            stamina.text = skin._stamina.ToString();
            criticalDamage.text = skin._criticalDamage.ToString();
            attackSpeed.text = skin._attackSpeed.ToString();
            protection.text = skin._protection.ToString();
            skinBody.sprite = skin._spriteSkinBody;
            skinRightHand.sprite = skin._spriteSkinHand;
            skinLeftHand.sprite = skin._spriteSkinHand;
            skinCellCurrent.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate{SelectedSkin(i);});

        }
    }
}
