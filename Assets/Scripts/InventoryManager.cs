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
        itemList.Add(item);
        print(item.itemName + " has been add to inventory");
        print(itemList.Count);
    }


    public List<ItemManager> GetItemList()
    {
        return itemList;
    }
   
}
