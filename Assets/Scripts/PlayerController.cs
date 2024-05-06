using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Vector3 startGamePosition;
    Quaternion startGameRotation;
    //Vector3 targetPos;
    float laneOffset = 2.5f;
    public float laneChangeSpeed = 15;
    //float timeElapsed;
    //float lerpDuration = 0.5f;
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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startGamePosition = transform.position;
        startGameRotation = transform.rotation;
        //targetPos = transform.position;
        SwipeManager.instance.MoveEvent += MovePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && pointFinish > -laneOffset)
        {
            MoveHorizontal(-laneChangeSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D) && pointFinish < laneOffset)
        {
            MoveHorizontal(laneChangeSpeed);
        }

        if(Input.GetKeyDown(KeyCode.W) && isJumping == false)
        {
            Jump();
        }
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

    //private void FixedUpdate()
    //{
    //    //transform.position = Vector3.MoveTowards(transform.position, targetPos, laneChangeSpeed * Time.deltaTime);
    //    //if(timeElapsed < lerpDuration) {
    //    //    transform.position = Vector3.MoveTowards(transform.position, targetPos, timeElapsed / lerpDuration);
    //    //    timeElapsed += Time.deltaTime;
    //    //}
    //    //else {
    //    //    transform.position = targetPos;
    //    //}
    //    rb.velocity = targetVelocity;
    //    if((transform.position.x > pointFinish && targetVelocity.x > 0) ||
    //        (transform.position.x < pointFinish && targetVelocity.x < 0))
    //    {
    //        targetVelocity = Vector3.zero;
    //        rb.velocity = targetVelocity;
    //        rb.position = new Vector3(pointFinish, rb.position.y, rb.position.z);
    //    }
    //}

    void MoveHorizontal(float speed) {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * laneOffset;
        if(isMoving) {
            StopCoroutine(movingCoroutine); 

        }
        movingCoroutine = StartCoroutine(MoveCoroutine(speed));


        //targetVelocity = new Vector3(-laneChangeSpeed, 0, 0);


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
        if (other.gameObject.tag == "Ramp")
        {
            //rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
        if(other.gameObject.tag == "Lose") {
            ResetGame();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Ramp")
    //    {
    //        //rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    //    }
    //}

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "NotLose") {
            MoveHorizontal(-lastVectorX);
        }
    }

    public void StartLevel()
    {
        RoadGenerator.instance.StartLevel();
    }

    public void ResetGame() {
        rb.velocity = Vector3.zero;
        pointStart = 0;
        pointFinish = 0;
        transform.position = startGamePosition;
        transform.rotation = startGameRotation;
        RoadGenerator.instance.ResetLevel();
    }
}
