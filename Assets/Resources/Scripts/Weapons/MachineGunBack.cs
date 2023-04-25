namespace LaninCode
{
    public class MachineGunBack:WeaponBack, IDelay
    {
        private int _ammoCount;
        
        public MachineGunBack(string name, int damage, float delay) : base(name, damage)
        {
            Delay = delay;
        }
        
        public float Delay { get; }
    }
}