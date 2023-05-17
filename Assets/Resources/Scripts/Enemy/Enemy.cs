using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace LaninCode
{
    [RequireComponent(typeof(Destructible))]
    public class Enemy : Poolable,IOnDamage
    {
        private Destructible _destructible;
        private Animator _animator;
        [SerializeField] string _nameOfEnemy;
        
        //todo сделать орудия 

        private IWeapon _equippedWeapon;

        private EnemyData _enemyData;
        private void Awake()
        {
            _destructible=GetComponent<Destructible>();
            _animator = GetComponent<Animator>();
            _enemyData = EnemyData.GetEnemyData(_nameOfEnemy);
            _destructible.SetDestructee(this);
            _equippedWeapon = WeaponDataManager.EnemyWeapons[_enemyData.AvailableWeapons[0]];
        }
        
        public override void Activate(OnOff onOff)
        {
            _destructible.Activate(onOff);
        }

        public override string Name => _nameOfEnemy;
        
        public void SetHealth(int health)
        {
            _animator.SetInteger("Health",health);
        }

        public int MaxHealth => _enemyData==null?100:_enemyData.MaxHealth;

        public void BackToPool()
        {
            Activate(OnOff.Off); 
            ObjectPoolsManager.Release(this);
            _animator.SetInteger("Health",_destructible.MaxHealth);
            _animator.StopPlayback();
        }
        
    }
}