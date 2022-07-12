using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_npc_1 : MonoBehaviour
{
    public GameObject textmanager;      // 텍스트 매니저 
    

    public int Dialog_Content;          // 내부 대화 내용
    public int Dialog_Name;             // 내부 인물 이름
    public int Dialog_FinerContent;     // 마지막 페이지

    TextManager gamemanager;            // 재선헌

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.isTalking = true;
            gamemanager = textmanager.GetComponent<TextManager>();                                  //참조를 위한 재선헌
            StartCoroutine(gamemanager.Dialogue(Dialog_Name, Dialog_Content, Dialog_FinerContent)); //코루틴 시작 함수
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gamemanager = textmanager.GetComponent<TextManager>(); //참조를 위한 재선헌
            StartCoroutine(gamemanager.Stop_Dialogue()); //코루틴
        }
    }



}
