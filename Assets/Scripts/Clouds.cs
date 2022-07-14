using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject[] CloudPrefabs;
    public Transform playerPosition;

    float randomPositionX;
    float randomPositionY;
    float randomPositionZ;

    void Start()
    {
        randomPositionX = Random.Range(-200f, 200f);
        randomPositionY = Random.Range(-40f, -20f);
        randomPositionZ = Random.Range(-200f, 200f);

        //Instantiate(CloudPrefabs, playerPosition.transform.position + new Vector3(randomPositionX, randomPositionY, randomPositionZ), playerPosition.transform.rotation * Quaternion.Euler(playerPosition.transform.rotation.x, playerPosition.transform.rotation.y, Random.Range(-90f, 90f)));
    }

    void Update()
    {
        //Instantiate()
    }
}
