using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject item1, item1Text, item2, item2Text, item3, item3Text;
    public int itemPrice, itemstock, selectedItem, item1index, item2index, item3index;
    public GameObject stocktext, salvageText, BuyButton, selectedItemText, Alienade;
    public GameObject[] damageItemPrefabs;
    public GameObject[] utilityItemPrefabs;
    public GameObject[] survivalItemPrefabs;

    public PlayerStats player;

    public Shop shop;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerStats>();
            assignShopItems();
        }

        if (shop == null)
        {
            shop = FindAnyObjectByType<Shop>();
        }

        stocktext.GetComponent<Text>().text = "Item Stock: " + itemstock;
        salvageText.GetComponent<Text>().text = "Salvage: " + player.salvage;
        if(selectedItem != 0)
        {
            if(selectedItem == 1)
            {
                selectedItemText.GetComponent<Text>().text = item1.GetComponent<Image>().sprite.name + ": (" + itemPrice + " Salvage)";
                if(itemstock > 0)
                {
                    BuyButton.SetActive(true);

                }
                else
                {
                    BuyButton.SetActive(false);
                }
            }
            if(selectedItem == 2)
            {
                selectedItemText.GetComponent<Text>().text = item2.GetComponent<Image>().sprite.name + ": (" + itemPrice + " Salvage)";
                if(itemstock > 0)
                {
                    BuyButton.SetActive(true);
                }
                else
                {
                    BuyButton.SetActive(false);
                }
            }
            if(selectedItem == 3)
            {
                selectedItemText.GetComponent<Text>().text = item3.GetComponent<Image>().sprite.name + ": (" + itemPrice + " Salvage)";
                if(itemstock > 0)
                {
                    BuyButton.SetActive(true);
                }
                else
                {
                    BuyButton.SetActive(false);
                }
            }
        }
        else
        {
            selectedItemText.GetComponent<Text>().text = "";
            BuyButton.SetActive(false);
        }
    }

    public void resetShop()
    {
        selectedItem = 0;
    }
    public void selectItem1()
    {
        selectedItem = 1;
    }

    public void selectItem2()
    {
        selectedItem = 2;
    }

    public void selectItem3()
    {
        selectedItem = 3;
    }


    public void buyItem()
    {
        if ((selectedItem == 1 || selectedItem == 2) && player.salvage >= itemPrice && itemstock > 0)
        {
            if (selectedItem == 1)
            {
                Instantiate(damageItemPrefabs[item1index], player.transform.position, player.transform.rotation);
                itemstock--;
                player.salvage -= itemPrice;
            }
            if(selectedItem == 2)
            {
                Instantiate(utilityItemPrefabs[item2index], player.transform.position, player.transform.rotation);
                itemstock--;
                player.salvage -= itemPrice;
            }

        }
        if (selectedItem == 3 && player.salvage >= itemPrice && itemstock > 0)
        {
            Instantiate(Alienade, player.transform.position, player.transform.rotation);
            itemstock--;
            player.salvage -= itemPrice;
        }
    }



    void assignShopItems()
    {
        item1index = Random.Range(0, damageItemPrefabs.Length);
        item1.GetComponent<Image>().sprite = damageItemPrefabs[item1index].GetComponent<SpriteRenderer>().sprite;
        item1Text.GetComponent<Text>().text = damageItemPrefabs[item1index].name;

        item2index = Random.Range(0, utilityItemPrefabs.Length);
        item2.GetComponent<Image>().sprite = utilityItemPrefabs[item2index].GetComponent<SpriteRenderer>().sprite;
        item2Text.GetComponent<Text>().text = utilityItemPrefabs[item2index].name;


        item3index = Random.Range(0, survivalItemPrefabs.Length);
        item3.GetComponent<Image>().sprite = survivalItemPrefabs[item3index].GetComponent<SpriteRenderer>().sprite;
        item3Text.GetComponent<Text>().text = survivalItemPrefabs[item3index].name;

        itemstock = 3;

        

        itemPrice = Random.Range(7 + player.currentLevel, 15 + player.currentLevel * 2);
    }
}
