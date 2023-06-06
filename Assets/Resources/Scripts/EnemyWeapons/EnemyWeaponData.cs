using System;
using UnityEngine;

namespace LaninCode
{
    public class EnemyWeaponData:ScriptableObject,IWeaponData
    {
        public EnemyWeaponName Name { get; private set; }
        public string NameOfWeapon => Name.ToString();
        public int Damage { get; private set;}
        public void Initialize(int damage,EnemyWeaponName nameOfEnemy)
        {
            Damage = damage;
            Name = nameOfEnemy;
        }
        public static IWeaponData CreateEnemyWeapon(EnemyWeaponName enemyWeaponName)
        {
            EnemyWeaponData enemyWeaponData;
            switch (enemyWeaponName)
            {
                case EnemyWeaponName.Rocket:
                    enemyWeaponData = CreateInstance<EnemyWeaponData>();
                    enemyWeaponData.Initialize(5,enemyWeaponName);
                    break;
                case EnemyWeaponName.FlyingEnemy:
                    enemyWeaponData = CreateInstance<EnemyWeaponData>();
                    enemyWeaponData.Initialize(10,enemyWeaponName);
                    return enemyWeaponData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyWeaponName), enemyWeaponName, null);
            }
            return null;
        }
    }
}