using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextevel : MonoBehaviour
{
    public int lvlToLoad;
    public Animator anim;
    public Fader fader;



    private void Start()
    {
        lvlToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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

        //fader.SetLevel(lvlToLoad);
        //SceneManager.LoadScene(lvlToLoad);
        GameManager.ManagerLoadLevel(lvlToLoad);   
    }
}
  