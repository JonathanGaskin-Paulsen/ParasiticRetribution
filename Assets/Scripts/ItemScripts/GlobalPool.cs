using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalPool : MonoBehaviour
{
    //Holds all the items in the game- 
    public static GlobalPool instance;
    public List<Item> pool = new List<Item>();
    
    //Repeat for every item- Define each item in the ItemDefinitions folder.

    AmmoBelt ammobelt = new AmmoBelt();
    Accelerant accelerant = new Accelerant();
    Alienade alienade = new Alienade();
    BackFire backfire = new BackFire();
    ElbowGrease elbowgrease = new ElbowGrease();
    ExplosiveBullets explosivebullets = new ExplosiveBullets();
    HazmatSuit hazmatsuit = new HazmatSuit();
    IceSkates iceskates = new IceSkates();
    IonSplitter ionsplitter = new IonSplitter();
    Petrifier petrifier = new Petrifier();
    RocketBullets rocketbullets = new RocketBullets();
    TrainingWheels trainingwheels = new TrainingWheels();
    TrumpCard trumpcard = new TrumpCard();

   // Wurst wurst;


    //Add them to the array here
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        pool.Clear();
        pool.Add(ammobelt);   
        pool.Add(accelerant);
        pool.Add(alienade);
        pool.Add(backfire);
        pool.Add(elbowgrease);
        pool.Add(explosivebullets);
        pool.Add(hazmatsuit);
        pool.Add(iceskates);
        pool.Add(ionsplitter);
        pool.Add(petrifier);
        pool.Add(rocketbullets);
        pool.Add(trainingwheels);
        pool.Add(trumpcard);
    }

    public string getRandomItem(){
        int index = Random.Range(0,pool.Count);

    
        string itemName = pool[1].name;
        pool.RemoveAt(index);
        return itemName;
    }


}