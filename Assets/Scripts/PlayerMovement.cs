using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    // 기본 플레이어 세팅(카메라, 속도, 제한 등)
    [Header("Player Settings")]
    public Camera PlayerCamera;
    public float movementSpeed = 4f;
    public float onBalloonSpeed = 0.5f;
    public float rotateSpeed = 10f;
    public float redeployLimitAltitude = -10f;
    public float deployAltitude = 40f;
    public Transform startPoint;

    public Transform jumpMapCenter;
    public GameObject Look;

    private Vector3 vel;

    [HideInInspector] public Vector3 rightMovement;
    [HideInInspector] public Vector3 upMovement;
    [Space(10)]

    Vector3 horizontalMovement;
    Vector3 verticalMovement;

    // 플레이어 점프 세팅(힘, 딜레이)
    [Header("Jump Setting")]
    public float jumpForce = 5f;
    private bool readyToJump;
    public float jumpWaitTime = 1.2f;
    [Space(10)]

    Rigidbody rb;

    [Header("Falling Setting")]
    public float fallSpeed = 3.5f;
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
    [Space(10)]

    // 점프맵 전용
    [Header("Jump Map Settings")]
    private float timeCounter = 0;
    public float jumpMapSpeed = 0.4f;
    public float jumpMap_jumpForce = 8f;
    [HideInInspector]
    public float jumpMapRadiusSize = 15f;

    [HideInInspector]
    Vector3 deployPoint;

    // 열기구용
    //[Header("AirBalloon")]
    //public Transform AirBalloonTarget;
    //RaycastHit hit;


    private void Start()
    {
        //transform.position = startPoint.position;     // 시작할 때 지정된 위치로 이동
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
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && isGrounded && GameManager.isTalking == false && GameManager.state != GameManager.Island.Puzzle_Maze)  // 미로 맵에서만 점프 제한
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
        GroundCheck();
        OnAirCheck();
        IslandCheck();

        // 플레이어 입력 및 이동 애니메이션
        if (GameManager.state != GameManager.Island.Puzzle_Jump || !GameManager.doJumpMap)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ||
                 Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && !GameManager.isTalking)
            {
                Move();
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }

        if(GameManager.state == GameManager.Island.Puzzle_Jump && GameManager.doJumpMap)
        {
            animator.SetBool("isMoving", false);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                timeCounter -= jumpMapSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.rotation.x, Look.transform.rotation.eulerAngles.y - 90f, transform.rotation.z);
                Move();
                animator.SetBool("isMoving", true);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S))
            {
                timeCounter += jumpMapSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.rotation.x, Look.transform.rotation.eulerAngles.y + 90f, transform.rotation.z);
                Move();
                animator.SetBool("isMoving", true);
            }
        }

        RedeployPlayer();
    }


    void GroundCheck()
    {
        // 땅 위에 있는지 확인
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        //Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

        if (!isGrounded)
        {
            GameManager.isGround = false;
            animator.SetBool("isGround", false);
        }
        else
        {
            GameManager.isGround = true;
            GameManager.onBalloon = false;

            animator.SetBool("isJumping", false);
            animator.SetBool("isGround", true);
        }
    }

    void OnAirCheck()
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

    void IslandCheck()
    {
        RaycastHit Islandcheck;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out Islandcheck, Mathf.Infinity))
        {
            if (Islandcheck.collider.tag == "Island")
            {
                // 떨어지는 위치
                deployPoint = Islandcheck.collider.transform.Find("RedeployPoint").gameObject.transform.position;
                //Debug.Log(deployPoint);

                switch (Islandcheck.collider.name)
                {
                    default:
                        GameManager.state = GameManager.Island.Air;
                        break;
                    case "FirstIsland":
                    case "FirstWorld_port":
                        GameManager.state = GameManager.Island.FirstWorld;
                        break;
                    case "Puzzle_Maze_Island":
                        GameManager.state = GameManager.Island.Puzzle_Maze;
                        break;
                    case "Puzzle_Jump_Island":
                        GameManager.state = GameManager.Island.Puzzle_Jump;
                        break;
                    case "Story_Island-1":
                    case "Story_Island-2":
                        GameManager.state = GameManager.Island.Story;
                        break;
                    case "Ending_Island":
                        GameManager.state = GameManager.Island.Ending;
                        break;
                }
            }
            else
            {
                GameManager.state = GameManager.Island.Air;
            }

            if (Islandcheck.collider.name == "rainbow")
            {
                GameManager.state = GameManager.Island.Ending;
            }
        }
    }

    void Move()
    {
        // 카메라를 기준으로 상하좌우 움직임
        horizontalMovement = Camera.main.transform.forward;
        horizontalMovement.y = 0;
        horizontalMovement = Vector3.Normalize(horizontalMovement);
        verticalMovement = Quaternion.Euler(new Vector3(0, 90, 0)) * horizontalMovement;

        if ((GameManager.state != GameManager.Island.Puzzle_Jump || !GameManager.doJumpMap ) && !GameManager.isTalking)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            rightMovement = verticalMovement * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            upMovement = horizontalMovement * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;

            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0;

            if (rb.drag != 0)
            {
                rb.MovePosition(rb.position + direction * onBalloonSpeed * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + direction * movementSpeed * Time.deltaTime);
            }
        }

        // 점프맵용 
        if (GameManager.state == GameManager.Island.Puzzle_Jump && GameManager.doJumpMap)
        {
            float posx = jumpMapCenter.position.x + Mathf.Cos(timeCounter) * jumpMapRadiusSize;
            float posy = transform.position.y;
            float posz = jumpMapCenter.position.z + Mathf.Sin(timeCounter) * jumpMapRadiusSize;

            // 캐릭터 이동(점프맵)
            transform.position = new Vector3(posx, posy, posz);     // 리지드바디를 활용할 수 있도록 수정을 해야함
            //rb.MovePosition(rb.position + new Vector3(posx, posy, posz) * Time.deltaTime);
            //rb.AddForce(transform.forward * jumpMapSpeed, ForceMode.Force);
        }


        // 공중에 있을 때 이동속도 감소

        //transform.position += upMovement;
        //transform.position += rightMovement;

    }

    void Jump()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isGround", false);

        if (GameManager.doJumpMap)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpMap_jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void RedeployPlayer()
    {
        // 섬 아래로 떨어졌을 때
        if (transform.position.y < redeployLimitAltitude)
        {
            transform.position = new Vector3(deployPoint.x, deployAltitude, deployPoint.z);

            animator.SetBool("isHanging", true);
            GameManager.onBalloon = true;

            InstantiateBalloon();

            // 낙하속도
            if (GameManager.onAir == true)
            {
                rb.drag = fallSpeed;
            }
        }
    }

    void InstantiateBalloon()
    {
        clone = Instantiate(Balloons, BalloonAttachPosition.transform.position, Balloons.transform.rotation * Quaternion.Euler(Balloons.transform.rotation.x, Balloons.transform.rotation.y, Random.Range(-90f, 90f)));
        clone.transform.SetParent(BalloonAttachPosition.transform);
    }


    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(jumpWaitTime);

        readyToJump = true;
    }

    IEnumerator DestroyBalloon()
    {
        clone.transform.parent = null;
        Destroy(clone, 10f);

        yield return null;
    }
}
