using System;
using System.Collections.Generic;
using UnityEngine;


namespace LaninCode
{
    public class PlayerProjectileMediator : MonoBehaviour
    {
        private static Dictionary<WeaponName, ProjectileWeapon> _allProjectiles = new();

        private static void AddToProjectiles(WeaponName weaponName,ProjectileWeapon weapon)
        {
            _allProjectiles.Add(weaponName,weapon);
        }

        public static ProjectileWeapon GetProjectileWeapon(WeaponName weaponName)
        {
            return _allProjectiles[weaponName];
        }

        private Player _player;
        private void Awake()
        {
            _player = GetComponentInParent<Player>(true);
             var weapons = GetComponents<ProjectileWeapon>();
             for (int i = 0; i < weapons.Length; i++)
             {
                 weapons[i].GetPosition += () => _player.CursorPosistion;
                 AddToProjectiles(weapons[i].NameOfWeapon,weapons[i]);
             }
        }
    }
}