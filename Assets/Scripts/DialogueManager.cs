using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject Text_Ui; //��ȭâ
    public Text text; //ä��
    public Text CharacterName; //ĳ���� �̸�
    public bool isAction; //�̰� ���� 
    public GameObject event_Chapters; // ���� ���ſ�

    public int Event_Chapters1_1;
                
    int Dialog_Content = 0;
    int Dialog_Name = 0;

    public TextAsset txt;
    public int lineSize;
    public int rowSize;

    void Start() 
    {
        
    }
    public void Event_1_1() 
    {  
        {
            Text_Ui.SetActive(true);
            text.text = "�׽�Ʈ";
            CharacterName.text = "���ΰ� �̸�";
            Time.timeScale = 0f;
            StartCoroutine(EventText());
        }
    }

    IEnumerator EventText()
    {   
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");         
        Event_Chapters1_1 = 0;
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
                
                if (Dialog_Content == 7)
                {
                    Dialog_Content = 0;
                    Dialog_Name = 0;
                    Time.timeScale = 1f;
                    Text_Ui.SetActive(false);
                    event_Chapters.SetActive(false);

                }
   
            }
        }

    }


    void Update()
    {

    }


}
