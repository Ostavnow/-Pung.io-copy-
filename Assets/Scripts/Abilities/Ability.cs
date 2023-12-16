using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AbilitiesSystem
{
    [Serializable]
    public class Ability
    {
        public Sprite _spriteAbility;
        public int _price;
        public AbilitiesEnum _abilityType;
        [SerializeField] protected float _timeOfAction;
        [SerializeField] public float _recoveryTime;
        protected MonoBehaviour _monoBehaviour;
        public Ability (MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            Initialize();
        }
        protected Ability ()
        {
            Initialize();
        }
        protected virtual void Initialize()
        {

        }
        public void AbilityActivation() => _monoBehaviour.StartCoroutine(AbilityCorutine());
        protected virtual IEnumerator AbilityCorutine()
        {
            return null;
        }
    }
}
