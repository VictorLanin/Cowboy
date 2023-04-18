using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class ObjectPoolsManager : MonoBehaviour
    {
        private static Dictionary<string, ObjectPoolWrapper> _pools;

        private void Awake()
        {
            _pools = CreateWrappers();
        }

        private Dictionary<string, ObjectPoolWrapper> CreateWrappers()
        {
            var dict = new Dictionary<string, ObjectPoolWrapper>();
            dict.Add("Fly",ObjectPoolWrapper.CreateInstance("Fly",10));
            return dict;
        }

        public static Poolable Get(string nameOfItem)
        {
            return _pools[nameOfItem].Get();
        }

        public static void Release(Poolable poolable)
        {
            _pools[poolable.Name].Release(poolable);
        }
    }
}