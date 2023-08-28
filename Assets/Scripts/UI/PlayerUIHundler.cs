using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace UI
{
    public abstract class PlayerUIHundler : MonoBehaviour
    {
        protected AnimationHandler _animationsHandler;

        public Image _healthStripe;
        public Image _staminaStripe;
        public Image _XPStripe;
        public Image _firstAbilityImage;
        public Image _secondAbilityImage;
        public Image _thirdAbilityImage;
        public Image _fourthAbilityImage;
        public TMP_Text _textOfHealthStrip;
        public TMP_Text _textOfStaminaStrip;
        public TMP_Text _attackDamageText;
        public TMP_Text _healthText;
        public TMP_Text _staminaText;
        public TMP_Text _criticalDamageText;
        public TMP_Text _attackSpeedText;
        public TMP_Text _multiplierAttackDamageImprovementText;
        public TMP_Text _healthImprovementMultiplierText;
        public TMP_Text _staminaImprovementMultiplierText;
        public TMP_Text _criticalDamageImprovementMultiplierText;
        public TMP_Text _attackSpeedImprovementMultiplierText;
        public TMP_Text _numberImproventsText;
        public TMP_Text _levelText;
        public TMP_Text _comboText;
        public TMP_Text _moneyCounterTextMenu;
        public TMP_Text _moneyCounterTextGame;
        public GameObject _skilsPanel;
        public GameObject _comboGameObject; 
        private GameObject _settingPanel;
        private RectTransform _scrollbarBackground;
        private Scrollbar _scrollbarSound;
        [HideInInspector] public Transform _canvasTransform;
        private DataManager _dataManager;
        private void Awake()
        {
                _dataManager = FindObjectOfType<DataManager>();
                Debug.Log(_settingPanel);
                UserPlayer player = FindObjectOfType<UserPlayer>();

                // Debug.Log(User._seletedAbilities[0]);
                // Debug.Log(User._seletedAbilities[1]);
                // Debug.Log(User._seletedAbilities[2]);
                // Debug.Log(User._seletedAbilities[3]);
                // _firstAbilityImage.sprite = _dataManager.DeserializeAbility(_dataManager._user._seletedAbilities[0])._spriteAbility;
                // _secondAbilityImage.sprite = _dataManager.DeserializeAbility(_dataManager._user._seletedAbilities[1])._spriteAbility;
                // _thirdAbilityImage.sprite = _dataManager.DeserializeAbility(_dataManager._user._seletedAbilities[2])._spriteAbility;
                // _fourthAbilityImage.sprite = _dataManager.DeserializeAbility(_dataManager._user._seletedAbilities[3])._spriteAbility;
                // _moneyCounterTextGame.text = _dataManager._user.AmountCoin.ToString();
        }
        private void Start()
        {
            _animationsHandler = GameObject.Find("Animation Handler").GetComponent<AnimationHandler>();
        }
        public abstract void LevelTextSet(int level);
        public abstract void AttackDamageTextSet(float attackDamage);
        public abstract void CriticalDamageTextSet(float criticalDamage);
        public abstract void AttackSpeedTextSet(float attackSpeed);
        public abstract void HealthTextSet(float health);
        public abstract void StaminaTextSet(float stamina);
        public abstract void NumberImproventsTextSet(int numberImprovents);
        public abstract void SkilsPanelSetActive(bool isActive);
        public abstract void ComboTextSetActive(bool isActive);
        public abstract void HealthStripeUpdate(float health,float fullHealth);
        public abstract void StaminaStripeUpdate(float stamina, float fullStamina);
        public abstract IEnumerator ComboUIUpdate(Player player);
        public abstract void FirstAbilityImageFillAmount(float value);
        public abstract void SecondAbilityImageFillAmount(float value);
        public abstract void ThirdAbilityImageFillAmount(float value);
        public abstract void FourthAbilityImageFillAmount(float value);
        public abstract void XPStripeFillAmount(float value);
        public abstract void UpdateMoneyСounter();
    }
}
