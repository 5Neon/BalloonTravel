using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isJumped = false;
    public static bool isTalking = false;

    public Text text; // ��ȭâ ����
    public Text CharacterName; // �ι� �̸�

    public GameObject Text_Ui; // ��ȭâ UI

    void Update()
    {

    }

    public IEnumerator Dialogue(int Content, int Name, int FinerContent) // ���̾�α� ��ȭ ��ũ��Ʈ
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");
        //Debug.Log("�ڷ�ƾ ���� �κ�");
        Text_Ui.SetActive(true);

        CharacterName.text = data_Dialog[Content]["Name"].ToString();
        StartCoroutine(Typing(text, data_Dialog[Name]["Content"].ToString(), 0.01f));

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
            {
                Content++;
                Name++;

                CharacterName.text = data_Dialog[Content]["Name"].ToString();
                StartCoroutine(Typing(text, data_Dialog[Name]["Content"].ToString(), 0.01f));

                if (Content == FinerContent +1 && Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
                {
                    Text_Ui.SetActive(false);
                    GameManager.isTalking = false;
                    yield break;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
        IEnumerator Typing(Text typingText, string message, float speed)
        {
            //Debug.Log("Ÿ���� �ڷ�ƾ");
            for (int i = 0; i < message.Length; i++)
            {
                typingText.text = message.Substring(0, i + 1);
                yield return new WaitForSeconds(speed);
            }
        }

    }
