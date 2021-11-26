using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //[SerializeField] int maxLevel = 50;
    //[SerializeField] int playerLevel = 1;
    //[SerializeField] int currentEXP;
    //[SerializeField] int[] expForNextLevel;
    //[SerializeField] int baseLevelExp = 100;
    [SerializeField] Sprite characterImage;

    public int maxHealth;
    public int health;
    public int currentExp;
    public int totalExp;
    //[SerializeField] int strenght;
    //[SerializeField] int vitality;
    //[SerializeField] int defence;

    public float direction;

    public bool canTakeDamage = true;
      
    private PlayerAttackControls pAC;

    private Animator anim;

    public Image healthUI;
    

    public static PlayerStats instance;

    private GatherInput gI;

    PlayerMoveControls player;
    CapsuleCollider2D col2D;

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        gI = GetComponent<GatherInput>();
        pAC = GetComponentInParent<PlayerAttackControls>();
        anim = GetComponentInParent<Animator>();
        //health = maxHealth;
        health = PlayerPrefs.GetInt("HealthKey", maxHealth);
        maxHealth = PlayerPrefs.GetInt("MaxHealthKey", maxHealth);
        MenuManager.instance.healthSlider.maxValue = maxHealth;
        MenuManager.instance.healthSlider.value = health;
        
 

        //expForNextLevel = new int[maxLevel];
        //expForNextLevel[1] = baseLevelExp;

        //for (int i = 2; i < expForNextLevel.Length; i++)
        //{
        //    //print("we are at" + i);
        //    expForNextLevel[i] = (int)(0.02f * i * i * i + 3.01f * i * i + 105.6f * i);
        //}
    }

    private void Update()
    {
        //if (gI.LvlUpInput)
        //{
        //    AddEXP(100);
        //}


    }

    //private void TestLvlUp()
    //{
    //    if (gI.LvlUpInput)
    //    {
    //        AddEXP(100);
    //    }
    //}

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= (int)damage;
            anim.SetBool("Damage", true);

            MenuManager.instance.healthSlider.value = health;

            pAC.ResetAttack();

            if (health <= 0)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                StartCoroutine(Shine());

                PlayerPrefs.SetFloat("HealthKey", maxHealth);
                GameManager.ManagerRestartLevel();

            }
                
            StartCoroutine(DamagePrevention());
        }  
    }   

    IEnumerator Shine()
    {
        yield return new WaitForSeconds(0.3f);
        col2D = GetComponentInParent<CapsuleCollider2D>();
        col2D.size = new Vector2(0.35f, 0.35f);
        
    }
    private IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if (health > 0)
        {
            canTakeDamage = true;
            anim.SetBool("Damage", false);
        }
        else
        {
            anim.SetBool("Death", true);
        }

    }

    //public void UpdateHealthUI()
    //{
    //    float fillAmount = (float)health / (float)maxHealth;
    //    healthUI.fillAmount = fillAmount;

    //}

    //public void AddEXP(int amountOfExpToAdd)
    //{
    //    currentEXP += amountOfExpToAdd;
    //    if (currentEXP > expForNextLevel[playerLevel])
    //    {
    //        currentEXP -= expForNextLevel[playerLevel];
    //        playerLevel++;

    //        if (playerLevel % 2 == 0)
    //        {
    //            strenght++;
    //        }
    //        else
    //        {
    //            defence++;
    //        }

    //        maxHealth = Mathf.FloorToInt(maxHealth * 1.06f);
    //        health = maxHealth;
    //    }
    //}

    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetFloat("HealthKey", health);
    }
}
