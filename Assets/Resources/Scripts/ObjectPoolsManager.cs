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
            dict.Add("Fly",CreatePoolWrapper("Fly",8));
            dict.Add("SmallHealth",CreatePoolWrapper("SmallHealth",4));
            return dict;
        }

        public static IPoolable Get(string nameOfItem)
        {
            if (!_pools.ContainsKey(nameOfItem)) throw new KeyNotFoundException($"{nameOfItem} not found");
            return _pools[nameOfItem].Get();
        }

        public static void Release(IPoolable poolable)
        {
            _pools[poolable.Name].Release(poolable);
        }

        private static ObjectPoolWrapper CreatePoolWrapper(string nameOfItem, int amountOfItems)
        {
            var poolWrapper = ScriptableObject.CreateInstance<ObjectPoolWrapper>();
            poolWrapper.CreatePoolWrapper(nameOfItem,amountOfItems);
            return poolWrapper;
        }
    }
}