using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("身体预制体")]
    public GameObject bodyPrefab;

    [Header("初始池大小")]
    public int poolSize = 30;

    // 对象池列表
    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        // 提前创建对象
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bodyPrefab);

            obj.SetActive(false);

            pool.Add(obj);
        }
    }

    // 获取对象
    public GameObject GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);

                return pool[i];
            }
        }

        // 如果不够，自动扩容
        GameObject newObj = Instantiate(bodyPrefab);

        newObj.SetActive(true);

        pool.Add(newObj);

        return newObj;
    }
}