using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public int level = 1;
    public int currentExp = 0;
    public int baseExp = 100;
    public int expToLevel = 100;
    public float expIncrease = 1.7f;
    public int totalExp;

    //public Slider expBar;
    //public Text currentExpDisplay;
    public Image expUI;

    private GatherInput gI;

    public static ExperienceController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gI = GetComponent<GatherInput>();
        UpdateExpUI();
        StartCoroutine(FrequentUpdateExp());
        currentExp = PlayerPrefs.GetInt("Exp", totalExp);
        totalExp = PlayerPrefs.GetInt("MaxExp", totalExp);
    }

    // Update is called once per frame
    void Update()
    {
        if (gI.LvlUpInput)
        {
            IncreaseExp(1);
        }

    }

    public void IncreaseExp(int exp)
    {
        currentExp += exp;
        totalExp += exp;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateExpUI();
        
    }

    void LevelUp()
    {
        currentExp -= expToLevel;
        level++;
        PlayerAttack.instance.attackDamage += 50;
        PlayerStats.instance.maxHealth += 50;
        PlayerStats.instance.health = PlayerStats.instance.maxHealth;
        expToLevel = (int)(baseExp * (Mathf.Pow(expIncrease, level)));
        UpdateExpUI();
        MenuManager.instance.healthSlider.maxValue = PlayerStats.instance.maxHealth;
        MenuManager.instance.healthSlider.value = PlayerStats.instance.health;
    }

    float CalculateExpPercentage()
    {
        return (float)currentExp / expToLevel;
    }

    IEnumerator FrequentUpdateExp()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            UpdateExpUI();
        }
    }

    void UpdateExpUI()
    {
        float fillAmount = (float)currentExp / (float)expToLevel;
        expUI.fillAmount = fillAmount;
    }

    // Whenever enabled, restart
    private void OnEnable()
    {
        StartCoroutine(FrequentUpdateExp());
    }
}
