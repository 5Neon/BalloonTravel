using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Button : MonoBehaviour
{
    public GameObject textmanager; // 텍스트 매니저 
    public GameObject player; // 플레이어 옮기는 용도

    public int Dialog_Content; // 내부 대화 내용
    public int Dialog_Name; // 내부 인물 이름
    public int Dialog_FinerContent; // 마지막 페이지

    public bool Button_Actvie;
    public bool O; //정답 버튼
    public bool button_O = true;

    TextManager gamemanager; // 재선헌 

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Button_Actvie)
        {
            StartCoroutine(buttenManager());
            GameManager.isTalking = true;
            if (!button_O)
            {
                GameManager.isTalking = false;
            }
        }
    }

    IEnumerator buttenManager()
    {
        float i = 0.001f;
        float temp = 0f;

        Vector3 down = new Vector3(0, -0.001f, 0);
        if (!O)
        {
            while (true)
            {
                yield return null;
                transform.position += down;
                temp += i;

                if (temp > 0.2f)
                {
                    yield return new WaitForSeconds(0.5f);
                    player.GetComponent<Player_Button_Move>().move();

                    while (true)
                    {
                        yield return null;
                        transform.position -= down;
                        temp -= i;
                        if (temp < 0f)
                        {
                            gamemanager = textmanager.GetComponent<TextManager>();                                      //참조를 위한 재선헌
                            StartCoroutine(gamemanager.Dialogue(Dialog_Name, Dialog_Content, Dialog_FinerContent));     //코루틴 시작 함수
                            yield break;
                        }
                    }
                }
            }
        }

        if (O)
        {
            while (button_O)
            {
                yield return null;
                transform.position += down;
                temp += i;

                if (temp > 0.2f)
                {
                    gamemanager = textmanager.GetComponent<TextManager>();                                      //참조를 위한 재선헌
                    StartCoroutine(gamemanager.Dialogue(Dialog_Name, Dialog_Content, Dialog_FinerContent));     //코루틴 시작 함수
                    button_O = false;
                    Button_Actvie = false;
                    yield break;
                }
            }
        }
    }
}
