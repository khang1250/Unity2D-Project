using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    private GatherInput gI;

    public Slider healthSlider;

    public static MenuManager instance;

    private PlayerStats playerStats;
    private ExperienceController expControl;
    public TextMeshProUGUI hpText, expText, totalText, strenghtText;
    public Image characterImage;
    public GameObject characterPanel;

    public TextMeshProUGUI statHp, statExp, statTotalExp, statStrenght;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;

    public TextMeshProUGUI itemName, itemDescription;

    public ItemManager activeItem;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gI = GetComponent<GatherInput>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
    

            }
            else
            {
                menu.SetActive(true);
            }
        }
    }
    
    //public void UpdateStats()
    //{
    //    playerStats = GameManager.instance.GetPlayerStats();
    //    expControl = GameManager.instance.GetExperienceControllers();


    //    characterPanel.SetActive(true);
    //    hpText.text = "Hp: " + playerStats.health.ToString() + "/" + playerStats.maxHealth.ToString();
    //    expText.text = "Exp: " + expControl.currentExp + "/" + expControl.expToLevel;
    //    totalText.text = "Total: " + expControl.totalExp;
    //    strenghtText.text = "Strenght: " + PlayerAttack.instance.attackDamage;
    //    Debug.Log(hpText.text = "Hp: " + PlayerStats.instance.health + "/" + PlayerStats.instance.maxHealth);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        
    }

    public void UpdateItemsInventory()
    {
        foreach(Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }
        foreach(ItemManager item in InventoryManager.instance.GetItemList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("item image").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            Text itemsAmountText = itemSlot.Find("AmountText").GetComponent<Text>();
            if (item.amount > 0)
                itemsAmountText.text = item.amount.ToString();
            else
                itemsAmountText.text = " ";

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void DiscardItem()
    {
        InventoryManager.instance.RemoveItem(activeItem);
        UpdateItemsInventory();
    }

    public void UseItem()
    {
        activeItem.UseItem();
        DiscardItem();
    }
}
