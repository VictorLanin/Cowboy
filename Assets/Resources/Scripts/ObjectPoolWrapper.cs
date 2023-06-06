
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    public class ObjectPoolWrapper:ScriptableObject
    {
        private string _nameOfItem;
        private int _amountOfItems;
        private const string Address = "Prefabs/Poolable/";
        private ObjectPool<IPoolable> _objectPool;
        
        public void CreatePoolWrapper(string nameOfItem, int amountOfItems)
        {
            _nameOfItem = nameOfItem;
            _amountOfItems = amountOfItems;
            _objectPool = new ObjectPool<IPoolable>(OnCreate,
                (IPoolable poolable) => poolable.Activate(OnOff.On),
                (IPoolable poolable) => poolable.Activate(OnOff.Off),
                (poolable) => poolable.Destroy());

            for (int i = 0; i < amountOfItems; i++)
            {
                OnCreate();
            }
        }
        


        private IPoolable OnCreate()
        {
            var prefab = UnityEngine.Resources.Load<GameObject>(Address + _nameOfItem);
            var go = Instantiate(prefab); 
            var poolable=go.GetComponent(typeof(IPoolable)) as IPoolable;
            poolable.SetPool(_objectPool);
            return poolable;
        }

        public IPoolable Get()
        {
            return _objectPool.Get();
        }

        public void Release(IPoolable poolable)
        {
            _objectPool.Release(poolable);
        }
        
    }
}