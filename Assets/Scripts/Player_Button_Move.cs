using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Button_Move : MonoBehaviour
{
    public void move()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        float i = 0.001f;
        float temp = 0f;

        Vector3 down = new Vector3(0, -0.001f, 0);
        while (true)
        {
            yield return null;
            transform.position -= down;
            temp += i;
            if (temp > 0.2f)
            {
                yield break;
            }
        }
    }
}
