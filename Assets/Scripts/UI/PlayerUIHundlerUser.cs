using System.Collections;
using UnityEngine;
namespace UI
{
    public class PlayerUIHundlerUser : PlayerUIHundler
    {
        public override void LevelTextSet(int level)
        {
            _levelText.text = level.ToString();
        }
        public override void AttackDamageTextSet(float attackDamage)
        {
            if(attackDamage == ((int)attackDamage))
                    {
                        _attackDamageText.text = attackDamage.ToString() + ".0";
                    }
                    else
                    {
                        _attackDamageText.text = attackDamage.ToString();  
                    } 
        }
        public override void CriticalDamageTextSet(float criticalDamage)
        {
            if(criticalDamage == ((int)criticalDamage))
            {
                _criticalDamageText.text = criticalDamage.ToString() + ".0";
            }
            else
            {
                _criticalDamageText.text = criticalDamage.ToString();  
            }
        }
        public override void AttackSpeedTextSet(float attackSpeed)
        {
            if(attackSpeed == ((int)attackSpeed))
            {
                _attackSpeedText.text = attackSpeed.ToString() + ".0";
            }
            else
            {
                _attackSpeedText.text = attackSpeed.ToString();  
            }
        }
        public override void HealthTextSet(float health)
        {
           _healthText.text = health.ToString();
        }
        public override void StaminaTextSet(float stamina)
        {
           _staminaText.text = stamina.ToString();
        }
        public override void NumberImproventsTextSet(int numberImprovents)
        {
           _numberImproventsText.text = numberImprovents.ToString();
        }
        public override void SkilsPanelSetActive(bool isActive)
        {
           _skilsPanel.SetActive(isActive);
        }
        public override void ComboTextSetActive(bool isActive)
        {
           _comboGameObject.SetActive(isActive);
        }
        public override void HealthStripeUpdate(float health,float fullHealth)
        {
           _textOfHealthStrip.text = "HP" + ((int)health).ToString() + "/" + fullHealth;
           _healthStripe.fillAmount = health / fullHealth;
        }
        public override void StaminaStripeUpdate(float stamina, float fullStamina)
        {
           _textOfStaminaStrip.text = "STM" + ((int)stamina).ToString() + "/" + fullStamina;
           _staminaStripe.fillAmount = stamina / fullStamina;
        }
        public override IEnumerator ComboUIUpdate(Player player)
        {
            player._isCorutinaComboRunning = true;
           _comboText.text = "x" + player.ComboValue.ToString();
            _animationsHandler.ComboUIPlay();
           _comboGameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
           _comboGameObject.SetActive(false);
            player._isCorutinaComboRunning = false;
        }
        public override void FirstAbilityImageFillAmount(float value)
        {
           _firstAbilityImage.fillAmount = value;
        }
        public override void SecondAbilityImageFillAmount(float value)
        {
           _secondAbilityImage.fillAmount = value;
        }
        public override void ThirdAbilityImageFillAmount(float value)
        {
           _thirdAbilityImage.fillAmount = value;
        }
        public override void FourthAbilityImageFillAmount(float value)
        {
           _fourthAbilityImage.fillAmount = value;
        }
        public override void XPStripeFillAmount(float value)
        {
           _XPStripe.fillAmount = value;
        }
        public override void UpdateMoneyСounter()
        {
           _moneyCounterTextGame.text = GameManager._instance._user.CoinBalance.ToString();
        }
    }
}
