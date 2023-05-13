using System;

namespace LaninCode
{
    public class RevolverInstance : PlayerWeapon, ILimitedAmmo, IDamageDelay
    {
        private bool _canDamage;
        private const float _damageDelay=3f;
        private const int _maxAmmo=20;
        private const int _reduceAmmoRate=1;
        private const int _damage=10;
        private const int _initialAmmo = 10;
        private int _availableAmmo=_initialAmmo;
        
        public override PlayerWeaponName Name => PlayerWeaponName.Revolver;
        public override int Damage => _damage;
        public override bool CanDamage => _canDamage;
        
        public float DamageDelay => _damageDelay;

        public int MaxAmmo => _maxAmmo;
        public int ReduceAmmoRate =>_reduceAmmoRate;

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

        public override void ApplyDamage(Destructible destructible)
        {
            base.ApplyDamage(destructible);
            AvailableAmmo -= ReduceAmmoRate;
        }

        public override void TryFiring(bool isFiring)
        {
            _canDamage = isFiring;
        }
    }
}