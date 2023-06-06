namespace LaninCode
{
    public interface IWeapon
    {
        void TryFiring();
        IWeaponData WeaponData { get; }
        string NameofWeapon { get; }
        bool CanDamage { get; }
        void ApplyDamage(Destructible destructible);
    }
}