using System;

namespace LaninCode
{
    public class MachineGunBack:WeaponBack, IDelay
    {
        private int _ammoCount;
        private bool _canDamage;
        public MachineGunBack(WeaponName name, int damage, float delay) : base(name, damage)
        {
            Delay = delay;
        }
        public float Delay { get; }

        public override bool CanDamage => _canDamage;

        public override void TryFiring(bool isFiring)
        {
            _canDamage = isFiring;
        }
    }
}