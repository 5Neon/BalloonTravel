using System.Collections;
using UnityEngine;

public class NPC_Move : MonoBehaviour
{

    public GameObject megas; // 스크립트 찾기
    public GameObject target_looking;
    public GameObject targetPosition;

    public int looking_speed = 1;
    public float Stoplooking_time = 9f;

    public bool looking;

    public float move_speed = 4;

    public void FixedUpdate()
    {
        if (looking)
        {
            megas.GetComponent<Look_Player>().lookingstop = true;
            target_Looking();
        }
        
    }



    public void target_Looking()
    {
        Vector3 dir = target_looking.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * looking_speed);
    }

    public void npc_move()
    {
        StartCoroutine(Times());
    }
    IEnumerator Times()
    {
        StartCoroutine(npc_Move());
        yield return new WaitForSeconds(Stoplooking_time);
        StopCoroutine(npc_Move());
        looking = false;
        megas.GetComponent<Look_Player>().lookingstop = false;
    }
    IEnumerator npc_Move()
    {
        Vector3 vel = Vector3.zero;
        while (true)
        {
            yield return null;
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition.transform.position, ref vel, move_speed);
        }
    }

    void Update()
    {

    }


}
