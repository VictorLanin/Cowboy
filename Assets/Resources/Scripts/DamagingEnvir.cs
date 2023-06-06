using System;
using UnityEngine;

namespace LaninCode
{
    public abstract class DamagingEnvironmentData : ScriptableObject,IWeaponData
    {
        public abstract EnvironmentDamageName Name { get; }
        public string NameOfWeapon => Name.ToString();
        public abstract int Damage { get; }

        public static IWeaponData CreateEnvironment(EnvironmentDamageName environmentDamageName)
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