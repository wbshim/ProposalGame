using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public GameObject[] ObjectsToPool;
    public int PooledAmount = 20;
    public bool WillGrow = true;
    public Transform PooledObjectsHolder;

    List<GameObject> pooledObjects;

	// Use this for initialization
	void Start () {
        pooledObjects = new List<GameObject>();
        // Pools objects randomly
        for(int i = 0; i < PooledAmount; i++)
        {
            int j = Random.Range(0, ObjectsToPool.Length);
            GameObject obj = (GameObject)Instantiate(ObjectsToPool[j]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(PooledObjectsHolder);
        }
	}
	
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if(WillGrow)
        {
            int j = Random.Range(0, ObjectsToPool.Length);
            GameObject obj = (GameObject)Instantiate(ObjectsToPool[j]);
            pooledObjects.Add(obj);
            obj.transform.SetParent(PooledObjectsHolder);
            return obj;
        }
        return null;
    }
}
