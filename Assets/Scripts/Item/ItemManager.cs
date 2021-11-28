using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType { Item, Weapon, Armor}
    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueInSoul;
    public Sprite itemImage;

    public enum AffectType { Hp, Mana}
    public int amountOfAffect;
    public AffectType affectType;

    public int weaponStrenght;
    public int armorVitality;

    public bool isStackable;
    public int amount;


    public void UseItem()
    {
        //PlayerStats knight = GameManager.instance.GetPlayerStats();
        if(itemType == ItemType.Item)
        {
            if(affectType == AffectType.Hp)
            {
                PlayerStats.instance.AddHP(amountOfAffect);
            }
        }
        //else if (itemType == ItemType.Weapon)
        //{
        //    InventoryManager.instance.AddItem(PlayerStats.instance.equipedWeapon);
        //    PlayerStats.instance.EquipWeapon(this);
        //}
        //else if (itemType == ItemType.Armor)
        //{
        //    InventoryManager.instance.AddItem(PlayerStats.instance.equipedArmor);
        //    PlayerStats.instance.EquipArmor(this);
        //}
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(this);
            SelfDestroy();
            Debug.Log("add thanh cong");
        }
    }
    public void SelfDestroy()
    {
        gameObject.SetActive(false);
        
    }
}
