using System;
using Positions;
using Turns;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))][RequireComponent(typeof(GolemBase))]
    public class AnimationController : MonoBehaviour
    {
        private Animator _animator;
        private GolemBase _golemBase;

        private bool isCasting = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _golemBase = GetComponent<GolemBase>();
            _golemBase.OnMovingStart += StartMovingAnimation;
            _golemBase.OnMovingEnd += StopMovingAnimation;
            TurnsLogic.Instance.OnTurnTransition += SwitchTurnAnimation;
            TurnsLogic.Instance.OnSpellCast += StartCastAnimation;
        }

        private void StartMovingAnimation()
        {
            _animator.SetTrigger("StartMoving");
        }

        private void StopMovingAnimation()
        {
            _animator.SetTrigger("EndMoving");
        }
        
        private void SwitchTurnAnimation()
        {
            if (_golemBase.Equals(TurnsLogic.Instance.CurrentGolem()))
            {
                _animator.SetTrigger("StartTurn");
                isCasting = true;
            }

            if (isCasting)
            {
                _animator.SetTrigger("EndCasting");
                isCasting = false;
            }
        }
        
        private void StartCastAnimation()
        {
            if (_golemBase.Equals(TurnsLogic.Instance.CurrentGolem()))
            {
                _animator.SetTrigger("EndTurn");
            }
        }
    }
}
