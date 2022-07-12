using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_Player : MonoBehaviour
{
    public GameObject targetEnInfo;
    public bool lookingstop;

    public float looking_Speed = 2f;
    public void OnTriggerStay(Collider other)
    {
        if (targetEnInfo != null && !lookingstop)
        {
            Vector3 dir = targetEnInfo.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * looking_Speed);
        }
    }
}
