using System;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    public class GameManager : MonoBehaviour
    {
        public static Player MainPlayer { get; private set; }
        
        private void Awake()
        {
            MainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        
    }
}