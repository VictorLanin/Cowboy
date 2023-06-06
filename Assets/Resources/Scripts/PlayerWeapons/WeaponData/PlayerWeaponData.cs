
using UnityEngine;

namespace LaninCode
{
    public abstract class PlayerWeaponData:ScriptableObject,IWeaponData
    { 
        public abstract PlayerWeaponName Name { get; }
        public string NameOfWeapon => Name.ToString();
        public abstract int Damage { get; }
        public static PlayerWeaponData CreateInstance(PlayerWeaponName name)
        {
            PlayerWeaponData playerWeaponData;
            switch (name)
            {
                case PlayerWeaponName.Grenade:
                {
                    playerWeaponData = CreateInstance<GrenadeData>();
                    break;
                }
                default: playerWeaponData=CreateInstance<MachineGunData>();
                    break;
            }
            return playerWeaponData;
        }
    }
}