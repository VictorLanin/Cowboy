using System;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(Animator))]
    public class DestructableEnvironment : MonoBehaviour,IOnDamage
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetHealth(int health)
        {
            _animator.SetInteger("Health",health);
        }
    }
}