using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{



    public GameObject Text_Ui; //��ȭâ
    public Text text; //ä��
    public Text CharacterName; //ĳ���� �̸�
    public bool isAction; //�̰� ����
    int clickCount = 0;//Ŭ��Ƚ�� ī���� 


    void Start()
    {
        
    }

    public void TalkStart() 
    {
        Text_Ui.SetActive(true);
        clickCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickCount == 0)
            {
                text.text = "1�� �ؽ�Ʈ �Դϴ�";
                clickCount++;
            }

            else if (clickCount == 1)
            {
                text.text = "2�� �ؽ�Ʈ �Դϴ�";
                clickCount++;
            }

            else if (clickCount == 2)
            {
                text.text = "3�� �ؽ�Ʈ �Դϴ�";
                clickCount++;

            }
            else if (clickCount == 3)
            {
                text.text = "";
                Text_Ui.SetActive(false);
                clickCount = 0;
            }
        }



        
    }
}
