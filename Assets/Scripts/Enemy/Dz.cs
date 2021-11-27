using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dz : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.instance.health -= 1000000;
        }
    }
}
