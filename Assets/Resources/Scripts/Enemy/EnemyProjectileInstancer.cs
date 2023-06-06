using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class EnemyProjectileInstancer : MonoBehaviour
    {
        [SerializeField] private EnemyWeaponName enemyWeaponName;
        private ProjectileInstancer _instancer; 
        public EnemyWeaponName WeaponName => enemyWeaponName; 
        private Enemy _enemy;

        private void Awake()
        {
            _instancer = GetComponent<ProjectileInstancer>();
            _enemy = GetComponentInParent<Enemy>();
            _instancer.GetProjectileData += GetProjectileData;
            _instancer.GetPositions += () => new List<Vector3>{Player.GetPlayerPosition()};
            StartCoroutine(_instancer.SetProjectilesReady(_enemy.Name));
        }

        private IProjectileData GetProjectileData()
        {
            return WeaponManager.GetEnemyWeapon(WeaponName) as IProjectileData;
        }
        
        
    }
}