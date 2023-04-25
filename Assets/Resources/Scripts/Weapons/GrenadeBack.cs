namespace LaninCode
{
    public class GrenadeBack : WeaponBack, ILimitedAmmo
    {
        public GrenadeBack(string name, int damage, int maxAmmo) : base(name, damage)
        {
            MaxAmmo = maxAmmo;
        }
        public int MaxAmmo { get; }
    }

}