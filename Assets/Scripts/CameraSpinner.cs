using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpinner : MonoBehaviour
{
    public GameObject Player;
    public Transform target;

    [Header("Camera Settings")]
    public int RotateAngle = 45;
    public float MinimumFollowAltitude = -10;
    public float MaximumFollowAltitude = 30;


    void Update()
    {
        Vector3 PlayerPosition = Player.transform.position;

        transform.position = target.position.x * Vector3.right + target.position.z * Vector3.forward;


        // 카메라가 일정 수직 범위 내에서 이동할 수 있도록(플레이어가 떨어지고 하늘에서 내려오는 장면을 보여주기 위함)
        if (target.position.y > MaximumFollowAltitude)
        {
            transform.position = new Vector3(target.position.x, 30f, target.position.z);
        }

        if (target.position.y < -10)
        {
            transform.position = new Vector3(target.position.x, 30, target.position.z);
        }

        if (target.position.y >= MinimumFollowAltitude && target.position.y <= MaximumFollowAltitude)       // 캐릭터가 떨어질 때 일정 부분까지만 따라가게 만듬 -> 포지션을 위로 옮겨 하늘에서 (풍선을 타고) 다시 떨어지는 표현을 하기 위함
        {
            transform.position = target.position.y * Vector3.up + target.position.x * Vector3.right + target.position.z * Vector3.forward;
            transform.LookAt(PlayerPosition);
        }

        // 카메라 회전
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.RotateAround(PlayerPosition, Vector3.up, -RotateAngle);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.RotateAround(PlayerPosition, Vector3.up, RotateAngle);
        }
    }
}