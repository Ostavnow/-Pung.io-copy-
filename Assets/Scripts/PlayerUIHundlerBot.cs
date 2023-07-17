using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUIHundlerBot : PlayerUIHundler
{
    private TMP_Text nicknameText;
    private TMP_Text levelText;
    private void Start()
    {
        levelText = transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }
    public override void LevelTextSet(int level)
    {
        levelText.text = level.ToString();
    }
    public override void AttackDamageTextSet(float attackDamage){}
    public override void CriticalDamageTextSet(float criticalDamage){}
    public override void AttackSpeedTextSet(float attackSpeed){}
    public override void HealthTextSet(float health){}
    public override void StaminaTextSet(float stamina){}
    public override void NumberImproventsTextSet(int numberImprovents){}
    public override void SkilsPanelSetActive(bool isActive){}
    public override void ComboTextSetActive(bool isActive){}
    public override void HealthStripeUpdate(float health,float fullHealth){}
    public override void StaminaStripeUpdate(float stamina, float fullStamina){}
    public override IEnumerator ComboUIUpdate(Player player)
    {
        yield return null;
    }
    public override void FirstAbilityImageFillAmount(float value){}
    public override void SecondAbilityImageFillAmount(float value){}
    public override void ThirdAbilityImageFillAmount(float value){}
    public override void FourthAbilityImageFillAmount(float value){}
    public override void XPStripeFillAmount(float value){}
    public override void UpdateMoneyСounter(){}
}
