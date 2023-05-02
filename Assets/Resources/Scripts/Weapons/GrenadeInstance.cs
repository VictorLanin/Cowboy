
namespace LaninCode
{
    public class GrenadeInstance : Weapon,ILimitedAmmo,IProjectile
    {
        private int _availableAmmo;
        private ProjectileInstancer _instancer;
        public GrenadeInstance()
        {
            _availableAmmo = Grenade.InitialAmmo;
            _instancer = ProjectileInstancer.CreateInstance(DelayToInstantiate);
        }

        public int MaxAmmo => Grenade.MaxAmmo;
        public int ReduceAmmoRate => Grenade.ReduceAmmoRate;
        public float SpeedOfProjectile => Grenade.SpeedOfProjectile;
        public float DelayToInstantiate => Grenade.DelayToInstantiate;
        public override WeaponName Name => WeaponName.Grenade;
        public override int Damage => Grenade.Damage;
        public override bool CanDamage => true;

        private bool _isFiring;
        
        public ProjectileInstancer Instancer => _instancer;
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
           _isFiring=isFiring;
        }
        
        public bool CanInstantiate => _isFiring && AvailableAmmo > 0 && _instancer.CanInstantiate;
    }

}