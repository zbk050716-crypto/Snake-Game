using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstaclePool : MonoBehaviour
{
    [Header("障碍物Prefab")]
    public GameObject obstaclePrefab;

    [Header("对象池大小")]
    public int poolSize = 20;

    private List<GameObject> pool =
        new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj =
                Instantiate(obstaclePrefab);

            obj.SetActive(false);

            pool.Add(obj);
        }
    }

    // 获取障碍物
    public GameObject GetObstacle()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);

                return pool[i];
            }
        }

        // 自动扩容
        GameObject newObj =
            Instantiate(obstaclePrefab);

        newObj.SetActive(true);

        pool.Add(newObj);

        return newObj;
    }
}
