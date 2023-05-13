
namespace LaninCode
{
    public class GrenadeInstance : PlayerWeapon,ILimitedAmmo,IProjectile
    {
        private const int WeaponMaxAmmo=10;
        private const int InitialAmmo=2;
        private const int RedAmmoRate=1;
        private const float ProjSpeed=5f;
        private const float DelayToInstantiate=2f;
        private const int WeaponDamage=20;
        private int _availableAmmo=InitialAmmo;
        private ProjectileInstancer _instancer = ProjectileInstancer.CreateInstance(DelayToInstantiate);
        public int MaxAmmo => WeaponMaxAmmo;
        public int ReduceAmmoRate => RedAmmoRate;
        public float SpeedOfProjectile => ProjSpeed;
        public override PlayerWeaponName Name => PlayerWeaponName.Grenade;
        public override int Damage => WeaponDamage;
         
        public override bool CanDamage => true;
        private bool _isFiring;
        public ProjectileInstancer Instancer => _instancer;
        public int AvailableAmmo
        {
            get => _availableAmmo;
            set
            {
                if (value >= MaxAmmo)
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
           _isFiring=isFiring;
        }
        
        public bool CanInstantiate => _isFiring && AvailableAmmo > 0 && _instancer.CanInstantiate;
    }

}