using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    [RequireComponent(typeof(Destructible))]
    public class Enemy : Poolable,IOnDamage
    {
        [SerializeField] string _nameOfEnemy;
        private Destructible _destructible;
        private Animator _animator;
        private int _health;

        private void Awake()
        {
            _destructible=GetComponent<Destructible>();
            _animator = GetComponent<Animator>();
            _destructible.SetDestructee(this);
            _health=Animator.StringToHash("Health");
        }
        
        public override void Activate(OnOff onOff)
        {
            _destructible.Activate(onOff);
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
            _animator.SetFloat(_health,_destructible.HealthRelatToMaxHealth);
            _animator.StopPlayback();
        }
    }
}