using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LaninCode
{
    //todo подумать об изменении системы ключей 
    public static class WeaponDataManager
    {
        public static readonly Dictionary<EnemyWeaponName, IWeapon> EnemyWeapons = new()
        {
            { EnemyWeaponName.FlyingEnemy,EnemyWeapon.CreateEnemyWeapon(EnemyWeaponName.FlyingEnemy)}
        };       
        
        private static readonly Dictionary<EnvironmentDamageName, IWeapon> EnvironmentsDamage = new()
        {
            { EnvironmentDamageName.Barrel,DamagingEnvironmentData.CreateEnvironment(EnvironmentDamageName.Barrel)}
        };

        public static IWeapon GetWeapon(string nameOfCol)
        {
            var weapon = Player.GetDamagingWeapon(nameOfCol);
            if (weapon != null) return weapon;
            EnemyWeaponName enemyWeaponName;
            var parsed=Enum.TryParse(nameOfCol,out enemyWeaponName);
            if (parsed) return EnemyWeapons[enemyWeaponName];
            EnvironmentDamageName environmentDamageName;
            parsed=Enum.TryParse(nameOfCol,out environmentDamageName);
            if (parsed) return EnvironmentsDamage[environmentDamageName];
            throw new InvalidEnumArgumentException($"cant parse {nameOfCol}");
        }
        

    }
}