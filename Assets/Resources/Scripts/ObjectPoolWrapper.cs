using Tests;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Windows.WebCam;

namespace LaninCode
{
    public class ObjectPoolWrapper
    {
        private string _nameOfItem;
        private int _amountOfItems;
        private const string Address = "Assets/Prefabs/Poolable/";
        private ObjectPool<Poolable> _objectPool;


        private ObjectPoolWrapper(string nameOfItem, int amountOfItems)
        {
            _nameOfItem = nameOfItem;
            _amountOfItems = amountOfItems;
            _objectPool = new ObjectPool<Poolable>(OnCreate, 
                (Poolable poolable) => poolable.Activate(OnOff.On),
                (Poolable poolable) => poolable.Activate(OnOff.Off),
                (Poolable poolable) => GameObject.Destroy(poolable.gameObject),true,amountOfItems);

        }


        public static ObjectPoolWrapper CreateInstance(string nameOfItem, int amountOfItems)
        {
            return new ObjectPoolWrapper(nameOfItem, amountOfItems);
        }

        private Poolable OnCreate()
        {
            var prefab = UnityEngine.Resources.Load<GameObject>(Address + _nameOfItem);
            var go = GameObject.Instantiate(prefab);
            return go.GetComponent<Poolable>();
        }

        public Poolable Get()
        {
            return _objectPool.Get();
        }

        public void Release(Poolable poolable)
        {
            _objectPool.Release(poolable);
        }
        
    }
}