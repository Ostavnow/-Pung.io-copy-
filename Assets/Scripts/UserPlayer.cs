using System.Collections;
using UnityEngine;

public class UserPlayer : Player, IPlayerInfo
{
    private void Start()
    {
        _playerUIHundler = GetComponent<PlayerUIHundler>();
        GameManager._instance.UpdateSkin();
    }
    public override void BlowHit()
    {
        GameManager._instance._user._amountMoney += 1;
        GameManager._instance.SaveUserProgress();
        _playerUIHundler.UpdateMoneyСounter();
        ComboValue++;
        AddingExperience();
    }
    public override void KillEnemy()
    {
        
    }
}