using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public float waitForKey = 2f;

    public GameObject anyKeyText;

    public string mainMenuScene;

    private void Update()
    {
        if(waitForKey > 0)
        {
            waitForKey -= Time.deltaTime;
            if(waitForKey <= 0)
            {
                anyKeyText.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}
