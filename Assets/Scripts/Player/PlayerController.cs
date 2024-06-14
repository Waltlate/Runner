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
    float laneOffset = 2.5f;
    public float laneChangeSpeed = 15;
    private BoxCollider bc;
    [HideInInspector]
    public Rigidbody rb;
    Vector3 targetVelocity;
    float pointStart;
    float pointFinish;
    bool isMoving = false;
    Coroutine movingCoroutine;
    Coroutine jumpCoroutine;
    Coroutine rollCoroutine;
    float lastVectorX;
    bool isRoll = false;
    bool isJumping = false;
    float jumpPower = 15;
    float jumpGravity = -40;
    float realGravity = -10;
    public GameObject sword;
    public GameObject bullet;
    public GameObject fireBall;
    public float bulletSpeed = 3f;

    private float timeAttack;
    public LayerMask Enemy;
    public LayerMask Bullet;
    private float attackRange = 0.5f;

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool useRootMotion = false;
    public float animationSpeed = 1.5f;
    private AudioSource audio;
    public float speedAudio = 2;
    public AudioClip soundRun;
    public AudioClip soundDamage;
    public AudioClip soundBlock;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        SwipeManager.instance.MoveEvent += MovePlayer;


    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>(); //вруби

        instance = this;
        audio = GetComponent<AudioSource>();
        audio.pitch = speedAudio;
        StartCoroutine(AttackCoroutine());
    }

    void Update()
    {
        SetAnimatorFloat("Animation Speed", animationSpeed);
        if (RoadGenerator.instance.isPlaying)
        {
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && pointFinish > -laneOffset)
            {
                MoveHorizontal(-laneChangeSpeed);
            }
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && pointFinish < laneOffset)
            {
                MoveHorizontal(laneChangeSpeed);
            }
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isJumping == false)
            {
                Jump();
            }
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && isRoll == false)
            {
                Roll();
            }
            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        }
    }

    void MovePlayer(bool[] swipes)
    {
        if (RoadGenerator.instance.isPlaying)
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
            if (swipes[(int)SwipeManager.Direction.Down] && isRoll == false)
            {
                Roll();
            }
        }
    }

    void MoveHorizontal(float speed)
    {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * laneOffset;
        if (isMoving)
        {
            StopCoroutine(movingCoroutine);
        }
        movingCoroutine = StartCoroutine(MoveCoroutine(speed));
    }

    IEnumerator MoveCoroutine(float vectorX)
    {
        isMoving = true;
        while (Mathf.Abs(pointStart - transform.position.x) < laneOffset)
        {
            //yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.0001f);
            rb.velocity = new Vector3(vectorX, rb.velocity.y, 0);
            lastVectorX = vectorX;
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);
        if (transform.position.y > 1)
        {
            rb.velocity = new Vector3(rb.velocity.x, realGravity, rb.velocity.z);
        }
        isMoving = false;
    }

    void Jump()
    {
        isJumping = true;
        if (isRoll)
        {
            StopCoroutine(rollCoroutine);
            animator.SetBool("Trigger", true);
            animator.SetInteger("Jumping", 0);
            bc.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            isRoll = false;
        }
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        animator.SetInteger("Jumping", 1);
        animator.SetInteger("Trigger Number", 1);
        animator.SetBool("Trigger", true);
        jumpCoroutine = StartCoroutine(StopJumpCoroutine());
    }

    IEnumerator StopJumpCoroutine()
    {
        do
        {
            if (rb.velocity.y > 0 && animator.GetInteger("Jumping") != 1)
            {
                animator.SetInteger("Jumping", 1);
            }
            yield return new WaitForSeconds(0.02f);
            if (rb.velocity.y < 0 && animator.GetInteger("Jumping") != 2)
            {
                animator.SetBool("Trigger", true);
                animator.SetInteger("Jumping", 2);
            }
        } while (rb.velocity.y != 0);
        animator.SetBool("Trigger", true);
        animator.SetInteger("Jumping", 0);
        isJumping = false;

        Physics.gravity = new Vector3(0, realGravity, 0);
    }

    void Roll()
    {
        isRoll = true;
        if (isJumping)
        {
            StopCoroutine(jumpCoroutine);
            animator.SetBool("Trigger", true);
            animator.SetInteger("Jumping", 0);
            isJumping = false;
            Physics.gravity = new Vector3(0, realGravity, 0);
        }
        bc.transform.localScale = new Vector3(0.5f, 0.35f, 0.5f);
        animator.SetInteger("Jumping", 1);
        animator.SetInteger("Trigger Number", 1);
        animator.SetBool("Trigger", true);
        rollCoroutine = StartCoroutine(StopRollCoroutine());
    }

    IEnumerator StopRollCoroutine()
    {
        animator.SetInteger("Jumping", 1);
        animator.SetBool("Trigger", true);
        animator.SetInteger("Jumping", 2);
        yield return new WaitForSeconds(0.75f);
        animator.SetBool("Trigger", true);
        animator.SetInteger("Jumping", 0);
        bc.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        isRoll = false;
    }

    public IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.0001f);
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

    public void ClearSettings() {
        rb.velocity = Vector3.zero;
        pointStart = 0;
        pointFinish = 0;
        transform.position = startGamePosition;
        LevelWorld.levelEnemy = 1; // Why?
    }
    public void ResetGame()
    {
        ClearSettings();
        RoadGenerator.instance.ResetLevel();
    }

    public void SetAnimatorFloat(string name, float f)
    {
        if (animator)
            animator.SetFloat(name, f);
    }

    public void AudioStop() {
        if(audio)
            audio.Stop();
    }

    public void AudioPlay()
    {
        if (audio)
            audio.Play();
    }

    public void SoundDamage()
    {
        StartCoroutine(SoundCoroutine(soundDamage));
    }

    public void SoundBlock()
    {
        StartCoroutine(SoundCoroutine(soundBlock));
    }

    //public void SoundDamageStop()
    //{
    //    StopCoroutine(SoundDamageCoroutine());
    //}
    IEnumerator SoundCoroutine(AudioClip clip)
    {
        audio.pitch = 1f;
        audio.clip = clip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.pitch = speedAudio;
        audio.clip = soundRun;
        audio.Play();
        if (PlayerParameters.health == PlayerParameters.maxHealth * PlayerParameters.archer.Level && RoadGenerator.instance.speed == 0)
        { 
            AudioStop();
        }
    }
}
