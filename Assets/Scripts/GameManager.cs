using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
     
    void Update()
    {
        //�޴� 
        if (Input.GetButtonDown("Cancel"))
        {

            //�޴� false
            if (menu.activeSelf)
            {
                Time.timeScale = 1.0f;
                Debug.Log("�ð� Ȱ��ȭ");
                menu.SetActive(false);
                
            }
            //�޴� true
            else
            {
                Time.timeScale = 0f;
                Debug.Log("�ð� ��Ȱ��ȭ");
                menu.SetActive(true);
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

