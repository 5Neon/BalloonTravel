using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move : MonoBehaviour
{

    public GameObject targetPosition;

    public void npc_move()
    {
        StartCoroutine(npc_Move());
        StartCoroutine(npc_Move_null());

    }

    IEnumerator npc_Move() //���ľߵ�
    {
        float speed = 5f;
        Vector3 vel = Vector3.zero;
        while (true)
        {
            yield return null; //���ľߵ�
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition.transform.position, ref vel, speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), 10f);
        }
    }

    IEnumerator npc_Move_null() //���ľ� ��
    {
        yield return new WaitForSecondsRealtime(15f);
        StopCoroutine(npc_Move());
        StopCoroutine(npc_Move_null());
        Debug.Log("�ڷ�ƾ ��ü ����");
        while (true)
        {
            Debug.Log("���ƺ��� �۵���...");
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, 238.0f, 0.0f), 10f);
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
