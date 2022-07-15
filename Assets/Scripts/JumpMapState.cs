using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMapState : MonoBehaviour
{
    Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        GameManager.doJumpMap = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.doJumpMap = false;
        _collider.isTrigger = false;    // 점프맵을 끝낸 후 재입장할 수 없도록 하기 위함
    }
}
