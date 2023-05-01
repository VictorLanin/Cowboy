using System;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        public Func<Vector3> GetPosition { get; set; }
        
        public void InstantiateProjectile(IProjectile projectileWeapon)
        {
            if (!projectileWeapon.CanInstantiate) return;
            StartCoroutine(projectileWeapon.Instancer.Delay());
            var proj= Instantiate(_projectile.gameObject);
            proj.name = "PlayerOne " + _projectile.Name;
            var projScr = proj.GetComponent<Projectile>(); 
            projScr.MoveToTarget(GetPosition());
        }
    }
}