using System;

namespace LaninCode
{
    public class MachineGunInstance : Weapon, IDamageDelay
    {
        private const int InflictedDamage=5;
        public const float DamDelay=2f;
        private bool _canDamage;
        public  float DamageDelay => DamDelay;
        public override WeaponName Name => WeaponName.MachineGun;
        public override int Damage=>InflictedDamage;
        public override bool CanDamage=>_canDamage;
        public override void TryFiring(bool isFiring)
        {
            _canDamage = isFiring;
        }
    }
}