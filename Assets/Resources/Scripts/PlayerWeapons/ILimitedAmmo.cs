namespace LaninCode
{
    public interface ILimitedAmmo:IWeapon
    {
        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; } 
        public int AvailableAmmo { get; set; }

    }
}