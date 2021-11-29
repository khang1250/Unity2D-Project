using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerMoveControls : MonoBehaviour
{
    public static PlayerMoveControls instance;
    public float speed;
    //public float baseSpeed;
    public float JumpForce;

    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator anim;

    private int direction =1;
    //private bool doubleJump = false; 

    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool isGrounded = true;

    public bool knockBack = false;
    public bool hasControls = true;
    public int additionalJump = 1;
    private int resetJumpNumber;

    public bool onLadders;
    public float climbSpeed;
    public float climbHorizontalSpeed;

    public float baseSpeed;
    public float rollPower;
    public float rollTime;
    bool isRolling = false;
    public float distanceBetweenImages;
    public float rollCoolDown;
    private float lastImageXpos;
    private float lastRoll = -100;
    private float rollTimeLeft;



    private float startGravity;
    public string levelToReload;






    private void Awake()
    {
        instance = this;
    }

    void Start()    
    {

        speed = baseSpeed;
        gI = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startGravity = rb.gravityScale;

        resetJumpNumber = additionalJump;

    }

    void Update()
    {     
        SetAnimatorValues();
        CheckRoll();

        if (/*Input.GetKeyDown(KeyCode.Z)*/gI.slideInput)
        {
            if(Time.time >= (lastRoll + rollCoolDown))
            {
                if (!isRolling && !onLadders )
                {
                    StartCoroutine(Roll());
                }
            }   
        }
    }

    private void FixedUpdate()
    { 
        CheckStatus();
        if (knockBack)
        return;
        Move();
        JumpPlayer();
    }
 

    private void Move()
    {
      Flip();
      rb.velocity = new Vector2(speed * gI.valueX, rb.velocity.y);

      if (onLadders)
      {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(climbHorizontalSpeed * gI.valueX, climbSpeed*gI.valueY);
            if (rb.velocity.y == 0)
            {
                anim.enabled = false;

            }
            else
            {
                anim.enabled = true;
            }
                    
      }

    }
    public void ExitLadder()
    {
        rb.gravityScale = startGravity;
        onLadders = false;
        anim.enabled = true;
    }

    private void JumpPlayer()
    {
        if (gI.jumpInput)
        {
            if (isGrounded || onLadders)
            {
                ExitLadder();
                rb.velocity = new Vector2(gI.valueX * speed, JumpForce);
            }
            else if (additionalJump > 0)
            {
                ExitLadder();
                rb.velocity = new Vector2(gI.valueX * speed, JumpForce);
                additionalJump -= 1;
            }
        }
        gI.jumpInput = false;

    }
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        if (leftCheckHit || rightCheckHit)
        {
            isGrounded = true;
            additionalJump = resetJumpNumber;
        }
        else
        {
            isGrounded = false;
        }
        SeeRays(leftCheckHit,rightCheckHit);
    }

    private void SeeRays(RaycastHit2D leftCheckHit,RaycastHit2D rightCheckHit)
    {
        Color color1 = leftCheckHit ? Color.red : Color.green;
        Color color2 = rightCheckHit ? Color.red : Color.green;

        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
        Debug.DrawRay(rightPoint.position, Vector2.down * rayLength, color2);
    }

    private void Flip()
    {
        if (gI.valueX * direction < 0)
        {   
          transform.localScale = new Vector3 (-transform.localScale.x, 1, 1);
          direction *= -1;
        }
    }

    private void SetAnimatorValues()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Climb", onLadders);
    }

    public IEnumerator KnockBack(float forceX, float forceY, float duration, Transform otherObject)
    {
        int knockBackDirection;
        if (transform.position.x < otherObject.position.x)
            knockBackDirection = -1;
        else
            knockBackDirection = 1;

        knockBack = true;
        rb.velocity = Vector2.zero;
        Vector2 theForce = new Vector2(knockBackDirection * forceX, forceY);
        rb.AddForce(theForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        knockBack = false;
        rb.velocity = Vector2.zero;
       
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(levelToReload);
        Time.timeScale = 1;

    }

    IEnumerator Roll()
    {   
        isRolling = true;
        rollTimeLeft = rollTime;
        lastRoll =Time.time;
        AfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
        GetComponentInChildren<Rigidbody2D>().gravityScale = 0;
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;


        speed *= rollPower;

        yield return new WaitForSeconds(rollTime);

        speed = baseSpeed;

        isRolling = false;

        GetComponentInChildren<Rigidbody2D>().gravityScale = 6;
        GetComponentInChildren<CapsuleCollider2D>().enabled = true;
        GetComponentInChildren<PolygonCollider2D>().enabled = true;
        GetComponentInChildren<BoxCollider2D>().enabled = true;

    }

    private void CheckRoll()
    {
        if (isRolling)
        {
            if(rollTimeLeft > 0)
            {
                rb.velocity = new Vector2(rollPower * gI.valueX, rb.velocity.y);
                rollTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    AfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
        }
    }
}
