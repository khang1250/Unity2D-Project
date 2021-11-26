using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    public float moveSpeed;
    public float damage;
    public bool chase = false;
    public Transform startingPoint;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (chase == true)
            Chase();
        else
        {
            ReturnToStartPoint();
            Flip();
        }
            
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void ReturnToStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, moveSpeed * Time.deltaTime);
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > player.transform.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public override void DeathSequence()
    {
        anim.SetTrigger("Death");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<Enemy_Ghost>().enabled = false;
        //PlayerStats.instance.currentEXP += 100;
        
    }
}
