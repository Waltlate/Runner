using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    Vector3 startGamePosition = new(0, 0.25f, -6f);
    readonly float laneOffset = 2.5f;
    readonly float laneChangeSpeed = 15;
    BoxCollider bc;
    [HideInInspector] private Rigidbody rb;
    float pointStart;
    float pointFinish;
    bool isMoving = false;
    Coroutine movingCoroutine;
    Coroutine jumpCoroutine;
    Coroutine rollCoroutine;
    bool isRoll = false;
    bool isJumping = false;
    float jumpPower = 15;
    float jumpGravity = -40;
    readonly float realGravity = -10;
    float lastVectorX;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float bulletSpeed = 3f;

    float timeAttack;
    [SerializeField] private LayerMask Enemy;
    [SerializeField] private LayerMask Bullet;
    readonly float attackRange = 0.5f;

    [HideInInspector] public Animator animator;
    [SerializeField] private float animationSpeed = 1.5f;
    AudioSource audioPlayer;
    [SerializeField] private float speedAudio = 2;
    [SerializeField] private AudioClip soundRun;
    [SerializeField] private AudioClip soundDamage;
    [SerializeField] private AudioClip soundBlock;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        SwipeManager.instance.MoveEvent += MovePlayer;


    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        instance = this;
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.pitch = speedAudio;
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

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.0001f);
            Collider[] enemies = Physics.OverlapCapsule(transform.position + new Vector3(0, 0, 1.5f),
                                                transform.position + new Vector3(0, 0, +laneOffset * PlayerParameters.archer.Distance),
                                                attackRange, Enemy);
            Collider[] bullets = Physics.OverlapCapsule(transform.position + new Vector3(0, 0, 1.5f),
                                                transform.position + new Vector3(0, 0, +laneOffset * PlayerParameters.archer.Distance),
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

    void CreateSword()
    {
        GameObject newSword = Instantiate(sword,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(0, -15, 0)) as GameObject;
        animator.SetInteger("Trigger Number", 2);
        animator.SetBool("Trigger", true);
    }

    void CreateBullet()
    {
        GameObject newBullet = Instantiate(bullet,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(90, 0, 0)) as GameObject;
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = this.transform.forward * bulletSpeed;
        animator.SetInteger("Trigger Number", 2);
        animator.SetBool("Trigger", true);
    }

    void CreateFireBall()
    {
        GameObject newFireBall = Instantiate(fireBall,
                            transform.position + new Vector3(0, 0, 1),
                            Quaternion.Euler(90, 0, 0)) as GameObject;
        Rigidbody fireBallRB = newFireBall.GetComponent<Rigidbody>();
        fireBallRB.velocity = this.transform.forward * bulletSpeed;
        animator.SetInteger("Trigger Number", 2);
        animator.SetBool("Trigger", true);
    }

    public void ClearSettings()
    {
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

    public void SetAnimatorFloat(string name, float f)
    {
        if (animator)
            animator.SetFloat(name, f);
    }

    public void AudioStop()
    {
        if (audioPlayer)
            audioPlayer.Stop();
    }

    public void AudioPlay()
    {
        if (audioPlayer)
            audioPlayer.Play();
    }

    public void SoundDamage()
    {
        StartCoroutine(SoundCoroutine(soundDamage));
    }

    public void SoundBlock()
    {
        StartCoroutine(SoundCoroutine(soundBlock));
    }

    IEnumerator SoundCoroutine(AudioClip clip)
    {
        audioPlayer.pitch = 1f;
        audioPlayer.clip = clip;
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        audioPlayer.pitch = speedAudio;
        audioPlayer.clip = soundRun;
        audioPlayer.Play();
        if (PlayerParameters.health == PlayerParameters.maxHealth * PlayerParameters.archer.Level && RoadGenerator.instance.speed == 0)
        {
            AudioStop();
        }
    }
}
