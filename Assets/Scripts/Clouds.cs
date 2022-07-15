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

    //void Start()
    //{
    //    for(int i = 0; i < 2; i++)
    //    {
    //        randomPositionX = Random.Range(-200f, 200f);
    //        randomPositionY = Random.Range(-40f, -20f);
    //        randomPositionZ = Random.Range(-200f, 200f);

    //        //Instantiate(CloudPrefabs[i], playerPosition.transform.position, CloudPrefabs[i].transform.rotation);
    //        //transform.SetParent(playerPosition.transform);
    //    }
    //}

    void Update()
    {
        //Instantiate()
    }

}
