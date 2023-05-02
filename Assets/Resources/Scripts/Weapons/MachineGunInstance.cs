using System;

namespace LaninCode
{
    public class MachineGunInstance : Weapon, IDamageDelay
    {
        private bool _canDamage;

        public float DamageDelay => MachineGun.DamageDelay;

        public override WeaponName Name => WeaponName.MachineGun;
        public override int Damage=>MachineGun.Damage;
        public override bool CanDamage=>_canDamage;

        public override void TryFiring(bool isFiring)
        {
            _canDamage = isFiring;
        }
    }
}