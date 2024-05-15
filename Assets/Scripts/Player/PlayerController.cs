using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
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
    private bool isClick = false;
    private float clickTime = 0;
    private float clickDelay = 0.5f;

    private float timeAttack;
    private float startTimeAttack;
    public LayerMask Enemy;
    public LayerMask Bullet;
    private float attackRange = 0.5f;


    void Start()
    {
        startTimeAttack = PlayerParameters.speedAttack / 2;
        rb = GetComponent<Rigidbody>();
       // startGameRotation = transform.rotation;
        //targetPos = transform.position;
        SwipeManager.instance.MoveEvent += MovePlayer;
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

        Collider[] enemies = Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                                                        new Vector3(transform.position.x, transform.position.y, transform.position.z + laneOffset * PlayerParameters.distance),
                                                        attackRange, Enemy);
        Collider[] bullets = Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                                                       new Vector3(transform.position.x, transform.position.y, transform.position.z + laneOffset * PlayerParameters.distance),
                                                       attackRange, Bullet);



        if (timeAttack <= 0 &&
            (transform.position.x == -laneOffset || transform.position.x == 0 || transform.position.x == laneOffset) &&
            (transform.position.y == 0.25f) || transform.position.y == (0.25f + laneOffset))
        {
            if (enemies.Length != 0) Debug.Log("----");
            if (enemies.Length != 0) Debug.Log(enemies.Length);
            if (bullets.Length != 0) Debug.Log(bullets.Length);
            //for(int i = 0; i < enemies.Length; i++) {
            //    Debug.Log(enemies[i].transform.position);
            //    CreateBullet();
            //}

            //if(Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            //                                            new Vector3(transform.position.x, transform.position.y, transform.position.z + laneOffset * PlayerParameters.distance),
            //                                            attackRange, Enemy).Length > Physics.OverlapCapsule(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            //                                           new Vector3(transform.position.x, transform.position.y, transform.position.z + laneOffset * PlayerParameters.distance),
            //                                           attackRange, Bullet).Length) CreateBullet();

            if (enemies.Length > bullets.Length) {
                if (PlayerParameters.archer.ClassName != "Warrior")
                    CreateBullet();
                else
                    CreateSword();
            }

            timeAttack = startTimeAttack;
        }
        else {
            timeAttack -= Time.deltaTime * Time.timeScale;
           // Debug.Log(Time.deltaTime);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(new Vector3(0, 0, transform.position.z), attackRange);
    //    Gizmos.DrawSphere(new Vector3(0, 0, transform.position.z + 1), attackRange);
    //}

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
                            transform.position + new Vector3(0.25f, 0, 1),
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
        if (other.gameObject.tag == "Lose")
        {
            PlayerParameters.Health -= 1;
        }
        if (PlayerParameters.Health == 0) {
            ResetGame();
        }

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
