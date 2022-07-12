using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Change : MonoBehaviour
{
    public GameObject Change_Object;    //바꿀 오브젝트 리스트
    public GameObject Change2_Object;
    public GameObject Change3_Object;
    public GameObject Change4_Object;
    public GameObject Change5_Object;

    public int Talking_End; // 대화 끝 정하기

    public bool roop;       // 루프 체크
    bool Roop;              // 루프 여부

    int roop_int = 0;       // 루프 문단 체크용
    public int Roop_int;    // 루프 문단


    int Talking = 0;                // 대화 카운트
    bool Talking_End_bool = false;  // 대화 멈춤

    public void RoopTalking()
    {
        Roop = false;
        Change_object(false);
    }

    public void OnTriggerExit(Collider other)
    {
        Change_object(Talking_End_bool);
    }



    public void Change_object(bool Talking_End_bool)      // 바꿔치기 함수
    {
        if (Talking == Talking_End - 1)                   // 대화 종료
        {
            Talking_End_bool = true;
        }

        if (roop)   // 루프 활성화
        {
            if (roop && roop_int == 1)
            {
                Roop = true;
                roop_int = 0;
            }
            else if (roop && roop_int == 2)
            {
                Roop = true;
                roop_int = 0;
            }
            else if (roop && roop_int == 3)
            {
                Roop = true;
                roop_int = 0;
            }
            else if (roop && roop_int == 4)
            {
                Roop = true;
                roop_int = 0;
            }
            else if (roop && roop_int == 5)
            {
                Roop = true;
                roop_int = 0;
            }
        }



        if (!Talking_End_bool && !Roop)      //대화 넘김
        {
            if (Talking == 0)
            {
                Change_Object.SetActive(false);
                Change2_Object.SetActive(true);
                Talking++;
                roop_int++;
            }
            else if (Talking == 1)
            {
                Change2_Object.SetActive(false);
                Change3_Object.SetActive(true);
                Talking++;
                roop_int++;
            }
            else if (Talking == 2)
            {
                Change3_Object.SetActive(false);
                Change4_Object.SetActive(true);
                Talking++;
                roop_int++;
            }
            else if (Talking == 3)
            {
                Change4_Object.SetActive(false);
                Change5_Object.SetActive(true);
            }

        }        
    }
}
