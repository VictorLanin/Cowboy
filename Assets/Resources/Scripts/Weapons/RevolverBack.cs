
namespace LaninCode
{
    public class RevolverBack: WeaponBack,ILimitedAmmo,IDelay
    {
        public RevolverBack(string name, int damage, float delay) : base(name, damage)
        {
            Delay = delay;
        }
        public float Delay { get; }
        public int MaxAmmo { get; }
    }
}