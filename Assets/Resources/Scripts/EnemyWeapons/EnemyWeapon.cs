using System;
using UnityEngine;

namespace LaninCode
{
    public abstract class EnemyWeapon:ScriptableObject,IWeapon
    {
        public abstract EnemyWeaponName Name { get; }
        public abstract int Damage { get;}
        public abstract bool CanDamage { get; }
        public IWeapon GetWeaponFromCollision(string nameOfCol)
        {
            return Enemy.GetWeapon(nameOfCol);
        }

        public void ApplyDamage(Destructible destructible)
        {
            destructible.CurrentHealth -= Damage;
        }

        public static IWeapon CreateEnemyWeapon(EnemyWeaponName enemyWeaponName)
        {
            switch (enemyWeaponName)
            {
                case EnemyWeaponName.Bullet:
                    break;
                case EnemyWeaponName.FlyingEnemy:
                    var enemyWeapon = CreateInstance<CollidedWeapon>();
                    enemyWeapon.Initialize(2,enemyWeaponName);
                    return enemyWeapon;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyWeaponName), enemyWeaponName, null);
            }
            return null;
        }
    }
}