using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{

    public GameObject GameStart_Ui;
    public Image Fade;
 
    float fadeCount = 1.0f; //�ʱ�ȭ ���İ� �ִ�


    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("��ưŬ��");
            StartCoroutine(FadeCoroutine()); // -3000��ƾ
        }
   
    }
    IEnumerator FadeCoroutine() // -3000
    {
        
        //Debug.Log("�ڷ�ƾ �ߵ�");
        while (fadeCount > 0.0f) //���İ� ���������� ����
        {
            fadeCount -= 0.005f; 
            yield return new WaitForSeconds(0.01f); //0.01�ʸ��� ����
            Fade.color = new Color(0, 0, 0, fadeCount); //���İ� ������ �϶�
            if (fadeCount < 0.0f)
            {
                GameStart_Ui.gameObject.SetActive(false);
            }
        }
    }
}
