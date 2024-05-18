using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Vector3 startGamePosition = new Vector3(0, 0.25f, -6f);
    //Quaternion startGameRotation;
    float laneOffset = 2.5f;
    public float laneChangeSpeed = 15;
    Rigidbody rb;
    Vector3 targetVelocity;
    float pointStart;
    float pointFinish;
    bool isMoving = false;
    Coroutine movingCoroutine;
    float lastVectorX;
    bool isJumping = false;
    float jumpPower = 15;
    float jumpGravity = -40;
    float realGravity = -10;
    public GameObject bullet;
    public GameObject sword;
    public float bulletSpeed = 3f;
    //private bool isClick = false;
    //private float clickTime = 0;
    //private float clickDelay = 0.5f;

    private float timeAttack;
    //private float startTimeAttack;
    public LayerMask Enemy;
    public LayerMask Bullet;
    private float attackRange = 0.5f;


    void Start()
    {
        //startTimeAttack = PlayerParameters.speedAttack / 2;
        rb = GetComponent<Rigidbody>();
       // startGameRotation = transform.rotation;
        //targetPos = transform.position;
        SwipeManager.instance.MoveEvent += MovePlayer;
        StartCoroutine(AttackCoroutine());
    }

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && pointFinish > -laneOffset)
        {
            MoveHorizontal(-laneChangeSpeed);
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && pointFinish < laneOffset)
        {
            MoveHorizontal(laneChangeSpeed);
        }

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isJumping == false)
        {
            Jump();
        }
        if (transform.position.y < 0)
            transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);

        ////if (EventSystem.current.IsPointerOverGameObject()) return;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CreateBullet();
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (!isClick)
        //    {
        //        isClick = true;
        //        clickTime = Time.time;
        //    }
        //    else
        //    {
        //        if (Time.time - clickTime < clickDelay)
        //        {
        //            CreateBullet();
        //            Debug.Log("Double click!");
        //        }
        //        isClick = false;
        //    }
        //}

    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.0001f);
            //yield return new WaitForSeconds(PlayerParameters.archer.Speed / 60 * 100 * 15);
            //Debug.Log("second" + PlayerParameters.archer.Speed);
            Collider[] enemies = Physics.OverlapCapsule(transform.position + new Vector3(0, 0, 1.5f),
                                                transform.position + new Vector3(0, 0, + laneOffset * PlayerParameters.archer.Distance),
                                                attackRange, Enemy);
            Collider[] bullets = Physics.OverlapCapsule(transform.position + new Vector3(0, 0, 1.5f),
                                                transform.position + new Vector3(0, 0, + laneOffset * PlayerParameters.archer.Distance),
                                                           attackRange, Bullet);
            if (
                timeAttack <= 0 &&
                        (transform.position.x == -laneOffset || transform.position.x == 0 || transform.position.x == laneOffset)
                        && (transform.position.y == 0.25f) ||
                        transform.position.y >= 2.5f && transform.position.y <= 3f
                        //transform.position.y == (0.25f + laneOffset)
                        )
            {
                //if(enemies.Length > 0)
                //    if(enemies[0].gameObject.GetComponent<EnemyBehavior>() != null)
                //Debug.Log(enemies[0].gameObject.GetComponent<EnemyBehavior>().Level);
                if (enemies.Length > bullets.Length)
                {
                    if (enemies[0].gameObject.GetComponent<EnemyBehavior>() != null && enemies[0].gameObject.GetComponent<EnemyBehavior>().Level <= PlayerParameters.archer.Level ||
                        enemies[0].gameObject.GetComponent<EnemyFlyBehavior>() != null && enemies[0].gameObject.GetComponent<EnemyFlyBehavior>().level <= PlayerParameters.archer.Level)
                    {

                    if (PlayerParameters.archer.ClassName != "Warrior")
                        CreateBullet();
                    else
                        CreateSword();
                    }
                }

                timeAttack = PlayerParameters.archer.Speed * 0.001f * 15 * 0.8f / Time.timeScale;
            }
            else
            {
                //timeAttack -= 0.0001f;
                timeAttack -= Time.deltaTime;
                // Debug.Log(Time.deltaTime);
            }
        }
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(bullet,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(90, 0, 0)) as GameObject;
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = this.transform.forward * bulletSpeed;
    }

    private void CreateSword()
    {
        GameObject newSword = Instantiate(sword,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(0, -15, 0)) as GameObject;
        //Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        //bulletRB.velocity = this.transform.forward * bulletSpeed;
    }


    void MovePlayer(bool[] swipes)
    {
        if (swipes[(int)SwipeManager.Direction.Left] && pointFinish > -laneOffset)
        {
            MoveHorizontal(-laneChangeSpeed);
        }
        if (swipes[(int)SwipeManager.Direction.Right] && pointFinish < laneOffset)
        {
            MoveHorizontal(laneChangeSpeed);
        }

        if (swipes[(int)SwipeManager.Direction.Up] && isJumping == false)
        {
            Jump();
        }
    }

    void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());
    }

    IEnumerator StopJumpCoroutine()
    {
        do {
            yield return new WaitForSeconds(0.02f);
        } while (rb.velocity.y != 0);
        isJumping = false;
        Physics.gravity = new Vector3(0, realGravity, 0);
    }

    void MoveHorizontal(float speed) {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * laneOffset;
        if(isMoving) {
            StopCoroutine(movingCoroutine); 

        }
        movingCoroutine = StartCoroutine(MoveCoroutine(speed));
    }

    IEnumerator MoveCoroutine(float vectorX) {
        isMoving = true;
        while(Mathf.Abs(pointStart - transform.position.x) < laneOffset) {
            yield return new WaitForFixedUpdate();

            rb.velocity = new Vector3(vectorX, rb.velocity.y, 0);
            lastVectorX = vectorX;
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Ramp")
        //{
        //    //rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        //}


        //if (other.gameObject.tag == "Lose")
        //{
        //    PlayerParameters.Health -= 1;
        //}
        //if (PlayerParameters.Health == 0) {
        //    ResetGame();
        //}

    }

    //private void OnCollisionEnter(Collision collision) {
    //    if(collision.gameObject.tag == "NotLose") {
    //        MoveHorizontal(-lastVectorX);
    //    }
    //}

    public void StartLevel()
    {
        RoadGenerator.instance.StartLevel();
    }

    private void ClearSettings() {
        rb.velocity = Vector3.zero;
        pointStart = 0;
        pointFinish = 0;
        PlayerParameters.Score = 0;
        transform.position = startGamePosition;
        LevelWorld.levelEnemy = 1;
    }

    public void ResetGame() {
        ClearSettings();
        RoadGenerator.instance.ResetLevel();
    }

    public void RestartGame()
    {
        ClearSettings();
        RoadGenerator.instance.RestartLevel();
    }
}
