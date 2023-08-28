using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBigPlayer : Ability
{
    PlayerController _playerController;
    protected override void Initialize()
    {
        _timeOfAction = 10f;
        _recoveryTime = 20f;
        _playerController = MonoBehaviour.FindObjectOfType<PlayerController>();
    }
    protected override IEnumerator AbilityCorutine()
    {
        _playerController.gameObject.transform.localScale = new Vector3(2f,2f,1f);
        _playerController._hand.transform.localScale = new Vector3(2f,2f,1f);
        yield return new WaitForSeconds(1f);
        _playerController.gameObject.transform.localScale = new Vector3(1f,1f,1f);
        _playerController._hand.transform.localScale = new Vector3(1f,1f,1f);
    }
}
