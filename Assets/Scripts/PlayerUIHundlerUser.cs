using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHundlerUser : PlayerUIHundler
{
    public override void LevelTextSet(int level)
    {
        _mainUIHandler._levelText.text = level.ToString();
    }
    public override void AttackDamageTextSet(float attackDamage)
    {
        if(attackDamage == ((int)attackDamage))
                {
                    _mainUIHandler._attackDamageText.text = attackDamage.ToString() + ".0";
                }
                else
                {
                    _mainUIHandler._attackDamageText.text = attackDamage.ToString();  
                } 
    }
    public override void CriticalDamageTextSet(float criticalDamage)
    {
        if(criticalDamage == ((int)criticalDamage))
        {
            _mainUIHandler._criticalDamageText.text = criticalDamage.ToString() + ".0";
        }
        else
        {
            _mainUIHandler._criticalDamageText.text = criticalDamage.ToString();  
        }
    }
    public override void AttackSpeedTextSet(float attackSpeed)
    {
        if(attackSpeed == ((int)attackSpeed))
        {
            _mainUIHandler._attackSpeedText.text = attackSpeed.ToString() + ".0";
        }
        else
        {
            _mainUIHandler._attackSpeedText.text = attackSpeed.ToString();  
        }
    }
    public override void HealthTextSet(float health)
    {
        _mainUIHandler._healthText.text = health.ToString();
    }
    public override void StaminaTextSet(float stamina)
    {
        _mainUIHandler._staminaText.text = stamina.ToString();
    }
    public override void NumberImproventsTextSet(int numberImprovents)
    {
        _mainUIHandler._numberImproventsText.text = numberImprovents.ToString();
    }
    public override void SkilsPanelSetActive(bool isActive)
    {
        _mainUIHandler._skilsPanel.SetActive(isActive);
    }
    public override void ComboTextSetActive(bool isActive)
    {
        _mainUIHandler._comboGameObject.SetActive(isActive);
    }
    public override void HealthStripeUpdate(float health,float fullHealth)
    {
        _mainUIHandler._textOfHealthStrip.text = "HP" + ((int)health).ToString() + "/" + fullHealth;
        _mainUIHandler._healthStripe.fillAmount = health / fullHealth;
    }
    public override void StaminaStripeUpdate(float stamina, float fullStamina)
    {
        _mainUIHandler._textOfStaminaStrip.text = "STM" + ((int)stamina).ToString() + "/" + fullStamina;
        _mainUIHandler._staminaStripe.fillAmount = stamina / fullStamina;
    }
    public override IEnumerator ComboUIUpdate(Player player)
    {
        player._isCorutinaComboRunning = true;
        _mainUIHandler._comboText.text = "x" + player.ComboValue.ToString();
        _animationsHandler.ComboUIPlay();
        _mainUIHandler._comboGameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        _mainUIHandler._comboGameObject.SetActive(false);
        player._isCorutinaComboRunning = false;
    }
    public override void FirstAbilityImageFillAmount(float value)
    {
        _mainUIHandler._firstAbilityImage.fillAmount = value;
    }
    public override void SecondAbilityImageFillAmount(float value)
    {
        _mainUIHandler._secondAbilityImage.fillAmount = value;
    }
    public override void ThirdAbilityImageFillAmount(float value)
    {
        _mainUIHandler._thirdAbilityImage.fillAmount = value;
    }
    public override void FourthAbilityImageFillAmount(float value)
    {
        _mainUIHandler._fourthAbilityImage.fillAmount = value;
    }
    public override void XPStripeFillAmount(float value)
    {
        _mainUIHandler._XPStripe.fillAmount = value;
    }
    public override void UpdateMoneyСounter()
    {
        _mainUIHandler._moneyCounterTextGame.text = GameManager._instance._user._amountMoney.ToString();
    }
}
