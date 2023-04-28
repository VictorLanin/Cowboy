using System;
using UnityEngine;


namespace LaninCode
{
    [RequireComponent(typeof(ProjectileWeapon))]
    public class PlayerProjectileWeapon : MonoBehaviour
    {
        private ProjectileWeapon _projectileWeapon;
        private void Awake()
        {
            _projectileWeapon=GetComponent<ProjectileWeapon>();
            _projectileWeapon.Awake();
            int howManyAmmo = howManyAmmo = _projectileWeapon.GetAmmo;
            _projectileWeapon.AdditionalConditions += () =>
                howManyAmmo == -1
                    ? _projectileWeapon.CanInstantiate
                    : _projectileWeapon.CanInstantiate && howManyAmmo > 0;
            _projectileWeapon.GetPosition += () => _projectileWeapon.СursorPosition;
        }
        
    }
}