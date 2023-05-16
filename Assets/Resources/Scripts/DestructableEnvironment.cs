using System;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(Animator))]
    public class DestructableEnvironment : MonoBehaviour,IOnDamage
    {
        [SerializeField] private int _maxHealth = 20;
        private Animator _animator;
        private Destructible _destructible;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _destructible = GetComponent<Destructible>();
            _destructible.SetDestructee(this);
            _animator.SetInteger("Health",_destructible.MaxHealth);
        }

        public void SetHealth(int health)
        {
            _animator.SetInteger("Health",health);
        }

        public void BackToPool()
        {
            _destructible.Activate(OnOff.Off);
            //ObjectPoolsManager.Release(this);
            _animator.SetInteger("Health",_destructible.MaxHealth);
            _animator.StopPlayback();
        }
        public int MaxHealth => _maxHealth;
    }
}