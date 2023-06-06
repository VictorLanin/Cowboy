using System.Collections;
using System.Collections.Generic;
using LaninCode;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FallDamageTest
    {
        private Player _player;
        private Enemy _enemy;

        [UnitySetUp]
        public void Init()
        {
            var playerPrefab = UnityEngine.Resources.Load($"Prefabs/PlayerOne");
            var playerGo =  GameObject.Instantiate(playerPrefab) as GameObject;
            playerGo.name = "PlayerOne";
            _player = playerGo.GetComponent<Player>();
            var playerCursor = playerGo.GetComponentInChildren<PlayerCursor>(true);
            var damageEnv = new GameObject();
            
        }

        [UnityTest]
        public IEnumerator HealthPowerUpTest()
        {
            yield return null;
        }
    }
}