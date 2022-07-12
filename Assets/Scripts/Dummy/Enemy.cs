using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    public float AngleOfField;
    public float ViewDistance;
    public LayerMask targetMask;

    public Transform target;
    public NavMeshAgent _enemy;

    [Header("전투 관련 값")]
    public float HP;
    public float Damage;

    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        View();
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    private void View()
    {
        //Vector3 _leftBoundary = BoundaryAngle(-AngleOfField * 0.5f);
        //Vector3 _rightBoundary = BoundaryAngle(AngleOfField * 0.5f);

        // 시야각 표시
        // Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        // Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);


        Vector3 _direction = (target.position - transform.position).normalized;
        float _angle = Vector3.Angle(_direction, transform.forward);

        if (_angle < AngleOfField * 0.5f)
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, ViewDistance) && _hit.transform.name == "Player")
            {
                _enemy.SetDestination(target.position);
                Debug.Log("플레이어가 시야 내에 있습니다.");

                //플레이어의 방향 표시
                // Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);
            }
            else
            {
                _enemy.ResetPath();
                Debug.Log("대기 중");
            }
        }
    }
}
