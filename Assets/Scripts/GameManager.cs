using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isJumped = false;    // 사용하지 않는 변수인것 같음 (검토 후 삭제할 예정)
    public static bool isTalking = false;
    public static bool isGround = true;
    public static bool onAir = false;   // 섬 밑으로 떨어지는 상태, 공중에 있는지 확인

    private void Start()
    {

    }

    void Update()
    {

    }
}
