using Tests;
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
        private Projectile _projectile;
        private void Awake()
        {
            _destructable=GetComponent<Desctructable>();
            _projectile = GetComponentInChildren<Projectile>();
            _projectile.Attack();
        }
        
        public override void Activate(OnOff onOff)
        {
            _destructable.Activate(onOff);
        }

        public override string Name => _nameOfEnemy;


        public void DestroyObject()
        {
            Debug.Log("back to pool");
            Activate(OnOff.Off);
            ObjectPoolsManager.Release(this);
        }
    }
}