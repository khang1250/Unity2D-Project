using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextevel : MonoBehaviour
{
    private int nextSceneToLoad;
    public Animator anim;

    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponentInChildren<PlayerStats>();
        PlayerPrefs.SetInt("HealthKey", playerStats.health);
        PlayerPrefs.SetInt("MaxHealthKey", playerStats.maxHealth);

        ExperienceController expControl = collision.GetComponentInChildren<ExperienceController>();
        PlayerPrefs.SetInt("ExpKey", expControl.currentExp);
        PlayerPrefs.SetInt("MaxExpKey", expControl.totalExp);
        PlayerPrefs.SetInt("Level", expControl.level);

        PlayerCollectibles collectibles = collision.GetComponentInChildren<PlayerCollectibles>();
        PlayerPrefs.SetInt("SoulNumber", collectibles.soulNumber);

        SceneManager.LoadScene(nextSceneToLoad);
        anim.SetTrigger("FadeIN");
    }
}
  