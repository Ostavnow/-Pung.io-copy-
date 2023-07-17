using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    void Update()
    {
        MainUIHandler mainUIHandler = FindObjectOfType<MainUIHandler>();
        _animator = mainUIHandler._comboGameObject.GetComponent<Animator>();
    }
    public void ComboUIPlay()
    {
        _animator.SetTrigger("Combo");
    }
}
