using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_UI : MonoBehaviour
{
    public GameObject[] Elphis;
    public GameObject[] Plila;
    public GameObject[] Demos;
    public GameObject[] Gerffa;

    public IEnumerator Text_UI_image(int Content) // 캐릭터 UI
    {
        yield return null;

        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");

        string charater = data_Dialog[Content]["UI"].ToString();

        Elphis[0].SetActive(false);
        Elphis[1].SetActive(false);
        Elphis[2].SetActive(false);
        Elphis[3].SetActive(false);
        Elphis[4].SetActive(false);

        Plila[0].SetActive(false);
        Plila[1].SetActive(false);
        Plila[2].SetActive(false);
        Plila[3].SetActive(false);
        Plila[4].SetActive(false);
        Plila[5].SetActive(false);
        Plila[6].SetActive(false);

        Demos[0].SetActive(false);
        Demos[1].SetActive(false);
        Demos[2].SetActive(false);

        Gerffa[0].SetActive(false);
        Gerffa[1].SetActive(false);
        Gerffa[2].SetActive(false);
        Gerffa[3].SetActive(false);


        if (charater == "Elphis_nomal")  // 주인공
        {
            Elphis[0].SetActive(true);
        }
        if (charater == "Elphis_happy")
        {
            Elphis[1].SetActive(true);
        }
        if (charater == "Elphis_bed")
        {
            Elphis[2].SetActive(true);
        }
        if (charater == "Elphis_surprised")
        {
            Elphis[3].SetActive(true);
        }
        if (charater != "Elphis_nomal" && charater != "Elphis_happy" && charater != "Elphis_bed" && charater != "Elphis_surprised")
        {
            Elphis[4].SetActive(true);
        }


        if (charater == "pilia_nomal")  // 요정
        {
            Plila[0].SetActive(true);
        }
        if (charater == "pilia_awe")
        {
            Plila[1].SetActive(true);
        }
        if (charater == "pilia_Curiosity")
        {
            Plila[2].SetActive(true);
        }
        if (charater == "pilia_Angry")
        {
            Plila[3].SetActive(true);
        }
        if (charater == "pilia_happy")
        {
            Plila[4].SetActive(true);
        }
        if (charater == "pilia_Sad")
        {
            Plila[5].SetActive(true);
        }
        if (charater == "pilia_Panic")
        {
            Plila[6].SetActive(true);
        }


        if (charater == "Demos_nomal")  // 앙마
        {
            Demos[0].SetActive(true);
        }
        if (charater == "Demos_happy")
        {
            Demos[1].SetActive(true);
        }
        if (charater == "Demos_Sad")
        {
            Demos[2].SetActive(true);
        }


        if (charater == "Gerffa_nomal") // 주민
        {
            Gerffa[0].SetActive(true);
        }
        if (charater == "Gerffa_nomal")
        {
            Gerffa[1].SetActive(true);
        }
        if (charater == "Gerffa_nomal")
        {
            Gerffa[2].SetActive(true);
        }
        if (charater == "Gerffa_nomal")
        {
            Gerffa[3].SetActive(true);
        }
    }
}
