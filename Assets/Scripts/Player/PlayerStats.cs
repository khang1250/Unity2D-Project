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
    
    //[SerializeField] int strenght;
    //[SerializeField] int vitality;
    //[SerializeField] int defence;

    public float direction;

    public bool canTakeDamage = true;
      
    private PlayerAttackControls pAC;

    private Animator anim;

    public Image healthUI;

    public string equippedWeaponName;
    public string equippedArmorName;

    public int weaponPower;
    public int armorVitality;

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
    }

    public void EquipWeapon(ItemManager weaponToEquip)
    {
        equipedWeapon = weaponToEquip;
        weaponPower = equipedWeapon.weaponStrenght;

    }

    public void EquipArmor(ItemManager armorToEquip)
    {
        equipedArmor = armorToEquip;
        armorVitality = equipedWeapon.armorVitality;

    }
}
