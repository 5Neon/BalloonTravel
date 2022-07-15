using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Active : MonoBehaviour
{
    public GameObject button_active;
    public GameObject button_active2;
    public GameObject button_active3;
    public GameObject button_active4;

    public void OnTriggerEnter(Collider other)
    {
        button_active.GetComponent<Text_Button>().Button_Actvie = true;
        button_active2.GetComponent<Text_Button>().Button_Actvie = true;
        button_active3.GetComponent<Text_Button>().Button_Actvie = true;
        button_active4.GetComponent<Text_Button>().Button_Actvie = true;
    }
}
