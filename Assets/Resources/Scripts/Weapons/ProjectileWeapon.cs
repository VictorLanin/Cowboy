using System.Collections;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileWeapon : WeaponInGameObject
    {
        [SerializeField] private Projectile _projectile;

        private ProjectileInstancer _instancer;
        protected override void Awake()
        {
            base.Awake();
            _instancer = ProjectileInstancer.CreateInstance(_projectile.name, 5);
        }
        
        public override void SetCanDamage(bool canDamage)
        {
            base.SetCanDamage(canDamage);
            if (!canDamage) return;
            InstantiateProjectile();
            
            void InstantiateProjectile()
            {
                if(!_instancer.CanInstantiate) return;
                StartCoroutine(_instancer.Delay());
                Instantiate(_projectile.gameObject);
                _projectile.MoveToTarget();
            }
        }
        
        private class ProjectileInstancer
        {
            public bool CanInstantiate { get; private set; }
        
            private ProjectileInstancer(string nameOfProjectile, int delayToInstantiate)
            {
                NameOfProjectile = nameOfProjectile;
                DelayToInstantiate = delayToInstantiate;
            }

            public static ProjectileInstancer CreateInstance(string nameOfProjectile, int delayToInstantiate)
            {
                return new ProjectileInstancer(nameOfProjectile, delayToInstantiate);
            }
        
            public string NameOfProjectile { get; }
            public int DelayToInstantiate { get; }

            public IEnumerator Delay()
            {
                CanInstantiate = false;
                for (int i = DelayToInstantiate; i > 0; i--)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                CanInstantiate = true;
            }
        }
    }
}