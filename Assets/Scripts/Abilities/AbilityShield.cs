using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShield : Ability
{
    private PlayerController _playerController;
    protected override void Initialize()
    {
        _playerController = MonoBehaviour.FindObjectOfType<PlayerController>();
        _timeOfAction = 5f;
        _recoveryTime = 15f;
    }
    protected override IEnumerator AbilityCorutine()
    {
        _playerController._shield.SetActive(true);
        yield return new WaitForSeconds(_timeOfAction);
        _playerController._shield.SetActive(false);
    }
}
