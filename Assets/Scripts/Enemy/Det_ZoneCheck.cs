using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Det_ZoneCheck : MonoBehaviour
{
    private Enemy_Behavior enemyParent;
    private bool inRange;
    private Animator anim;
    

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behavior>();
        anim = GetComponentInParent<Animator>();
        
    }

    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemyParent.Flip();
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
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }
}
