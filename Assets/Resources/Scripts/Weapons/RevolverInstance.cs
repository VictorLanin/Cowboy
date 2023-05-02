using System;

namespace LaninCode
{
    public class RevolverInstance : Weapon, ILimitedAmmo, IDamageDelay
    {
        private int _availableAmmo;
        private bool _canDamage;

        public RevolverInstance(int availableAmmo)
        {
            _availableAmmo = availableAmmo;
        }

        public override WeaponName Name => WeaponName.Revolver;
        public override int Damage => Revolver.Damage;
        public override bool CanDamage => _canDamage;


        public float DamageDelay => Revolver.DamageDelay;

        public int MaxAmmo => Revolver.MaxAmmo;
        public int ReduceAmmoRate => Revolver.ReduceAmmoRate;

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