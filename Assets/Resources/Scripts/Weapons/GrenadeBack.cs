namespace LaninCode
{
    public class GrenadeBack : WeaponBack, ILimitedAmmo,IProjectile
    {
        public GrenadeBack(WeaponName name, int damage, int maxAmmo, int reduceAmmoRate, float speedOfProjectile) : base(name, damage)
        {
            MaxAmmo = maxAmmo;
            ReduceAmmoRate = reduceAmmoRate;
            SpeedOfProjectile = speedOfProjectile;
        }

        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; }
        public float SpeedOfProjectile { get; }

       
    }

}