using UnityEngine;

namespace LaninCode
{
    public abstract class PlayerWeapon:IWeapon
    { 
        public abstract PlayerWeaponName Name { get; }
        public abstract int Damage { get; }
        public abstract bool CanDamage { get;  }
        public IWeapon GetWeaponFromCollision(string nameOfCol)
        {
            return Player.GetDamagingWeapon(nameOfCol);
        }

        public static PlayerWeapon CreateInstance(PlayerWeaponName name)
        {
            PlayerWeapon playerWeapon;
            switch (name)
            {
                case PlayerWeaponName.Grenade:
                {
                    playerWeapon = new GrenadeInstance();
                    break;
                }
                default: playerWeapon=new MachineGunInstance();
                    break;
            }
            return playerWeapon;
        }

        public virtual void ApplyDamage(Destructible destructible)
        {
            destructible.GetDamage(Damage);
        }
        public abstract void TryFiring(bool isFiring);
    }
}