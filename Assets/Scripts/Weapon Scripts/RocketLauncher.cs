using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Firearm
{
    public static RocketLauncher instance;
    // Start is called before the first frame update
    void Awake(){
        if(instance == null){
            instance = this;
        }
    }
    void Start()
    {
        ReloadAni.SetBool("isReloading", false);
        ammo = magSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        if (cooldown == 0)
        {
            if (Input.GetMouseButton(0) && ammo != 0)
            {
                animations.SetTrigger("Shoot");
                animations.SetFloat("Reload Speed", (float)(1.0/fireRate));
                Shoot();
            }
            else if (ammo == 0 || Input.GetKey(KeyCode.R))
            {
                ammo = 0;
                animations.SetTrigger("Reload");
                ReloadAni.SetBool("isReloading", true);
                Reload();
            }
        }
    }

}
