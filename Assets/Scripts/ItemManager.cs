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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
