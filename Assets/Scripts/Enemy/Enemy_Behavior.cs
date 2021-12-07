using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : Enemy
{
    #region public Variables
    public float attackDistance;
    public float moveSpeed;
    public float damage;
    public float timer; //attack reset
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //check if Player is in range or not
    public GameObject detectZone;
    public GameObject triggerArea;
    public AudioSource source;


    #endregion

    #region private Variables
    private float distance; //store distance b/w enemy & player
    private bool attackMode;
    private bool cooling; //check if the Enemy is cooling after attack
    private float intTimer;
    #endregion
    
    void Awake()
    {
        
        SelectTarget();
        intTimer = timer; //store the inital value of timer
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }
        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SelectTarget();
        }
        if (inRange)
        {
           
            EnemyLogic(); 
        }
    }
 
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canMove", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("canMove", false);
        anim.SetBool("Attack", true);
        source.Play();

    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
        source.Stop();
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private  bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public override void HurtSequence()
    {
        anim.SetTrigger("Hurt");
    }

    public override void DeathSequence()
    {
        
        anim.SetTrigger("Death");
        rb.gravityScale = 0;
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Enemy_Behavior>().enabled = false;
        GetComponentInChildren<Det_ZoneCheck>().enabled = false;
        GetComponentInChildren<TriggerAreaCheck>().enabled = false;
        rb.gravityScale = 0;
        ExperienceController.instance.IncreaseExp(50);
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        HurtSequence();
        if (health <= 0)
        {
            //PlayerStats.instance.currentEXP += 500;
            DeathSequence();
        }
    }
}
     