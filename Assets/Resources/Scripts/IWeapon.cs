namespace LaninCode
{
    public interface IWeapon
    {
        public int Damage { get; }
        public bool CanDamage { get;  }
        public IWeapon GetWeaponFromCollision(string nameOfCol);
        void ApplyDamage(Destructible destructible);
    }
}