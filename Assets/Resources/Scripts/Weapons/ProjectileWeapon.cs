using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileWeapon : WeaponInGameObject
    {
        [SerializeField] private WeaponName weaponName;
        [SerializeField] private Projectile _projectile;
        private ProjectileInstancer _instancer;
        public Func<Vector3> GetPosition { get; set; }

        public WeaponName NameOfWeapon => weaponName;

        public void Awake()
        {
            _projectile.WeaponGameObject = this;
        }
        public void InstantiateProjectile()
        {
            var proj= Instantiate(_projectile.gameObject);
            var projScr = proj.GetComponent<Projectile>(); 
            projScr.MoveToTarget(GetPosition());
        }
    }
}