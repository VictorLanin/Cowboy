using UnityEngine;

namespace LaninCode
{
    public class SmallHealthPowerUp : PowerUp
    {
        private const int HealthAddOn=20;
        private const string SpriteName = "Small_Health";
        private static Sprite _healthSprite;
        public override void Awake()
        {
            base.Awake();
            _healthSprite = LoadSprite(SpriteName);
            Destruct.SetSprite(_healthSprite);
        }

        public override void AddPowerUp()
        {
            PlayerToGetPowerUp.AddHealth(HealthAddOn);
        }
    }
}