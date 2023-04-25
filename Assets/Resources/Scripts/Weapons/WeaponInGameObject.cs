using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaninCode
{
    public class WeaponInGameObject : MonoBehaviour
    {
        private static Dictionary<string, WeaponBack> _weapons;
        private bool _canDamage;
        public  WeaponBack CurrentWeapon { get; private set; }

        public bool CanDamage => _canDamage;

        public virtual void SetCanDamage(bool canDamage)
        {
            _canDamage = canDamage;
        }

        protected virtual void Awake()
        {
            _weapons = CreateDict();
            
            Dictionary<string, WeaponBack> CreateDict()
            {
                var dict = new Dictionary<string, WeaponBack>();
                var weapon=WeaponBack.CreateInstance("MachineGun");
                dict.Add(weapon.Name,weapon);
                return dict;
            }
        }
        
        public void SetWeapon(string nameOfWeapon)
        {
            if (!_weapons.Keys.Contains(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            CurrentWeapon= _weapons[nameOfWeapon];
        }

        public static WeaponBack GetWeaponBack(string nameOfWeapon)
        {
            if (!_weapons.Keys.Contains(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            return _weapons[nameOfWeapon];
        }
        
    }
}