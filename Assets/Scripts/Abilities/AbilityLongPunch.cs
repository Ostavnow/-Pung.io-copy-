using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitiesSystem
{
    public class AbilityLongPunch : Ability
    {
        private PlayerController _player;
        
        protected override void Initialize()
        {
            _player = MonoBehaviour.FindObjectOfType<PlayerController>();
            _timeOfAction = 10f;
            _recoveryTime = 25f;    
        }
        protected override IEnumerator AbilityCorutine()
        {
            _player._flightDistance = 20f;
            yield return new WaitForSeconds(_timeOfAction);
            _player._flightDistance = 2.5f;
        }
    }
}