using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LaninCode
{
    public class ProjectileManager : MonoBehaviour
    {
        [FormerlySerializedAs("_projectile")] [SerializeField] private PlayerProjectile playerProjectile;
        public Func<Vector3> GetPosition { get; set; }
        
        public void InstantiateProjectile(IProjectile projectileWeapon)
        {
            if (!projectileWeapon.CanInstantiate) return;
            StartCoroutine(projectileWeapon.Instancer.Delay());
            var proj= Instantiate(playerProjectile.gameObject);
            proj.name = "PlayerOne " + playerProjectile.Name;
            var projScr = proj.GetComponent<PlayerProjectile>(); 
            projScr.MoveToTarget(GetPosition(),projectileWeapon.SpeedOfProjectile);
        }
    }
}