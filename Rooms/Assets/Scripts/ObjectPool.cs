﻿using UnityEngine;
using System.Collections.Generic;


public class ObjectPool : MonoBehaviour
{
    // the prefab that this object pool returns instances of
    public GameObject Prefab;
    
    
    
    private static ObjectPool _objectPool;
    private ObjectPool(){}
    // collection of currently inactive instances of the prefab
    private Stack<GameObject> _inactiveInstances = new Stack<GameObject>();
    
    public static ObjectPool Instance
    {
        get
        {
            if (_objectPool == null)
            {
                var dc = new GameObject("ObjectPool");
                dc.AddComponent<ObjectPool>();
            }

            return _objectPool;
        }
    }
    
    private void Awake()
    {
        _objectPool = this;
    }
    
    // Returns an instance of the prefab
    public GameObject GetObject(GameObject prefab) 
    {
        GameObject spawnedGameObject;
        
        // if there is an inactive instance of the prefab ready to return, return that
        if (_inactiveInstances.Count > 0) 
        {
            // remove the instance from teh collection of inactive instances
            spawnedGameObject = _inactiveInstances.Pop();
        }
        // otherwise, create a new instance
        else 
        {
            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);
            
            // add the PooledObject component to the prefab so we know it came from this pool
            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.Pool = this;
        }
        
        // put the instance in the root of the scene and enable it
        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);
        
        // return a reference to the instance
        return spawnedGameObject;
    }
    
    // Return an instance of the prefab to the pool
    public void ReturnObject(GameObject toReturn) 
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();
        
        // if the instance came from this pool, return it to the pool
        if(pooledObject != null && pooledObject.Pool == this)
        {
            // make the instance a child of this and disable it
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);
            
            // add the instance to the collection of inactive instances
            _inactiveInstances.Push(toReturn);
        }
        // otherwise, just destroy it
        else
        {
            Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(toReturn);
        }
    }
}

// a component that identifies the pool that a GameObject came from
public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool;
}