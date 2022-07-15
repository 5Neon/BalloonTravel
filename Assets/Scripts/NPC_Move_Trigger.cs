using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move_Trigger : MonoBehaviour
{
    public GameObject Move_npc;
    public void OnTriggerExit(Collider other)
    {
        Move_npc.GetComponent<NPC_Move>().npc_move();
        Move_npc.GetComponent<NPC_Move>().looking = true;
    }

}
