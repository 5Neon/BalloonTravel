using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    [Header("Player Settings")]
    public Camera PlayerCamera;
    public float speed = 5f;
    public float onBalloonSpeed = 0.5f;
    public float rotateSpeed = 10f;
    public Transform startPoint;

    private Vector3 vel;

    [HideInInspector] public Vector3 rightMovement;
    [HideInInspector] public Vector3 upMovement;
    [Space(10)]

    Vector3 horizontalMovement;
    Vector3 verticalMovement;

    [Header("Jump Setting")]
    public float jumpForce;
    private bool readyToJump;
    public float jumpWaitTime = 2f;
    [Space(10)]

    public Rigidbody rb;

    [Header("Falling Setting")]
    public float fallSpeed = 5f;
    [Space(10)]

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;
    bool isGrounded;
    [Space(10)]

    [Header("Balloon")]
    public GameObject Balloons;
    private GameObject clone;
    public Transform BalloonAttachPosition;
    //[Space(10)]

    //[Header("AirBalloon")]
    //public Transform AirBalloonTarget;
    //RaycastHit hit;     // 열기구용


    private void Start()        // 시작할 때 지정된 위치로 이동
    {
        transform.position = startPoint.position;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        readyToJump = true;
    }

    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && isGrounded && GameManager.isTalking == false)
        {
            readyToJump = false;
            StartCoroutine(JumpDelay());
            Jump();
        }

        // 열기구 탑승
        //if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 10))
        //{
        //    Debug.Log(hit.collider.name);
        //    //Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * hit.distance, Color.red);

        //    if (Input.GetKey(KeyCode.E) && hit.collider.name == "AirBalloon")
        //    {
        //        transform.position = AirBalloonTarget.position;
        //    }
        //}
    }

    void FixedUpdate()
    {
        // 땅 위에 있는지 확인
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        // Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.red);
        GroundCheck();

        // 카메라를 기준으로 상하좌우 움직임
        horizontalMovement = Camera.main.transform.forward;
        horizontalMovement.y = 0;
        horizontalMovement = Vector3.Normalize(horizontalMovement);
        verticalMovement = Quaternion.Euler(new Vector3(0, 90, 0)) * horizontalMovement;

        // 애니메이션 상태 업데이트
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && GameManager.isTalking == false)
        {
            Move();
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (!isGrounded)
        {
            GameManager.isGround = false;
            animator.SetBool("isGround", false);
        }
        else
        {
            GameManager.isJumped = false;
            GameManager.isGround = true;

            animator.SetBool("isJumping", false);
            animator.SetBool("isGround", true);
        }

        // 섬 아래로 떨어졌을 때
        if (transform.position.y < -10)
        {
            // 떨어지는 위치(맵 배치에 따라 스폰 장소는 달라질 수 있음) 
            Vector3 deployPoint = GameObject.FindWithTag("Island").transform.position;

            transform.position = new Vector3(deployPoint.x, 30f, deployPoint.z);

            animator.SetBool("isHanging", true);

            clone = Instantiate(Balloons, BalloonAttachPosition.transform.position, Balloons.transform.rotation * Quaternion.Euler(Balloons.transform.rotation.x, Balloons.transform.rotation.y, Random.Range(-180f, 180f)));
            clone.transform.SetParent(BalloonAttachPosition.transform);

            // StartCoroutine(CameraUp());

            // 낙하속도
            if (GameManager.onAir == true)
            {
                rb.drag = fallSpeed;
            }
        }
    }

    void GroundCheck()
    {
        RaycastHit check;

        if (!Physics.Raycast(transform.position, Vector3.down, out check, 5f))
        {
            GameManager.onAir = true;
            animator.SetBool("isFalling", true);
            animator.SetFloat("FallBlend", 1f);
        }
        else
        {
            GameManager.onAir = false;

            if (GameManager.isGround == true)
            {
                rb.drag = 0f;

                animator.SetBool("isFalling", false);
                animator.SetBool("isHanging", false);

                if (clone != null)
                {
                    StartCoroutine(DestroyBalloon());
                }
            }
            animator.SetFloat("FallBlend", 0f);
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // 공중에 있을 때 이동속도 감소
        //if (!GameManager.onAir && GameManager.isGround)
        //{
        rightMovement = verticalMovement * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        upMovement = horizontalMovement * speed * Time.deltaTime * Input.GetAxis("Vertical");
        //}
        //else
        //{
        //    rightMovement = verticalMovement * onBalloonSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        //    upMovement = horizontalMovement * onBalloonSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        //}

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }

    void Jump()
    {
        GameManager.isJumped = true;
        animator.SetBool("isJumping", true);
        animator.SetBool("isGround", false);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(jumpWaitTime);

        readyToJump = true;
    }

    IEnumerator DestroyBalloon()
    {
        clone.transform.parent = null;
        Destroy(clone, 5f);

        // StartCoroutine(CameraDown());
        yield return null;

    }

    //IEnumerator CameraUp()
    //{
    //    PlayerCamera.transform.position = Vector3.SmoothDamp(PlayerCamera.transform.position, PlayerCamera.transform.position + new Vector3(0, 1.5f, 0), ref vel, 0.2f);
    //    yield return null;
    //}

    //IEnumerator CameraDown()
    //{
    //    PlayerCamera.transform.position = Vector3.SmoothDamp(PlayerCamera.transform.position, PlayerCamera.transform.position - new Vector3(0, 1.5f, 0), ref vel, 0.2f);
    //    yield return null;
    //}
}
