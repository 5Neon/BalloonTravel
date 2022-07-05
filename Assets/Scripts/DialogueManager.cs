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
    public GameObject Butten; // ��Ȱ��ȭ ������Ʈ


    int wrong_butten = 0; //�߸��� ��ư ī����
    int Dialog_Content = 0; 
    int Dialog_Name = 0;

    public TextAsset txt;
    public int lineSize;
    public int rowSize;

    public GameObject Npc1;
    public GameObject Npc1_1;
    public GameObject moveNPC;

    public void Event_1_1() 
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");

            Dialog_Content = 29;
            Dialog_Name = 29;

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
                
                if (Dialog_Content == 38)
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
        text.text = "�� ���󼼰� \n ���� ��";
        CharacterName.text = "ǥ����";
    }

    public void Event_1_Sing_Out()
    {
        Text_Ui.SetActive(false);
    }

    public void NPC1_1()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");
        Text_Ui.SetActive(true);
        text.text = data_Dialog[59]["Content"].ToString();
        CharacterName.text = data_Dialog[59]["Name"].ToString();
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

                if (Dialog_Content == 58)
                { 
                    Dialog_Content = 0;
                    Dialog_Name = 0;
                    Time.timeScale = 1f;
                    Text_Ui.SetActive(false);
                    Npc1.SetActive(false);
                    Npc1_1.SetActive(true);
                    yield break;
                }
            }
        }
    }

    public void Wrong_Butten()
    {
        wrong_butten++;
        if (wrong_butten == 1)
        {
            List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");
            Time.timeScale = 0f;
            Dialog_Content = 61;
            Dialog_Name = 61;           
            text.text = data_Dialog[Dialog_Content]["Content"].ToString();
            CharacterName.text = data_Dialog[Dialog_Name]["Name"].ToString();
            Text_Ui.SetActive(true);
            StartCoroutine(worng_Butten());
        }
    }

    IEnumerator worng_Butten()
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
                if (Dialog_Name == 63)
                {
                    Time.timeScale = 1f;
                    Text_Ui.SetActive(false);
                    wrong_butten = 0;
                    yield break;
                }

            }
        }
    }

    public void Right_Butten()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");
        Time.timeScale = 0f;
        Dialog_Content = 64;
        Dialog_Name = 64;
        text.text = data_Dialog[Dialog_Content]["Content"].ToString();
        CharacterName.text = data_Dialog[Dialog_Name]["Name"].ToString();
        Text_Ui.SetActive(true);
        StartCoroutine(right_Butten());
    }

    IEnumerator right_Butten()
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
                if (Dialog_Name == 76)
                {
                    Time.timeScale = 1f;
                    Text_Ui.SetActive(false);
                    Butten.GetComponent<ButtenTrigger>().enabled = false;
                    moveNPC.GetComponent<NPC_Move>().npc_move();
                    yield break;
                }

            }
        }
    }

    void Update()
    {

    }


}
