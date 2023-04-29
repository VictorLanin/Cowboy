using System;
using Unity;
namespace LaninCode
{
    public class GrenadeBack : WeaponBack, ILimitedAmmo,IProjectile
    {
        private int _availableAmmo;
        private ProjectileInstancer _instancer;
        private ProjectileWeapon _projectileWeapon;
        public GrenadeBack(WeaponName name, int damage, int availableAmmo, int maxAmmo, int reduceAmmoRate, float speedOfProjectile, float delayToInstantiate) : base(name, damage)
        {
            _availableAmmo = availableAmmo;
            _instancer = ProjectileInstancer.CreateInstance(delayToInstantiate);
            MaxAmmo = maxAmmo;
            ReduceAmmoRate = reduceAmmoRate;
            SpeedOfProjectile = speedOfProjectile;
            DelayToInstantiate = delayToInstantiate;
        }

        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; }
        public float SpeedOfProjectile { get; }
        public bool CanInstantiate { get; } = true;
        public float DelayToInstantiate { get; }

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

        public override bool CanDamage { get; } = true;
        public override void TryFiring(bool isFiring)
        {
            if (!isFiring || _availableAmmo==0 || !_instancer.CanInstantiate) return;
            _projectileWeapon ??= PlayerProjectileMediator.GetProjectileWeapon(Name);
            _projectileWeapon.StartCoroutine(_instancer.Delay());
            _projectileWeapon.InstantiateProjectile();
        }
    }

}