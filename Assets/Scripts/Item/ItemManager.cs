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

    public enum AffectType { Hp, Damage, Health}
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
        else if (itemType == ItemType.Weapon)
        {
            if (affectType == AffectType.Damage)
            {
                PlayerAttack.instance.AddDamage(amountOfAffect);
            }
        }
        else if (itemType == ItemType.Armor)
        {
            if (affectType == AffectType.Health)
            {
                PlayerStats.instance.AddMaxHP(amountOfAffect);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(this);
            SelfDestroy();
        }
    }
    public void SelfDestroy()
    {
        gameObject.SetActive(false);
        
    }
}
