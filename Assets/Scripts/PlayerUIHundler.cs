using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerUIHundler : MonoBehaviour
{
    protected MainUIHandler _mainUIHandler;
    protected AnimationHandler _animationsHandler;
    private void Start()
    {
        _mainUIHandler = FindObjectOfType<MainUIHandler>();
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
