using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] Text currentSoulsText;

    public List<ItemManager> itemForSale;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotBuyContainerParent;
    [SerializeField] Transform itemSlotSellContainerParent;


    [SerializeField] ItemManager selectedItem;
    [SerializeField] TextMeshProUGUI buyItemName, buyItemDescription;
    [SerializeField] Text buyItemValue;
    [SerializeField] TextMeshProUGUI sellItemName, sellItemDescription;
    [SerializeField] Text sellItemValue;


    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);

        currentSoulsText.text = "Souls: " + PlayerCollectibles.instance.soulNumber;
        buyPanel.SetActive(true);
    }
    public void CloseShop()
    {
        shopMenu.SetActive(false);

    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);

        UpdateItemInShop(itemSlotBuyContainerParent, itemForSale); 
    }

    public void OpenSellPanel()
    {
        sellPanel.SetActive(true);
        buyPanel.SetActive(false);

        UpdateItemInShop(itemSlotSellContainerParent, InventoryManager.instance.GetItemList());
    }

    private void UpdateItemInShop(Transform itemSlotContainerParent, List<ItemManager> itemToLookFor)
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }
        foreach (ItemManager item in itemToLookFor)
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("item image").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            Text itemsAmountText = itemSlot.Find("AmountText").GetComponent<Text>();
            if (item.amount > 0)
            {
                itemsAmountText.text = "";
            }
            else
                itemsAmountText.text = "";

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void SelectedBuyItem(ItemManager itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.valueInSoul;

    }


    public void SelectedSellItem(ItemManager itemToSell)
    {
        selectedItem = itemToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value: " + (int)(selectedItem.valueInSoul * 0.75f);
    }

    public void BuyItem()
    {
        if(PlayerCollectibles.instance.soulNumber >= selectedItem.valueInSoul)
        {
            PlayerCollectibles.instance.soulNumber -= selectedItem.valueInSoul;
            InventoryManager.instance.AddItem(selectedItem);

            currentSoulsText.text = "Souls: " + PlayerCollectibles.instance.soulNumber;
            
        }    
    }

    public void SellItem()
    {
        if (selectedItem)
        {
            PlayerCollectibles.instance.soulNumber += (int)(selectedItem.valueInSoul * 0.75f);
            InventoryManager.instance.RemoveItem(selectedItem);

            currentSoulsText.text = "Souls: " + PlayerCollectibles.instance.soulNumber;
            selectedItem = null;

            OpenSellPanel();
        }    

    }

}
