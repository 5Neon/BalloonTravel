using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Continue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        //���� �ð� Ȱ��ȭ
        Debug.Log("�ð� Ȱ��ȭ"); 
        Time.timeScale = 1.0f;
    }

}
