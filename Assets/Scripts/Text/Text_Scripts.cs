using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Scripts : MonoBehaviour
{
    public GameObject gm; // 게임 매니져 

    public int Dialog_Content;          // 내부 대화 내용
    public int Dialog_Name;             // 내부 인물 이름
    public int Dialog_FinerContent;     // 마지막 페이지

    TextManager gamemanager;

    public  void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.isTalking = true;
            gamemanager = gm.GetComponent<TextManager>();   //참조를 위한 재선헌
            StartCoroutine(gamemanager.Dialogue(Dialog_Name, Dialog_Content, Dialog_FinerContent)); //코루틴 시작 함수
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
              
        }
    }
}
