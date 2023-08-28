using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDush : Ability
{
    private PlayerController _playerController;
    protected override void Initialize()
    {
        _timeOfAction = 0.4f;
        _recoveryTime = 15f;
        _playerController = MonoBehaviour.FindObjectOfType<PlayerController>();
    }
    protected override IEnumerator AbilityCorutine()
    {
            _playerController._speed = 15f;
            float timeOfAciton = _timeOfAction;
            while (timeOfAciton > 0)
            {
                _playerController._rb.MovePosition(_playerController._rb.position + ((Vector2)_playerController.transform.up) * _playerController._speed * Time.deltaTime);
                timeOfAciton -= Time.deltaTime;
                yield return null;
            }
            _playerController._speed = 5f;
    }
}
