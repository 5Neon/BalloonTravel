using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject Text_Ui; //��ȭâ
    public Text text; //ä��
    public Text CharacterName; //ĳ���� �̸�
    public GameObject event_Chapters; // ���� ���ſ�
                
    int Dialog_Content = 0;
    int Dialog_Name = 0;

    public TextAsset txt;
    public int lineSize;
    public int rowSize;

    public GameObject Npc1;
    public GameObject Npc1_1;

    public void Event_1_1() 
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");

            Dialog_Content = 28;
            Dialog_Name = 28;

            Text_Ui.SetActive(true);
            text.text = data_Dialog[Dialog_Content]["Content"].ToString();
            CharacterName.text = data_Dialog[Dialog_Name]["Name"].ToString();
            Dialog_Content++;
            Dialog_Name++;
            Time.timeScale = 0f;
            StartCoroutine(EventText());
        
    }

    IEnumerator EventText()
    {   
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");         
        //Debug.Log("�ڷ�ƾ ���� �κ�");
        while (true)
        { 
            yield return null;
            if (Input.GetKeyDown(KeyCode.Z))
            {


                Dialog_Content++;
                Dialog_Name++;

                text.text = data_Dialog[Dialog_Content]["Content"].ToString();
                CharacterName.text = data_Dialog[Dialog_Name]["Name"].ToString();
                
                if (Dialog_Content == 37)
                {
                    Dialog_Content = 0;
                    Dialog_Name = 0;
                    Time.timeScale = 1f;
                    Text_Ui.SetActive(false);
                    event_Chapters.SetActive(false);
                    yield break;
                }
   
            }
        }

    }
    public void Event_1_Sing()
    {
        Text_Ui.SetActive(true);
        text.text = "�� �ΰ� \n ���� ��";
        CharacterName.text = "ǥ����";
    }

    public void Event_1_Sing_Out()
    {
        Text_Ui.SetActive(false);
    }

    public void NPC1_1()
    {
        Text_Ui.SetActive(true);
        text.text = "������ ��Ծ���? \n'���� ������ ������?'";
        CharacterName.text = "�ֹ�1";
    }

    public void NPC1()
    {

        Time.timeScale = 0f;

        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");

        Dialog_Content = 39;
        Dialog_Name = 39;

        Text_Ui.SetActive(true);
        text.text = data_Dialog[Dialog_Content]["Content"].ToString();
        CharacterName.text = data_Dialog[Dialog_Name]["Name"].ToString();
        Dialog_Content++;
        Dialog_Name++;
        StartCoroutine(npc1());
    }

    IEnumerator npc1()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Dialog_Content++;
                Dialog_Name++;

                text.text = data_Dialog[Dialog_Content]["Content"].ToString();
                CharacterName.text = data_Dialog[Dialog_Name]["Name"].ToString();

                if (Dialog_Content == 55)
                {

                    Dialog_Content = 0;
                    Dialog_Name = 0;
                    Time.timeScale = 1f;
                    Text_Ui.SetActive(false);
                    Npc1.SetActive(false);
                    Npc1_1.SetActive(true);

                }


            }
        }
    }





    void Update()
    {

    }


}
