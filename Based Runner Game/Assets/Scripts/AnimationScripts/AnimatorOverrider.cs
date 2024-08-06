using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimatorOverrider : MonoBehaviour
{
    //  [SerializeField] private AnimatorOverrideController[] overrideControllers;
    //  [SerializeField] private AnimatorOverrider overrider;

    /* 
        public void Set(int value)
    {
    overrider.SetAnimations(overrideControllers[value]);
    }
      */


    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        _animator.runtimeAnimatorController = overrideController;
    }

    /*
     SetType
     //  [SerializeField] private AnimatorOverrideController[] overrideControllers;
    //  [SerializeField] private AnimatorOverrider overrider;

     
        public void Set(int value)
    {
    overrider.SetAnimations(overrideControllers[value]);
    }
      

     */

}