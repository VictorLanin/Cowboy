using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    [RequireComponent(typeof(Destructable))]
    public class Enemy : Poolable,IOnDamage
    {
        [SerializeField] string _nameOfEnemy;
        private Destructable _destructable;
        private Animator _animator;
        private int _health;

        private void Awake()
        {
            _destructable=GetComponent<Destructable>();
            _animator = GetComponent<Animator>();
            _destructable.SetDestructee(this);
            _health=Animator.StringToHash("Health");
        }
        
        public override void Activate(OnOff onOff)
        {
            _destructable.Activate(onOff);
        }

        public override string Name => _nameOfEnemy;
        
        public void SetHealth(float health)
        {
            _animator.SetFloat(_health,health);
        }

        public void BackToPool()
        {
            Activate(OnOff.Off);
            //ObjectPoolsManager.Release(this);
            _animator.SetFloat(_health,_destructable.Health);
            _animator.enabled = false;
        }
    }
}