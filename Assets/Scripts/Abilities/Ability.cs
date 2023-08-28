using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public abstract class Ability
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
    protected abstract void Initialize();
    public void AbilityActivation() => _monoBehaviour.StartCoroutine(AbilityCorutine());
    protected abstract IEnumerator AbilityCorutine();

    public static implicit operator Ability(Type v)
    {
        throw new NotImplementedException();
    }
}
