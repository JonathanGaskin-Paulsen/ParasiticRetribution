using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopMenu ShopPrefab;
    bool isShopOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       ShopPrefab = FindAnyObjectByType<ShopMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            ShopPrefab.gameObject.GetComponent<Animator>().SetTrigger("Show");
            Cursor.visible = true;
            isShopOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        { 
            ShopPrefab.gameObject.GetComponent<Animator>().SetTrigger("Show");
            Cursor.visible = false;
            isShopOpen = false;
        }
    }
}
