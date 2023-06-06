namespace LaninCode
{
    public interface ILimitedAmmo:IWeaponData
    {
        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; }
        public int InitialAmmo { get; }

    }
}