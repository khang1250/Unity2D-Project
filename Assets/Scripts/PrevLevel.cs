using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevLevel : MonoBehaviour
{

    private int prevSceneToLoad;
    public Animator anim;

    void Start()
    {
        prevSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponentInChildren<PlayerStats>();
        PlayerPrefs.SetFloat("HealthKey", playerStats.health);

        PlayerCollectibles collectibles = collision.GetComponent<PlayerCollectibles>(); 
        PlayerPrefs.SetInt("SoulNumber", collectibles.soulNumber);

        SceneManager.LoadScene(prevSceneToLoad);
        anim.SetTrigger("FadeIN");
    }
}
