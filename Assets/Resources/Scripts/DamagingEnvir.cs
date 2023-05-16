using System;
using UnityEngine;

namespace LaninCode
{
    public abstract class DamagingEnvironmentData : ScriptableObject,IWeapon
    {
        public abstract EnvironmentDamageName Name { get; }
        public abstract int Damage { get; }
        public abstract bool CanDamage { get; }
        public void ApplyDamage(Destructible destructible)
        {
            destructible.GetDamage(Damage);
        }

        public static IWeapon CreateEnvironment(EnvironmentDamageName environmentDamageName)
        {
            switch (environmentDamageName)
            {
                case EnvironmentDamageName.Barrel:
                    var weapon = CreateInstance<FallingEnvironmentDataData>();
                    weapon.Initialize(50,EnvironmentDamageName.Barrel);
                    return weapon;
                default:
                    throw new ArgumentOutOfRangeException(nameof(environmentDamageName), environmentDamageName, null);
            }
        }



    }
}