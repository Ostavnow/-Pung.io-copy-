using UI;
using UnityEngine;

public class UserPlayer : Player, IPlayerInfo
{
    private DataManager _dataManager;
    private void Start()
    {
        _playerUIHundler = GetComponent<PlayerUIHundler>();
        GameManager._instance.UpdateSkin();
        _dataManager = FindObjectOfType<DataManager>();
    }
    public override void BlowHit()
    {
        _dataManager._user.CoinBalance += 1;
        _dataManager.SaveProgress();
        _playerUIHundler.UpdateMoneyСounter();
        ComboValue++;
        AddingExperience();
    }
    public override void KillEnemy()
    {
        
    }
}