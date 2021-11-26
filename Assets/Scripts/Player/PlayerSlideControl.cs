using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSlideControl : MonoBehaviour
{
    //public float slideSpeed;
    //private float slideTime;
    //public float startSlideTime;
    //private int direction;

    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator anim;

    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;

    public Vector2 savedVelocity;

    void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                var isDashKeyDown = gI.slideInput;
                if (isDashKeyDown)
                {
                    savedVelocity = rb.velocity;
                    rb.velocity = new Vector2(rb.velocity.x * 3f, rb.velocity.y);
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    rb.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}