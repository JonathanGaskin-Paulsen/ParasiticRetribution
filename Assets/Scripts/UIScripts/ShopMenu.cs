using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject item1, item1Text, item2, item2Text, item3, item3Text;
    public int item12Price, item3Price, item12stock, item3stock, selectedItem, item1index, item2index;
    public GameObject stocktext, alienadestock, salvageText, BuyButton, selectedItemText, Alienade;
    public GameObject[] damageItemPrefabs;
    public GameObject[] utilityItemPrefabs;

    public PlayerStats player;

    public Shop shop;
    
    private List<Sprite> Damagetems = new List<Sprite>();
    private List<Sprite> UtilityItems = new List<Sprite>();
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        shop = FindAnyObjectByType<Shop>();
        assignShopItems();
    }

    // Update is called once per frame
    void Update()
    {
        stocktext.GetComponent<Text>().text = "Item Stock: " + item12stock;
        alienadestock.GetComponent<Text>().text = "Alienade Stock: " + item3stock;
        salvageText.GetComponent<Text>().text = "Salvage: " + player.salvage;
        if(selectedItem != 0)
        {
            if(selectedItem == 1)
            {
                selectedItemText.GetComponent<Text>().text = item1.GetComponent<Image>().sprite.name + ": (" + item12Price + " Salvage)";
                if(item12stock > 0)
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
                selectedItemText.GetComponent<Text>().text = item2.GetComponent<Image>().sprite.name + ": (" + item12Price + " Salvage)";
                if(item12stock > 0)
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
                selectedItemText.GetComponent<Text>().text = item3.GetComponent<Image>().sprite.name + ": (" + item3Price + " Salvage)";
                if(item3stock > 0)
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
        if ((selectedItem == 1 || selectedItem == 2) && player.salvage >= item12Price && item12stock > 0)
        {
            if (selectedItem == 1)
            {
                Instantiate(damageItemPrefabs[item1index], player.transform.position, player.transform.rotation);
                item12stock--;
                player.salvage -= item12Price;
            }
            if(selectedItem == 2)
            {
                Instantiate(utilityItemPrefabs[item2index], player.transform.position, player.transform.rotation);
                item12stock--;
                player.salvage -= item12Price;
            }

        }
        if (selectedItem == 3 && player.salvage >= item3Price && item3stock > 0)
        {
            Instantiate(Alienade, player.transform.position, player.transform.rotation);
            item3stock--;
            player.salvage -= item3Price;
        }
    }



    void assignShopItems()
    {
        item1index = Random.Range(0, damageItemPrefabs.Length);
        item1.GetComponent<Image>().sprite = damageItemPrefabs[item1index].GetComponent<SpriteRenderer>().sprite;
        item1Text.GetComponent<Text>().text = damageItemPrefabs[item1index].name;

        item2index = Random.Range(0, UtilityItems.Count);
        item2.GetComponent<Image>().sprite = utilityItemPrefabs[item2index].GetComponent<SpriteRenderer>().sprite;
        item2Text.GetComponent<Text>().text = utilityItemPrefabs[item2index].name;


        item3.GetComponent<Image>().sprite = Alienade.GetComponent<SpriteRenderer>().sprite;
        item3Text.GetComponent<Text>().text = Alienade.name;

        item12stock = 3;
        item3stock = 5;

        

        item12Price = Random.Range(7 + player.currentLevel, 15 + player.currentLevel * 2);

        item3Price = 5;
    }
}
