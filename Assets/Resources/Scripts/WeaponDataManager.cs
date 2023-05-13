using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public static class WeaponDataManager
    {
        private static readonly Dictionary<EnemyWeaponName, IWeapon> _enemyWeapons = new()
        {
            { EnemyWeaponName.FlyingEnemy,EnemyWeapon.CreateEnemyWeapon(EnemyWeaponName.FlyingEnemy)}
        };

        public static IWeapon GetWeapon(string nameOfCol)
        {
            var weapon = Player.GetDamagingWeapon(nameOfCol);
            if (weapon != null) return weapon;
            var key = Enum.Parse<EnemyWeaponName>(nameOfCol);
            return _enemyWeapons[key];
        }
        
    }
}