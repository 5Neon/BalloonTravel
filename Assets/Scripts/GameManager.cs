using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject menu;
     
    void Update()
    {
        //�޴� 
        if (Input.GetButtonDown("Cancel"))
        {


            if (menu.activeSelf)
            {
                Time.timeScale = 1;
                Debug.Log("�ð� Ȱ��ȭ");
                menu.SetActive(false);
                Paused = false;
            }
            else
            {
                Time.timeScale = 0;
                Debug.Log("�ð� ��Ȱ��ȭ");
                menu.SetActive(true);
                Paused = true;
            }
   
        }
        
    }


    public void GameExit()
    {

        // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
        
        Debug.Log("���� ����");
#endif
    }




}

