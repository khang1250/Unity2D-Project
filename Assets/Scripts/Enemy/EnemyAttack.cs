using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage;
    //protected PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stats"))
        {
            PlayerStats.instance = collision.GetComponent<PlayerStats>();
            PlayerStats.instance.TakeDamage(damage);

            SpeacialAttack();   
        }
    }

    public virtual void SpeacialAttack()
    {

    }
}
