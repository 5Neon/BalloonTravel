using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Inout : MonoBehaviour
{
    public GameObject gm; // 게임 매니져 

    public int Dialog_Content;  // 내부 대화 내용
    public int Dialog_Name;     // 내부 인물 이름

    TextManager gamemanager;    // 재선헌 

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gamemanager = gm.GetComponent<TextManager>(); //참조를 위한 재선헌
            StartCoroutine(gamemanager.Dialogue_Inout(Dialog_Content, Dialog_Name)); //코루틴 시작 함수
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gamemanager = gm.GetComponent<TextManager>(); //참조를 위한 재선헌
            StartCoroutine(gamemanager.Stop_Dialogue()); //코루틴
        }
    }

}
