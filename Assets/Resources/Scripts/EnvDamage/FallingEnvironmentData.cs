namespace LaninCode
{
    public class FallingEnvironmentDataData : DamagingEnvironmentData
    {
        private int _damage;
        private EnvironmentDamageName _name;
        public override EnvironmentDamageName Name => _name;
        public override int Damage => _damage;
        public override bool CanDamage => true;

        public void Initialize(int damage, EnvironmentDamageName damageName)
        {
            _damage = damage;
            _name = damageName;
        }
        
    }
}