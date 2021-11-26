using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetZone : MonoBehaviour
{
    private Boss_behavior bossParent;
    private bool inRange;
    private Animator anim;


    private void Awake()
    {
        bossParent = GetComponentInParent<Boss_behavior>();
        anim = GetComponentInParent<Animator>();

    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            bossParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            bossParent.triggerArea.SetActive(true);
            bossParent.inRange = false;
            bossParent.SelectTarget();
        }
    }
}
