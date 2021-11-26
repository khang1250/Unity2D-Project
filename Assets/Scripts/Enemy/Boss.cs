using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health;

    protected Rigidbody2D rb;
    protected Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HurtSequence();
        if(health <= 50)
        {
            TransformSequence();
        }

        if (health <= 0)
        {
            //PlayerStats.instance.currentEXP += 500;
            DeathSequence();
        }
    }

    public virtual void HurtSequence()
    {

    }
    public virtual void TransformSequence()
    {
       

    }
    public virtual void DeathSequence()
    {

    }

}
