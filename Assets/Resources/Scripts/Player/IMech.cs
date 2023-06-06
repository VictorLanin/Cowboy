namespace LaninCode
{
    public interface IMech
    {
        PlayerWeaponName SelectedPlayerWeapon { get; }
        int MaxHealth { get; }
        PlayerWeaponName GetNextWeapon();
        PlayerWeaponName GetPreviousWeapon();
    }
}