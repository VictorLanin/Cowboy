using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileInstancer : MonoBehaviour,IWeapon
    {
        private Stack<ProjectileGameObject> _projectiles;
        public Action OnPoppingProjectile { get; set; } = null;
        public Action OnPushProjectile { get; set; } = null;
        public bool CanInstantiate { get; set; } = true;
        private IEnumerator _cor;

        public void Awake()
        {
            _cor = Shoot();
        }

        //todo может поменять на делегат по получению данных от 
        public IEnumerator Shoot()
        {
            CanInstantiate = false;
            var projData = GetProjectileData();
            var positions = GetPositions();
            for (int i = 0; i < projData.HowManyInstances; i++)
            {
                var proj = _projectiles.Pop();
                OnPoppingProjectile?.Invoke();
                proj.MoveToTarget(positions[i], projData.SpeedOfProjectile);
                yield return new WaitForSeconds(projData.DelayToInstantiate);
            }
            CanInstantiate = true;
        }

        public void AddToStack(ProjectileGameObject proj)
        {
            if (_projectiles.Contains(proj)) return;
            _projectiles.Push(proj);
            OnPushProjectile();
        }

        public bool CanDamage => true;
        public void ApplyDamage(Destructible destructible)
        {
            var projData = GetProjectileData();
            destructible.GetDamage(projData.Damage); 
        }

        public string NameofWeapon => GetProjectileData()?.NameOfWeapon;
        public IWeaponData WeaponData => GetProjectileData?.Invoke();

        public void TryFiring()
        {
            if (!OnCheckToInstance()) return;
            StartCoroutine(_cor);
        }

        public Func<List<Vector3>> GetPositions { get; set; }
        public Func<bool> OnCheckToInstance { get; set; }
        public Func<IProjectileData> GetProjectileData { get; set; }
        public IEnumerator SetProjectilesReady(string nameOfInstancer)
        {
            _projectiles = new Stack<ProjectileGameObject>(GetComponentsInChildren<ProjectileGameObject>());
            foreach (var proj in _projectiles)
            {
                var data = GetProjectileData();
                while (GetProjectileData().SpriteOfProjectile is null)
                {
                    data = GetProjectileData();
                    yield return null;
                }
                proj.SetData(data);
                proj.name = nameOfInstancer +' '+data.NameOfWeapon;
            }
        }
    }
}