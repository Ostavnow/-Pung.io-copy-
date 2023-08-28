using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExsperienceBoost : Ability
{
    private Player _player;
    protected override void Initialize()
    {
        _player = MonoBehaviour.FindObjectOfType<Player>();
        _timeOfAction = 10f;
        _recoveryTime = 20f;    
    }
    protected override IEnumerator AbilityCorutine()
    {   
        _player._experienceImprovementMultiplier = 2f;
        yield return new WaitForSeconds(_timeOfAction);
        _player._experienceImprovementMultiplier = 1f;
    }
}
