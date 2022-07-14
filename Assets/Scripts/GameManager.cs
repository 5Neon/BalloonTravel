using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isTalking = false;   // NPC와 대화중인지 확인할 때 사용
    public static bool isGround = true;     
    public static bool onAir = false;       // 섬 밑으로 떨어지는 상태, 공중에 있는지 확인(isGround, CoundCheck보다 더 긴 거리를 측정)
    public static bool onBalloon = false;   // 풍선에 매달려 있을 때


    // 어느 섬에 있는지 
    public static Island state;

    public enum Island
    {
        Air,
        FirstWorld,
        Puzzle_Maze,
        Puzzle_Jump,
        Story,
        Ending
    }


    private void Start()
    {

    }

    void Update()
    {
        Debug.Log(state);
    }
}
