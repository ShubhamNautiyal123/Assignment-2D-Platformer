using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assignment.Core
{
    public class AnimationController : MonoBehaviour
    {

        #region Fields
        private Animator _animator;


        ///Animation States
        public enum AnimationState { 
            Idle,
            Run
        }
        public static AnimationState currentAnimState= AnimationState.Idle;
        #endregion

        #region Monobehaviours
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {

            switch (currentAnimState) {

                case AnimationState.Idle:
                    _animator.Play("idle");
                    break;
                
                case AnimationState.Run:
                    _animator.Play("run");
                    break;
            }
          
        }
        #endregion

    }

}
