using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaninCode
{
    public class WeaponCursor : MonoBehaviour
    {
        private static Dictionary<string, WeaponBack> _weapons;
        public WeaponBack CurrentWeapon { get; private set; }
        public bool IsShooting { get; set; }
        private void Awake()
        {
            GameManager.UsedCursors.Add(this);
            _weapons = CreateDict();
            
            Dictionary<string, WeaponBack> CreateDict()
            {
                var dict = new Dictionary<string, WeaponBack>();
                var weapon=WeaponBack.CreateInstance("MachineGun");
                dict.Add(weapon.Name,weapon);
                weapon = WeaponBack.CreateInstance("Pistol");
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
        
    }
}