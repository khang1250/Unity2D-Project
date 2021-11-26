using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private List<ItemManager> itemList;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        itemList = new List<ItemManager>();
        
    }

    public void AddItem(ItemManager item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach(ItemManager itemInventory in itemList)
            {
                if(itemInventory.itemName == item.itemName)
                {
                    itemInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
    }

    public void RemoveItem(ItemManager item)
    {
        if (item.isStackable)
        {
            ItemManager inventoryItem = null;

            foreach(ItemManager itemInInventory in itemList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if(inventoryItem != null && inventoryItem.amount <= 0)
            {
                itemList.Remove(inventoryItem);
            }
        }
        else
        {
            itemList.Remove(item);  
        }
    }


    public List<ItemManager> GetItemList()
    {
        return itemList;
    }
   
}
