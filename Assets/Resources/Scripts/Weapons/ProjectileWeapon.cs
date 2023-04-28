using System;
using System.Collections;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileWeapon : WeaponInGameObject
    {
        [SerializeField] private Projectile _projectile;

        private ProjectileInstancer _instancer;
        public bool CanInstantiate => _instancer.CanInstantiate;
        public Func<Vector3> GetPosition { get; set; }
        
        public override void Awake()
        {
            base.Awake();
            _instancer = ProjectileInstancer.CreateInstance(_projectile.name, 5);
            _projectile.WeaponGameObject = this;
        }
        public override void StartFiring(bool isFiring)
        {
            base.StartFiring(isFiring);
            if (!isFiring) return;
            InstantiateProjectile();
            void InstantiateProjectile()
            {
                if(!_instancer.CanInstantiate) return;
                StartCoroutine(_instancer.Delay());
               var proj= Instantiate(_projectile.gameObject);
               var projScr = proj.GetComponent<Projectile>(); 
               projScr.MoveToTarget(GetPosition());
            }
        }
        
        private class ProjectileInstancer
        {
            public bool CanInstantiate { get; private set; }
        
            private ProjectileInstancer(string nameOfProjectile, int delayToInstantiate)
            {
                NameOfProjectile = nameOfProjectile;
                DelayToInstantiate = delayToInstantiate;
                CanInstantiate = true;
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

        public override bool CanDamage { get; } = true;
    }
}