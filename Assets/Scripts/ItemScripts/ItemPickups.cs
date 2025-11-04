using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickups : MonoBehaviour
{
    public Item item;
    public Items itemDrop;
    // Start is called before the first frame update
    void Start()
    {
        item = AssignItem(itemDrop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        Debug.Log("Item Pickup called");
        if (player.tag == "Player")
        {
            AddItem(PlayerStats.instance);
            Destroy(this.gameObject);
        }
    }

    public void AddItem(PlayerStats player)
    {
        item.OnPickup();
        foreach (ItemList i in PlayerStats.instance.items)
        {
            if(i.name == item.name)
            {
                i.stacks++;
                return;
            }
        }
        PlayerStats.instance.items.Add(new ItemList(item, item.name, 1));
    }
    public Item AssignItem(Items itemToAssign)
    {
        switch (itemToAssign)
        {
            case Items.Accelerant:
                return new Accelerant();
            case Items.Alienade:
                return new Alienade();
            case Items.AmmoBelt:
                return new AmmoBelt();
            case Items.ElbowGrease:
                return new ElbowGrease();
            case Items.ExplosiveBullets:
                return new ExplosiveBullets();
            case Items.HazmatSuit:
                return new HazmatSuit();
            case Items.IceSkates:
                return new IceSkates();
            case Items.IonSplitter:
                return new IonSplitter();
            case Items.Petrifier:
                return new Petrifier();
            case Items.RocketBullets:
                return new RocketBullets();
            case Items.TrainingWheels:
                return new TrainingWheels();
            case Items.TrumpCard:
                return new TrumpCard();
            case Items.BackFire:
                return new BackFire();
            default:
                return null;
             
        }
    }
}

public enum Items
{
    Accelerant,
    Alienade,
    AmmoBelt,
    ElbowGrease,
    ExplosiveBullets,
    HazmatSuit,
    IceSkates,
    IonSplitter,
    Petrifier,
    RocketBullets,
    TrainingWheels,
    TrumpCard,
    BackFire
}