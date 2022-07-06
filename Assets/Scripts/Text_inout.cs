using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Inout : MonoBehaviour
{
    public GameObject gm; // ���� �Ŵ��� 

    public int Dialog_Content; // ���� ��ȭ ����
    public int Dialog_Name; // ���� �ι� �̸�
    public int Dialog_FinerContent; // ������ ������

    GameManager gamemanager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gamemanager = gm.GetComponent<GameManager>(); //������ ���� �缱��
            StartCoroutine(gamemanager.Dialogue(Dialog_Content, Dialog_Name, Dialog_FinerContent)); //�ڷ�ƾ ���� �Լ�
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gamemanager = gm.GetComponent<GameManager>(); //������ ���� �缱��
            StartCoroutine(gamemanager.Stop_Dialogue()); //�ڷ�ƾ
        }
    }
}
