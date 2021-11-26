using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    public float moveSpeed;
    public float damage;
    public GameObject[] wayPoint;

    int nextWaypoint = 1;
    float distToPoint;


    void Update()
    {
        Move();
    }

    void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoint[nextWaypoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoint[nextWaypoint].transform.position, moveSpeed * Time.deltaTime);

        if (distToPoint < 0.2f)
        {
            TakeTurn();
        }
    }

    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoint[nextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWayPoint();
    }

    void ChooseNextWayPoint()
    {
        nextWaypoint++;
        if (nextWaypoint == wayPoint.Length)
        {
            nextWaypoint = 0;
        }
    }

    public override void HurtSequence()
    {
        anim.SetTrigger("Hurt");
    }

    public override void DeathSequence()
    {
        moveSpeed = 0;
        anim.SetTrigger("Death");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        GetComponent<Enemy_Slime>().enabled = false;
        GetComponentInChildren<SlimeAttack>().enabled = false;
        //PlayerStats.instance.currentEXP += 100;
    }
}
