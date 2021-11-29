using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    public static PlayerCollectibles instance;
    public Text textComponent;  
    public int soulNumber;
    
    void Start()
    {
        instance = this;
        soulNumber = PlayerPrefs.GetInt("Soul", 0);   
        UpdateText();     
    }

    private void UpdateText()
    {
        textComponent.text = soulNumber.ToString();    
    }

    public void soulCollected()
    {
        soulNumber += 200;
        UpdateText();
    }
   
}
