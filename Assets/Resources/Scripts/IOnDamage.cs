namespace LaninCode
{
    public interface IOnDamage
    {
        void SetHealth(int health);
        int MaxHealth { get; }
    }
}