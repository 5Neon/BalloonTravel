using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
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
                // Debug.Log("�ð� Ȱ��ȭ");
                Time.timeScale = 1.0f;
                menu.SetActive(false);
            }
            //�޴� true
            else
            {
                // Debug.Log("�ð� ��Ȱ��ȭ");
                Time.timeScale = 0f;
                menu.SetActive(true);
            }
        }
    }

    public void Restart()
    {
        // ���� �ð� Ȱ��ȭ
        // Debug.Log("�ð� Ȱ��ȭ");
        Time.timeScale = 1.0f;
    }

    public void Retry()
    {
        Debug.Log("�ٽ��ϱ� ����");
    }

    public void GameExit()
    {
        // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();   
        // Debug.Log("���� ����");
#endif
    }
}

