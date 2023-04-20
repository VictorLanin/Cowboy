using Tests;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    [RequireComponent(typeof(Desctructable))]
    public class Enemy : Poolable,IOnDamage
    {
        [SerializeField] string _nameOfEnemy;
        [SerializeField] private string _nameOfWeapon;
        private Desctructable _destructable;
        private Animator _animator;
        private int _health;

        private void Awake()
        {
            _destructable=GetComponent<Desctructable>();
            _animator = GetComponent<Animator>();
            _destructable.SetDestructee(this);
            _health=Animator.StringToHash("Health");
        }
        
        public override void Activate(OnOff onOff)
        {
            _destructable.Activate(onOff);
        }

        public override string Name => _nameOfEnemy;
        
        public void SetHealth(int health)
        {
            _animator.SetInteger(_health,health);
        }

        public void BackToPool()
        {
            Activate(OnOff.Off);
            //ObjectPoolsManager.Release(this);
            _animator.SetInteger(_health,20);
            _animator.enabled = false;
        }
    }
}