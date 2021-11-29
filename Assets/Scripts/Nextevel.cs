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
        PlayerPrefs.SetInt("Health", playerStats.health);
        PlayerPrefs.SetInt("MaxHealt", playerStats.maxHealth);

        PlayerAttack playerAttack = collision.GetComponentInChildren<PlayerAttack>();
        PlayerPrefs.SetInt("Attack", playerAttack.attackDamage);

        ExperienceController expControl = collision.GetComponentInChildren<ExperienceController>();
        PlayerPrefs.SetInt("Exp", expControl.currentExp);
        PlayerPrefs.SetInt("MaxExp", expControl.totalExp);
        PlayerPrefs.SetInt("Lvl", expControl.level);

        PlayerCollectibles collectibles = collision.GetComponentInChildren<PlayerCollectibles>();
        PlayerPrefs.SetInt("Soul", collectibles.soulNumber);

        SceneManager.LoadScene(nextSceneToLoad);
        anim.SetTrigger("FadeIN");
    }
}
  