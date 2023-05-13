using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace LaninCode
{
    [RequireComponent(typeof(Destructible))]
    public class Enemy : Poolable,IOnDamage
    {
        private Destructible _destructible;
        private Animator _animator;
        private int _health; 
        [SerializeField] string _nameOfEnemy;

       [SerializeField] private List<EnemyWeaponName> availableWeaponNames;

        private static Dictionary<EnemyWeaponName, EnemyWeapon> _enemyWeapons;
        private EnemyWeapon _equippedWeapon;
        
        private void Awake()
        {
            _destructible=GetComponent<Destructible>();
            _animator = GetComponent<Animator>();
            _destructible.SetDestructee(this);
            _health=Animator.StringToHash("Health");
            _equippedWeapon = _enemyWeapons[availableWeaponNames[0]];
        }
        
        public override void Activate(OnOff onOff)
        {
            _destructible.Activate(onOff);
        }

        public override string Name => _nameOfEnemy;
        
        public void SetHealth(int health)
        {
            _animator.SetInteger(_health,health);
        }

        public int MaxHealth { get; }

        public void BackToPool()
        {
            Activate(OnOff.Off);
            //ObjectPoolsManager.Release(this);
            _animator.SetInteger(_health,_destructible.MaxHealth);
            _animator.StopPlayback();
        }

        public static IWeapon GetWeapon(string nameOfCol)
        {
            var key = Enum.Parse<EnemyWeaponName>(nameOfCol);
            return _enemyWeapons[key];
        } 
    }
}