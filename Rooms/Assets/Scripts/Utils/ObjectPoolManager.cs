using System.Collections.Generic;
using System.Linq;
//using NUnit.Framework.Constraints;
//using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public List<ObjectPool> ObjectPools;

        private static ObjectPoolManager _objectPoolManager;

        public static ObjectPoolManager Instance
        {
            get
            {
                if (_objectPoolManager == null)
                {
                    var opManager = new GameObject("ObjectPoolManager");
                    opManager.AddComponent<ObjectPoolManager>();
                }

                return _objectPoolManager;
            }
        }

        private void Awake()
        {
            _objectPoolManager = this;
        }

        public GameObject AddItem(GameObject prefab)
        {
            ObjectPool sop = GetObjectPool(prefab);
            GameObject newItem = sop.GetObject();
            ;
            return newItem;
        }

        public void RemoveItem(GameObject item)
        {
            ObjectPool sop = GetObjectPool(item);
            sop.ReturnObject(item);
        }

        private ObjectPool GetObjectPool(GameObject item)
        {
            if (ObjectPools != null)
            {
                ObjectPool pool = ObjectPools.FirstOrDefault(p =>
                    p.Prefab.CompareTag(item.tag));
                if (pool != null)
                    return pool;
            }

            return CreatePool(item);
        }

        private ObjectPool CreatePool(GameObject prefab)
        {
            ObjectPool newSop = prefab.AddComponent<ObjectPool>();
            ObjectPools.Add(newSop);
            return newSop;
        }
    }
}
