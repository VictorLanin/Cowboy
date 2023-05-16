using System;

namespace LaninCode
{
    public class MachineGunInstance : PlayerWeapon, IDamageDelay
    {
        private const int InflictedDamage=5;
        public const float DamDelay=1f;
        private bool _canDamage;
        public  float DamageDelay => DamDelay;
        public override PlayerWeaponName Name => PlayerWeaponName.MachineGun;
        public override int Damage=>InflictedDamage;
        public override bool CanDamage=>_canDamage;
        public override void TryFiring(bool isFiring)
        {
            _canDamage = isFiring;
        }
    }
}