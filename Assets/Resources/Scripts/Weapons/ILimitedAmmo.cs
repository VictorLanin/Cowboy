namespace LaninCode
{
    public interface ILimitedAmmo
    {
        public int MaxAmmo { get; }
        public int ReduceAmmoRate { get; }
    }
}