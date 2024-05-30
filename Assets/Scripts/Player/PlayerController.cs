using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using WarriorAnimsFREE;

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
    public GameObject sword;
    public GameObject bullet;
    public GameObject fireBall;
    public float bulletSpeed = 3f;
    //private bool isClick = false;
    //private float clickTime = 0;
    //private float clickDelay = 0.5f;

    private float timeAttack;
    //private float startTimeAttack;
    public LayerMask Enemy;
    public LayerMask Bullet;
    private float attackRange = 0.5f;

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool useRootMotion = false;
    public float animationSpeed = 1.5f;
    //[HideInInspector] public WarriorInputSystemController warriorInputSystemController;
    //[HideInInspector] public WarriorController warriorController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SwipeManager.instance.MoveEvent += MovePlayer;
        StartCoroutine(AttackCoroutine());

    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>(); //вруби

        instance = this;
    }

    void Update()
    {
        SetAnimatorFloat("Animation Speed", animationSpeed);
        //if (warriorController)
        //{
        //    warriorController.LockMove(false);
        //    warriorController.SetAnimatorBool("Moving", true);
        //    warriorController.isMoving = true;
        //    warriorController.SetAnimatorFloat("Velocity", Vector3.forward.magnitude);
        //}
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


        //if(warriorInputSystemController.inputAttack)
        //Debug.Log(warriorInputSystemController.inputAttack);

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
            if (timeAttack <= 0 &&
                (transform.position.x == -laneOffset || transform.position.x == 0 || transform.position.x == laneOffset) &&
                (transform.position.y == 0.25f) || transform.position.y >= 2.5f && transform.position.y <= 3f)
            {
                if (enemies.Length > bullets.Length)
                {
                    if (enemies[0].gameObject.GetComponent<EnemyBehavior>() != null && enemies[0].gameObject.GetComponent<EnemyBehavior>().level <= PlayerParameters.archer.Damage ||
                        enemies[0].gameObject.GetComponent<EnemyFlyBehavior>() != null && enemies[0].gameObject.GetComponent<EnemyFlyBehavior>().level <= PlayerParameters.archer.Damage)
                    {
                        if (PlayerParameters.archer.ClassName == "Warrior")
                        {
                            CreateSword();
                        }
                        else
                        {
                            if (PlayerParameters.archer.ClassName == "Archer")
                            {
                                CreateBullet();
                            }
                            else
                            {
                                if (bullets.Length < 1)
                                {
                                    CreateFireBall();
                                }
                            }
                        }
                        //if(warriorInputSystemController)
                        //    warriorInputSystemController.inputAttack = true;

                    }

                }

                timeAttack = PlayerParameters.archer.Speed * 0.001f * 15 * 0.8f / Time.timeScale;
            }
            else
            {
                timeAttack -= Time.deltaTime;
            }
        }
    }

    private void CreateSword()
    {
        GameObject newSword = Instantiate(sword,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(0, -15, 0)) as GameObject;
        animator.SetInteger("Trigger Number", 2);
        animator.SetBool("Trigger", true);
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(bullet,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(90, 0, 0)) as GameObject;
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = this.transform.forward * bulletSpeed;
        animator.SetInteger("Trigger Number", 2);
        animator.SetBool("Trigger", true);
    }

    private void CreateFireBall()
    {
        GameObject newFireBall = Instantiate(fireBall,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(90, 0, 0)) as GameObject;
        Rigidbody fireBallRB = newFireBall.GetComponent<Rigidbody>();
        fireBallRB.velocity = this.transform.forward * bulletSpeed;
        animator.SetInteger("Trigger Number", 2);
        animator.SetBool("Trigger", true);
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
        //if(warriorInputSystemController)
        //warriorInputSystemController.inputJump = true;
        isJumping = true;
        animator.SetInteger("Jumping", 1);
        animator.SetInteger("Trigger Number", 1);
        animator.SetBool("Trigger", true);
        //Debug.Log("Here " + animator.GetInteger("Jumping"));
        //Debug.Log("Here " + animator.GetInteger("Trigger Number"));
        //Debug.Log("Here " + animator.GetBool("Trigger"));
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());


    }

    IEnumerator StopJumpCoroutine()
    {

        do {
            if (rb.velocity.y > 0 && animator.GetInteger("Jumping") != 1)
            {
                animator.SetInteger("Jumping", 1);
                //Debug.Log("Here " + animator.GetInteger("Jumping"));
                //Debug.Log("Here " + animator.GetInteger("Trigger Number"));
                //Debug.Log("Here " + animator.GetBool("Trigger"));
            }
            yield return new WaitForSeconds(0.02f);
            if (rb.velocity.y < 0 && animator.GetInteger("Jumping") != 2) {
                animator.SetBool("Trigger", true);
                animator.SetInteger("Jumping", 2);
                //Debug.Log("Here " + animator.GetInteger("Jumping"));
                //Debug.Log("Here " + animator.GetInteger("Trigger Number"));
                //Debug.Log("Here " + animator.GetBool("Trigger"));
            }
        } while (rb.velocity.y != 0);
        animator.SetBool("Trigger", true);
        animator.SetInteger("Jumping", 0);
        //Debug.Log("Here " + animator.GetInteger("Jumping"));
        //Debug.Log("Here " + animator.GetInteger("Trigger Number"));
        //Debug.Log("Here " + animator.GetBool("Trigger"));
        //animator.SetInteger("Trigger Number", 1);
        //animator.SetBool("Trigger", true);
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

    public void ClearSettings() {
        rb.velocity = Vector3.zero;
        pointStart = 0;
        pointFinish = 0;
        transform.position = startGamePosition;
        LevelWorld.levelEnemy = 1;
    }

    public void ResetGame()
    {
        ClearSettings();
        RoadGenerator.instance.ResetLevel();
    }

    //public void SetAnimatorInt(string name, int i)
    //{
    //    animator.SetInteger(name, i);
    //}

    //public void SetAnimatorTrigger(AnimatorTrigger trigger)
    //{
    //    animator.SetInteger("Trigger Number", (int)trigger);
    //    animator.SetTrigger("Trigger");
    //}

    //public void SetAnimatorBool(string name, bool b)
    //{
    //    animator.SetBool(name, b);
    //}

    //public void SetAnimatorRootMotion(bool b)
    //{
    //    useRootMotion = b;
    //}

    public void SetAnimatorFloat(string name, float f)
    {
        if (animator)
            animator.SetFloat(name, f);
    }
}
