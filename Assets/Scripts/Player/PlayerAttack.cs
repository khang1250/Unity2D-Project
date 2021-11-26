using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage;
    public int enemyLayer;
    public int bossLayer;

    public static PlayerAttack instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        bossLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer && collision.gameObject.layer == bossLayer)
        {
            collision.GetComponent<Enemy>().TakeDamage(attackDamage);
            collision.GetComponent<Boss>().TakeDamage(attackDamage);
        }
    }
}
 