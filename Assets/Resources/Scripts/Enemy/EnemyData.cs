using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LaninCode
{
    public class EnemyData:ScriptableObject
    {
        public int MaxHealth { get; private set; } = 0;

        public List<EnemyWeaponName> AvailableWeapons { get; private set; }

        public void Initialize(int maxHealth, List<EnemyWeaponName> list)
        {
            AvailableWeapons = list;
            MaxHealth = maxHealth;
        }

        private static Dictionary<string, EnemyData> _enemies = new()
        {
            { "Fly", CreateEnemy("Fly") }
        };
        public static EnemyData GetEnemyData(string nameOfEnemy)
        {
            if (!_enemies.ContainsKey(nameOfEnemy)) throw new NullReferenceException($"{nameOfEnemy} not found");
            return _enemies[nameOfEnemy];
        }

        public static EnemyData CreateEnemy(string nameOfEnemy)
        {
            switch (nameOfEnemy)
            {
              case "Fly":
                  var data = CreateInstance<EnemyData>();
                  data.Initialize(20, new List<EnemyWeaponName> { EnemyWeaponName.Rocket });
                  return data;
            }

            throw new KeyNotFoundException($"{nameOfEnemy} not found");
        }
    }
}