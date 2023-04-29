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
        private static readonly Dictionary<WeaponName, WeaponBack> _weapons=new ()
        {
            { WeaponName.MachineGun ,WeaponBack.CreateInstance(WeaponName.MachineGun)},
            { WeaponName.Grenade ,WeaponBack.CreateInstance(WeaponName.Grenade)}
        };
        private bool _isFiring;

        [SerializeField] protected TypeOfWeapon weaponType;

        public TypeOfWeapon WeaponType => weaponType;

        public WeaponBack EquippedWeapon { get; private set; }
        public WeaponInGameObject WeaponGameObject => this;
        
        public void SetWeapon(WeaponName nameOfWeapon)
        {
            if (!_weapons.Keys.Contains(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            EquippedWeapon= _weapons[nameOfWeapon];
        }

        public static WeaponBack GetWeaponBack(WeaponName nameOfWeapon)
        {
            if (!_weapons.Keys.Contains(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            return _weapons[nameOfWeapon];
        }
        
        
    }
}