using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaninCode
{
    public class WeaponInGameObject : MonoBehaviour,IGetWeapon
    {
        public enum TypeOfWeapon
        {
            CursorBased,ProjectileBased
        }
        private static Dictionary<WeaponName, WeaponBack> _weapons=new ()
        {
            { WeaponName.MachineGun ,WeaponBack.CreateInstance(WeaponName.MachineGun)},
            { WeaponName.Grenade ,WeaponBack.CreateInstance(WeaponName.Grenade)}
        };
        private bool _isFiring;

        private Player _player;
        [SerializeField] protected TypeOfWeapon _weaponType;

        public TypeOfWeapon WeaponType
        {
            get => _weaponType;
        }

        public WeaponBack EquipedWeapon { get; private set; }
        public Func<bool> AdditionalConditions { get; set; } = null;
        public virtual bool CanDamage => AdditionalConditions==null?_isFiring:_isFiring && AdditionalConditions();

        public WeaponInGameObject WeaponGameObject => this;

        public int GetAmmo => _player.GetAmountOfAmmo();
        public Vector3 СursorPosition => _player.CursorPosistion;
        public virtual void StartFiring(bool isFiring)
        {
            _isFiring = isFiring;
        }

        public virtual void Awake()
        {
            _player = GetComponentInParent<Player>(true);
        }
        
        public void SetWeapon(WeaponName nameOfWeapon)
        {
            if (!_weapons.Keys.Contains(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            EquipedWeapon= _weapons[nameOfWeapon];
            
        }

        public static WeaponBack GetWeaponBack(WeaponName nameOfWeapon)
        {
            if (!_weapons.Keys.Contains(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            return _weapons[nameOfWeapon];
        }

        public void ApplyDamage(Destructable destructable)
        {
            EquipedWeapon.ApplyDamage(destructable);
            if (EquipedWeapon is not ILimitedAmmo) return;
            _player.ReduceAmmo(EquipedWeapon);
        }
        
    }
}