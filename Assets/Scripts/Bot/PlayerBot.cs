using UnityEngine;
using UI;
[RequireComponent(typeof(PlayerUIHundlerBot))]
[RequireComponent(typeof(BotPlayerController))]
public class PlayerBot : Player 
{
    private void Start()
    {
        _playerUIHundler = GetComponent<PlayerUIHundler>();
        PlayerController playerController = GetComponent<PlayerController>();
    }
    public override void BlowHit()
    {
        ComboValue++;
        AddingExperience();
    }
    public override void KillEnemy()
    {
        
    }
}