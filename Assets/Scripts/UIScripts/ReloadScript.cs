using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadScript : MonoBehaviour
{

    void Update(){
        float aMod = 1;
        foreach (ItemList i in PlayerStats.instance.items)
        {
            aMod += i.item.AmmoChange(i.stacks);
        }
        int ammo = (int)RocketLauncher.instance.ammo;
        int mag = (int)((float) RocketLauncher.instance.magSize * aMod);
        this.transform.GetComponent<UnityEngine.UI.Text>().text = ammo + "/" + mag;
    }

    
    
}
