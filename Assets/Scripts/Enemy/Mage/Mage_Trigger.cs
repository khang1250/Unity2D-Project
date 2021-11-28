using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_Trigger : MonoBehaviour
{
    private Mage_Behavior enemyParent;


    private void Awake()
    {
        enemyParent = GetComponentInParent<Mage_Behavior>();
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
