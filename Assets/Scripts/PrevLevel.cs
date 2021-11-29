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
        PlayerPrefs.SetInt("Health", playerStats.health);
        PlayerPrefs.SetInt("MaxHeal", playerStats.maxHealth);

        PlayerAttack playerAttack = collision.GetComponentInChildren<PlayerAttack>();
        PlayerPrefs.SetInt("Attack", playerAttack.attackDamage);

        ExperienceController expControl = collision.GetComponentInChildren<ExperienceController>();
        PlayerPrefs.SetInt("ExpKey", expControl.currentExp);
        PlayerPrefs.SetInt("MaxExp", expControl.totalExp);
        PlayerPrefs.SetInt("Lvl", expControl.level);


        PlayerCollectibles collectibles = collision.GetComponent<PlayerCollectibles>();
        PlayerPrefs.SetInt("Soul", collectibles.soulNumber);

        SceneManager.LoadScene(prevSceneToLoad);
        anim.SetTrigger("FadeIN");
    }
}
