namespace LaninCode
{
    public interface IWeapon
    {
        public int Damage { get; }
        public bool CanDamage { get;  }

        void ApplyDamage(Destructible destructible);
    }
}