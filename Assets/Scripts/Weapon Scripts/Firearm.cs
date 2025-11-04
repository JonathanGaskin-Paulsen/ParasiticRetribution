using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firearm : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPNG; 
    public GameObject hands;
    public GameObject ExplosionPng;

    public AudioSource reloadSound;
    public AudioSource fireSound;

    public Transform bulletSpawnPoint;

    public float projectileSpeed;
    public float reloadTime;
   
    public float bulletSize = 2.0f;

    public float spread = 15.0f;

    protected float reloading = 0;
   
    public float damage;
    public float fireRate;

    public int magSize;
    public int ammo;
    
    protected float cooldown;

    public bool backFire = false;
    public bool trumpCard = false;

    public Animator ReloadAni;
    public Animator animations;


    protected void FixedUpdate()
    {
        float FMul = 1;
        foreach (ItemList i in PlayerStats.instance.items)
        {
            FMul *= i.item.FireRateMultiplier(i.stacks);
        }
        if (cooldown < (fireRate * FMul) && cooldown != 0)
        {
            cooldown += Time.deltaTime;
        }
        else if (cooldown > (fireRate * FMul))
        {
            animations.SetTrigger("Idel");
            cooldown = 0;
        }
    }

    public void Shoot(){
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
            fireSound.Play();
            if(ammo == 1 && trumpCard){
                CreateBullet(1.0f * mousepos, 4 * bulletSize, 4 * damage, projectileSpeed / 3.0f);
                if (backFire)
                {
                    CreateBullet((1.0f * mousepos), 4 * bulletSize, 4 * damage, -projectileSpeed / 3.0f);
                }
            }
            else{
                CreateBullet(1.0f * mousepos, bulletSize, damage, projectileSpeed);
                if (backFire)
                {
                    CreateBullet((1.0f * mousepos), bulletSize, damage, -projectileSpeed);
                }

            }
        foreach (ItemList i in PlayerStats.instance.items)
        {
            i.item.OnFire(i.stacks, mousepos);
        }


        ammo--;
        
        cooldown += Time.deltaTime;

    }

    public void CreateBullet(Vector3 destination, float _bulletSize, float _damage, float _projectileSpeed){
        GameObject bullet = GameObject.Instantiate(bulletPNG);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = hands.transform.rotation;
        bullet.transform.Rotate(0,0,-90);
        bullet.transform.localScale = new Vector3(_bulletSize,_bulletSize,1);
        

        bullet.AddComponent<Rigidbody2D>();
        bullet.AddComponent<Bullet>();
        
        bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        bullet.AddComponent<CapsuleCollider2D>();
        bullet.GetComponent<CapsuleCollider2D>().isTrigger = true;
        bullet.GetComponent<Bullet>().PS = _projectileSpeed;
        bullet.GetComponent<Bullet>().damage = _damage;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        bullet.GetComponent<Bullet>().destination = destination;
    }


    public void Reload(){
        float rMod = 1;
        float aMod = 1;
        float RMul = 1;
        foreach (ItemList i in PlayerStats.instance.items)
        {
            rMod += i.item.ReloadSpeed(i.stacks);
        }
        foreach (ItemList i in PlayerStats.instance.items)
        {
            aMod += i.item.AmmoChange(i.stacks);
        }
        foreach (ItemList i in PlayerStats.instance.items)
        {
            RMul *= i.item.ReloadSpeedMultiplier(i.stacks);
        }
        if (reloading < (reloadTime * rMod * RMul)){
            reloading += Time.deltaTime;
        }
        else{
            reloadSound.Play();
            ammo = (int) ((float)magSize * aMod);
            reloading = 0;
            animations.SetTrigger("Idel");
            ReloadAni.SetBool("isReloading", false);
        }
    }


}
