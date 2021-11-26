using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    public Text textComponent;  
    public int soulNumber;
    
    void Start()
    {
        soulNumber = PlayerPrefs.GetInt("SoulNumber", 0);   
        UpdateText();     
    }

    private void UpdateText()
    {
        textComponent.text = soulNumber.ToString();    
    }

    public void soulCollected()
    {
        soulNumber += 1;
        UpdateText();
    }
   
}
