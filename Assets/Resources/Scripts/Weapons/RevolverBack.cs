
namespace LaninCode
{
    public class RevolverBack: WeaponBack,ILimitedAmmo,IDelay
    {
        public RevolverBack(WeaponName name, int damage, float delay, int maxAmmo, int reduceRate) : base(name, damage)
        {
            Delay = delay;
            MaxAmmo = maxAmmo;
            ReduceAmmoRate = reduceRate;
        }

        public float Delay { get; }
        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; }
        
    }
}