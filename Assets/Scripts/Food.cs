using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("地图边界")]
    public float boundX = 30f;
    public float boundZ = 30f;

    void Start()
    {
        RandomSpawn();
    }

    public void RandomSpawn()
    {
        float x = Random.Range(-boundX, boundX);
        float z = Random.Range(-boundZ, boundZ);

        transform.position = new Vector3(
            Mathf.Round(x),
            0.5f,
            Mathf.Round(z)
        );
    }
}
