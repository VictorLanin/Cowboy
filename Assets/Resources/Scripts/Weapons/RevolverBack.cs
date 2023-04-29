using System;

namespace LaninCode
{
    public class RevolverBack : WeaponBack, ILimitedAmmo, IDelay
    {
        private int _availableAmmo;
        private bool _canDamage;
        public RevolverBack(WeaponName name, int damage, int availableAmmo, float delay, int maxAmmo,
            int reduceAmmoRate) : base(name, damage)
        {
            _availableAmmo = availableAmmo;
            Delay = delay;
            MaxAmmo = maxAmmo;
            ReduceAmmoRate = reduceAmmoRate;
        }

        public override bool CanDamage => _canDamage;


        public float Delay { get; }
        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; }

        public int AvailableAmmo
        {
            get => _availableAmmo;
            set
            {
                if (value <= MaxAmmo)
                {
                    _availableAmmo = MaxAmmo;
                    return;
                }

                if (value <= 0)
                {
                    _availableAmmo = 0;
                    return;
                }
                _availableAmmo = value;
            }
        }

        public override void ApplyDamage(Destructable destructable)
        {
            base.ApplyDamage(destructable);
            AvailableAmmo -= ReduceAmmoRate;
        }

        public override void TryFiring(bool isFiring)
        {
            _canDamage = isFiring;
        }
    }
}