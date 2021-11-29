using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] Sprite characterImage;

    public int maxHealth;
    public int health;
    

    public float direction;

    public bool canTakeDamage = true;
      
    private PlayerAttackControls pAC;

    private Animator anim;

    public Image healthUI;


    public ItemManager equipedWeapon, equipedArmor;
    

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
        health = PlayerPrefs.GetInt("Health", maxHealth);
        maxHealth = PlayerPrefs.GetInt("MaxHealt", maxHealth);
        MenuManager.instance.healthSlider.maxValue = maxHealth;
        MenuManager.instance.healthSlider.value = health;
       
    }

    private void Update()
    {
    }

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

                PlayerPrefs.SetInt("Health", maxHealth);


            }
      
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
            GameManager.ManagerRestartLevel();
            anim.SetBool("Death", true);
            
        }

    }

   

    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetFloat("HealthKey", health);
    }
    

    public void AddHP(int amountHpToAdd)
    {
        health += amountHpToAdd;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        MenuManager.instance.healthSlider.value = health;
    }
    public void AddMaxHP(int amountMaxHpToAdd)
    {
        maxHealth += amountMaxHpToAdd;
        MenuManager.instance.healthSlider.value = health;

    }
}
