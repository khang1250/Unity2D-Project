using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpenShop;
    private GatherInput gI;



    [SerializeField] List<ItemManager> shopItemsForSale;
    // Start is called before the first frame update
    void Start()
    {
        gI = GetComponent<GatherInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpenShop && Input.GetKeyDown("a")
            && !ShopManager.instance.shopMenu.activeInHierarchy)
        {
            ShopManager.instance.itemForSale = shopItemsForSale;
            ShopManager.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            canOpenShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpenShop = false;
        }
    }
}
