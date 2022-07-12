using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtenTrigger : MonoBehaviour
{

    public GameObject dialoguemanager;

    string player = "Player";

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == player)
        {
            StartCoroutine(buttenManager());
        }
    }
    IEnumerator buttenManager()
    {
        bool Buttens = true;
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialog");
        float i = 0.001f;
        float temp = 0f;
        player = null; //다시 실행 방어

        Vector3 down = new Vector3(0, -0.001f, 0);

        while (Buttens)
        {
            yield return null;
            transform.position += down;
            temp += i;

            if (temp > 0.2f)
            {
                dialoguemanager.GetComponent<DialogueManager>().Wrong_Butten();
                Buttens = false;
            }
        }
    }
}
