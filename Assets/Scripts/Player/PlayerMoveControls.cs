using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveControls : MonoBehaviour
{
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

    public float slidePower;
    public float slideTime;
    bool isSliding = false;


    //public Transform ledgeCheck;
    //private bool isTouchingLedge;
    //private bool canClimbLedge = false;
    //private bool ledgeDetected;

    private float startGravity;

    //[Header("Dash Variables")]
    //[SerializeField] private float dashSpeed = 15f;
    //[SerializeField] private float dashLength = .3f;
    //[SerializeField] private float dashBufferLength = .1f;
    //private float dashBufferCounter;
    //private bool isDashing;
    //private bool hasDashed;
    //private bool canDashed => dashBufferCounter > 0f && !hasDashed;
    //private float _horizontalDirection;
    //private float _verticalDirection;




    void Start()    
    {
      //speed = baseSpeed;
      gI = GetComponent<GatherInput>();
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      startGravity = rb.gravityScale;

      resetJumpNumber = additionalJump;

    }

    void Update()
    {     
        SetAnimatorValues();
      
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

    //IEnumerator Slide()
    //{
    //    isSliding = true;
    //    speed *= slidePower;

    //    yield return new WaitForSeconds(slideTime);

    //    speed = baseSpeed;
    //    isSliding = false;
    //}

    //private void HandleRoll()
    //{
    //    anim.SetBool("isRolling", canRoll);

    //    if (facingRight)
    //    {
    //        direction = 2;
    //    }
    //    if (!facingRight)
    //    {
    //        direction = 1;
    //    }

    //    if (direction == 1 && Input.GetKeyDown(KeyCode.Mouse1) && isGrounded)
    //    {
    //        if (canImpulse && !canRoll)
    //        {
    //            canRoll = true;
    //            rb.AddForce(Vector2.left * rollImpulse);
    //        }
    //    }
    //    else if (direction == 2 && Input.GetKeyDown(KeyCode.Mouse1) && isGrounded)
    //    {
    //        if (canImpulse && !canRoll)
    //        {
    //            canRoll = true;
    //            rb.AddForce(Vector2.right * rollImpulse);
    //        }
    //    }
    //}

}
