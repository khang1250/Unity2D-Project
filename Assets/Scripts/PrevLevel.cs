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
        PlayerPrefs.SetInt("HealthKey", playerStats.health);
        PlayerPrefs.SetInt("MaxHealthKey", playerStats.maxHealth);

        ExperienceController expControl = collision.GetComponentInChildren<ExperienceController>();
        PlayerPrefs.SetInt("ExpKey", expControl.currentExp);
        PlayerPrefs.SetInt("MaxExpKey", expControl.totalExp);

        PlayerCollectibles collectibles = collision.GetComponent<PlayerCollectibles>();
        PlayerPrefs.SetInt("SoulNumber", collectibles.soulNumber);

        SceneManager.LoadScene(prevSceneToLoad);
        anim.SetTrigger("FadeIN");
    }
}
