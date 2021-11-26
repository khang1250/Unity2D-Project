using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerArea : MonoBehaviour
{
    private Boss_behavior bossParent;


    private void Awake()
    {
        bossParent = GetComponentInParent<Boss_behavior>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            bossParent.target = collider.transform;
            bossParent.inRange = true;
            bossParent.detectZone.SetActive(true);
        }
    }
}
