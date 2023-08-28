using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    void Update()
    {
        _animator = FindObjectOfType<Animator>();
    }
    public void ComboUIPlay()
    {
        _animator.SetTrigger("Combo");
    }
}
