
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LaninCode
{
    public class RocketData : EnemyWeaponData, IProjectileData
    {
        private Sprite _sprite;
        public float SpeedOfProjectile => 5f;
        public float DelayToInstantiate => 2f;
        public int HowManyInstances => 1;
        public Sprite SpriteOfProjectile => _sprite;

        private async void Awake()
        {
            var handle = Addressables.LoadAssetAsync<Sprite>("Rocket");
            await handle.Task;
            _sprite = handle.Task.Result;
        }
    }
}