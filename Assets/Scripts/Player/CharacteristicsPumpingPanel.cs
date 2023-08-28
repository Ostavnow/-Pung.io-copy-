using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsPumpingPanel : MonoBehaviour
{
    public PlayerController _playerController;
    private Player _player;
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ImproveAttackDamage()
    {
        if(_player.NumberImprovents > 0)
        {
            _player.NumberImprovents--;
            _player.AttackDamage += (float)System.Math.Round(_player._multiplierAttackDamageImprovement * 1,1);
        }
    }
    public void ImproveHealth()
    {
        if(_player.NumberImprovents > 0)
        {
        _player.NumberImprovents--;
        _player.FullHealth += (float)System.Math.Round(_player._healthImprovementMultiplier * 1,1);
        }
    }
    public void ImproveStamina()
    {
        if(_player.NumberImprovents > 0)
        {
        _player.NumberImprovents--;
        _player.FullStamina += (float)System.Math.Round(_player._staminaImprovementMultiplier * 1,1);
        }
    }
    public void ImproveCriticalDamage()
    { 
        if(_player.NumberImprovents > 0)
        {
        _player.NumberImprovents--;
        _player.CriticalDamage += (float)System.Math.Round(_player._criticalDamageImprovementMultiplier * 1,1);
        }
    }
    public void ImproveAttackSpeed()
    {
        if(_player.NumberImprovents > 0 & _player.AttackSpeed < 350)
        {
        _player.NumberImprovents--;
        _player.AttackSpeed += (float)System.Math.Round(_player._attackSpeedImprovementMultiplier * 1);
        _playerController._timeAfterWhichBeNextBlow = PlayerController._timeAfterWhichBeNextBlowConst / Mathf.Pow(1.00915f,_player.AttackSpeed);
        if(_playerController._timeAfterWhichBeNextBlow < 0.02f)
        {
            _playerController._timeAfterWhichBeNextBlow = 0.02f;
        }
        }
    }
}
