using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextevel : MonoBehaviour
{
    public int lvlToLoad;
    public Animator anim;
    public Fader fader;

    //private int nextSceneToLoad;

    //private void LoadLevel()
    //{
    //    SceneManager.LoadScene(lvlToLoad);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        LoadLevel();
    //        anim.SetTrigger("FadeIN");
    //    }
    //}


    //private List<string> sceneHistory = new List<string>();  //running history of scenes
    //                                                         //The last string in the list is always the current scene running

    //void Start()
    //{
    //    DontDestroyOnLoad(this.gameObject);  //Allow this object to persist between scene changes
    //}

    ////Call this whenever you want to load a new scene
    ////It will add the new scene to the sceneHistory list
    //public void LoadScene(string lvlToLoad)
    //{
    //    sceneHistory.Add(lvlToLoad);
    //    SceneManager.LoadScene(lvlToLoad);
    //}

    ////Call this whenever you want to load the previous scene
    ////It will remove the current scene from the history and then load the new last scene in the history
    ////It will return false if we have not moved between scenes enough to have stored a previous scene in the history
    //public bool PreviousScene()
    //{
    //    bool returnValue = false;
    //    if (sceneHistory.Count >= 2)  //Checking that we have actually switched scenes enough to go back to a previous scene
    //    {
    //        returnValue = true;
    //        sceneHistory.RemoveAt(sceneHistory.Count - 1);
    //        SceneManager.LoadScene(sceneHistory[sceneHistory.Count - 1]);
    //    }

    //    return returnValue;
    //}

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
  