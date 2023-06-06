
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    [RequireComponent(typeof(Destructible))]
    public class Enemy :MonoBehaviour, IPoolable,IOnDamage
    {
        private Destructible _destructible;
        private Animator _animator;
        [SerializeField] string _nameOfEnemy;
        private EnemyData _enemyData;
        private IObjectPool<IPoolable> _pool;
        public EnemyWeaponData EquippedWeaponData => WeaponManager.GetEnemyWeapon(_enemyData.AvailableWeapons[0]) as EnemyWeaponData;

        public virtual void Awake()
        {
            _destructible=GetComponent<Destructible>();
            _animator = GetComponent<Animator>();
            _enemyData = EnemyData.GetEnemyData(_nameOfEnemy);
            _destructible.SetDestructee(this);
        }
        
        public void Activate(OnOff onOff)
        {
            _destructible.Activate(onOff);
            _animator.enabled = onOff == OnOff.On;
        }

        public string Name => _nameOfEnemy;
        public void Destroy()
        {
            GameObject.Destroy(this);
        }

        public void SetPool(IObjectPool<IPoolable> pool)
        {
            _pool = pool;
        }


        public void SetHealth(int health)
        {
            _animator.SetInteger("Health",health);
        }

        private void OnBecameInvisible()
        {
            BackToPool();
        }

        public int MaxHealth => _enemyData==null?100:_enemyData.MaxHealth;

        public void BackToPool()
        {
            Activate(OnOff.Off); 
            ObjectPoolsManager.Release(this);
            _animator.StopPlayback();
            _animator.SetInteger("Health",_destructible.MaxHealth);
        }
        
    }
}