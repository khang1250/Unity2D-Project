using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    private GatherInput gI;

    public Slider healthSlider;

    public static MenuManager instance;

    private PlayerStats playerStats;
    private ExperienceController expControl;
    [SerializeField] TextMeshProUGUI hpText, expText, totalText, equipedWeaponText, equipedArmorText, weaponPowerText, ArmorDefenceText;
    public Image characterImage;
    public GameObject characterPanel;

    [SerializeField] TextMeshProUGUI statHp, statExp, statTotalExp, statStrenght;
    [SerializeField] GameObject statsButton;

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
                UpdateStats();

            }
            else
            {
                
                menu.SetActive(true);
            }
        }
    }

    public void UpdateStats()
    {
        
    }

    public void StatMenuUpdate()
    {
        hpText.text = playerStats.health.ToString() + "/" + playerStats.maxHealth;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   

    public void UpdateItemsInventory()
    {
        foreach(Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }
        foreach (ItemManager item in InventoryManager.instance.GetItemList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("item image").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            Text itemsAmountText = itemSlot.Find("AmountText").GetComponent<Text>();
            if (item.amount > 1)
            {
                itemsAmountText.text = item.amount.ToString();
                Debug.Log(itemsAmountText.text = item.amount.ToString());
            }
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
