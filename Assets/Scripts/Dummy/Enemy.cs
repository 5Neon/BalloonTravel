using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent _enemy;
    private Animator anim;

    [Header("전투 관련 값")]
    public float Damage;

    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _enemy.destination = target.transform.position;
        anim.SetBool("Move", true);

        if(_enemy.remainingDistance < _enemy.stoppingDistance)
        {
            anim.SetBool("Move", false);
        }
    }

}
