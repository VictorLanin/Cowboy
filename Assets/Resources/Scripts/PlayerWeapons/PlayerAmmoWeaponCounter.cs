using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(PlayerProjectileInstancer))]
    public class PlayerAmmoWeaponCounter : MonoBehaviour, IAmmoWeapon
    {
        private int _availableAmmo = 0;
        private PlayerProjectileInstancer _playerProjectileInstancer;
        public int AvailableAmmo
        {
            get => _availableAmmo;
            set
            {
                if (value <= 0)
                {
                    _availableAmmo = 0;
                    return;
                }
                var maxAmmo = ((ILimitedAmmo)WeaponManager.GetPlayerWeapon(_playerProjectileInstancer.WeaponName))
                    .MaxAmmo;
                if (value >= maxAmmo)
                {
                    _availableAmmo = maxAmmo;
                    return;
                }
                _availableAmmo = value;
            }
        }

        private void Awake()
        {
            _playerProjectileInstancer = GetComponent<PlayerProjectileInstancer>();
            var instancer = GetComponent<ProjectileInstancer>();
            var projData = WeaponManager.GetPlayerWeapon(_playerProjectileInstancer.WeaponName) as ILimitedAmmo;
            AvailableAmmo = projData.InitialAmmo;
            instancer.OnPoppingProjectile += () => AvailableAmmo--;
            instancer.OnPushProjectile += () => AvailableAmmo++;
            instancer.OnCheckToInstance += () => instancer.CanInstantiate && _availableAmmo > 0;
            GetComponentInParent<Player>(true).AddAmmoCounter(_playerProjectileInstancer.WeaponName,this);
        }

        private IProjectileData GetProjectileData()
        {
            return WeaponManager.GetPlayerWeapon(_playerProjectileInstancer.WeaponName) as IProjectileData;
        }

    }
    
}