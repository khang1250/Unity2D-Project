using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private Enemy_Behavior enemyParent;
    
    
    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_Behavior>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true; 
            enemyParent.detectZone.SetActive(true);
        }
    }
}
