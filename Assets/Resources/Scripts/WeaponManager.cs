using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LaninCode
{
    //todo подумать об изменении системы ключей 
    public static class WeaponManager
    {
        private static readonly Dictionary<EnemyWeaponName, IWeaponData> EnemyWeapons = new()
        {
            { EnemyWeaponName.FlyingEnemy,EnemyWeaponData.CreateEnemyWeapon(EnemyWeaponName.FlyingEnemy)}
        };       
        
        private static readonly Dictionary<EnvironmentDamageName, IWeaponData> EnvironmentsDamage = new()
        {
            { EnvironmentDamageName.Barrel,DamagingEnvironmentData.CreateEnvironment(EnvironmentDamageName.Barrel)}
        };

        private static readonly Dictionary<PlayerWeaponName, IWeaponData> PlayerWeapons= new()
        {
            { PlayerWeaponName.MachineGun, PlayerWeaponData.CreateInstance(PlayerWeaponName.MachineGun) },
            { PlayerWeaponName.Grenade, PlayerWeaponData.CreateInstance(PlayerWeaponName.Grenade) }
        };
        public static IWeaponData GetEnemyWeapon(EnemyWeaponName enemyWeaponName)
        {
            if (!EnemyWeapons.ContainsKey(enemyWeaponName))
                throw new KeyNotFoundException($"key {enemyWeaponName} not found");
            return EnemyWeapons[enemyWeaponName];
        }

        public static IWeaponData GetPlayerWeapon(PlayerWeaponName playerWeaponName)
        {
            if (!PlayerWeapons.ContainsKey(playerWeaponName))
                throw new KeyNotFoundException($"key {playerWeaponName} not found");
            return PlayerWeapons[playerWeaponName];
        }
        
        
        public static IWeapon GetWeapon(string nameOfCol)
        {
            var weapon = Player.GetDamagingWeapon(nameOfCol);
            if (weapon != null) return weapon;
            // EnemyWeaponName enemyWeaponName;
            // parsed=Enum.TryParse(nameOfCol,out enemyWeaponName);
            // if (parsed) return EnemyWeapons[enemyWeaponName];
            // EnvironmentDamageName environmentDamageName;
            // parsed=Enum.TryParse(nameOfCol,out environmentDamageName);
            // if (parsed) return EnvironmentsDamage[environmentDamageName];
            throw new InvalidEnumArgumentException($"cant parse {nameOfCol}");
        }
        
        
        
    }
}