using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class Item
{

	public abstract string name { get; }

	public virtual void update(PlayerStats player, Firearm firearm, PlayerMovement mobil)
	{

	}

	public virtual void OnPickup(){
	//Picking Up the item
	} 
	public virtual void OnHit(int stacks, GameObject gameobject)
	{
	 //Dealing damage
	}
	public virtual void OnHit(int stacks, Enemy enemy)
    {
        //Dealing damage
    }

    public virtual void OnDamage(int stacks, ref float damage)
	{
	 //Taking damage
	}

	public virtual void OnDashContact(int stacks, GameObject gameobject)
    {
		//Contact with enemy
    }
	public virtual void OnReload(){
	
	}
	public virtual void OnDash(){
	
	}
	public virtual void OnFire(int stacks)
	{
	//Firing gun
	}

	public virtual void OnFire(int stacks, Vector3 mousePosition)
	{
		//Firing gun
	}

	public virtual void onTermination(int stacks, Bullet bullet)
	{

	}

    public virtual void OnRoomEnter(int stacks, Enemy[] enemies)
	{

    }
    public virtual void OnRoomExit(){

    }
    public virtual void OnDefeatEnemy(){
    
    }
    public virtual void OnClearRoom(){
    //Defeating all the enemies of a room that has enemies
    }

	public virtual float AmmoChange(int stacks)
    {
		return 0;
    }

	public virtual float ReloadSpeed(int stacks)
    {
		return 0;
    }

	public virtual float ReloadSpeedMultiplier(int stacks)
	{
		return 1;
	}

	public virtual float FireRateMultiplier(int stacks)
	{
		return 1;
	}

	public virtual float getDashCD(int stacks)
    {
		return 1;
    }

	public virtual float getDashLength(int stacks)
    {
		return 0;
    }

	public virtual void onUse()
    {      
		//Using the item
    }
    public virtual void onUse(PlayerStats player)
    {
        //Using the item
    }
	public virtual void OnBulletUpdate(Rigidbody2D rigidbody, Vector3 target, int stacks)
	{

	}
	public virtual void OnBulletUpdate(int stacks, Bullet bullet)
    {

    }
}

public class Accelerant : Item
{
	//Greatly Increases Sprint Speed
	public override string name { get { return "Accelerant"; } }


    public override float getDashCD(int stacks)
    {
		return 0.5f * (float)stacks;
    }
    public override float getDashLength(int stacks)
    {
		return 0.5f * (float)stacks;
    }
}

public class Alienade : Item
{
	public override string name { get { return "Alienade"; } }
    // Heals the player for 25% of their max health when picked up
    public override void OnPickup()
	{

	}

    public override void onUse(PlayerStats player)
    {
        foreach (ItemList i in player.items)

        {
            PlayerStats.instance.health += 0.25f * PlayerStats.instance.maxHealth;
            if (PlayerStats.instance.health > PlayerStats.instance.maxHealth)
            {
                PlayerStats.instance.health = PlayerStats.instance.maxHealth;
            }


            if (i.name == "Alienade")
            {
				i.stacks--;
				if (i.stacks == 0)
				{
					player.items.Remove(i);
				}
                break;
            }
        }
    }
}

public class AmmoBelt : Item
{
	public override string name { get { return "AmmoBelt"; } }
	//This item doubles the magazine capacity of the player's currently equipped weapon and Increases reload speed.

    public override float AmmoChange(int stacks)
    {
		return 1.0f * (float)stacks;
	}

    public override float ReloadSpeed(int stacks)
    {
		return .25f * (float)stacks;
	}


}

public class ElbowGrease : Item
{
	public override string name { get { return "ElbowGrease"; } }
    //Increases reload speed and Fire rate

    public override float FireRateMultiplier(int stacks)
    {
		return Mathf.Pow(.75f, stacks);
	}

    public override float ReloadSpeedMultiplier(int stacks)
    {
		return Mathf.Pow(.75f, stacks);
    }

}

public class ExplosiveBullets : Item
{
	public override string name { get { return "ExplosiveBullets"; } }
    //Makes your bullets explode
    public override void onTermination(int stacks, Bullet bullet)
	{
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
		explosion.transform.localScale = new Vector3(1f+(.1f*(float)stacks), 1f + (.1f * (float)stacks), 1f + (.1f * (float)stacks));
		explosion.transform.position = bullet.transform.position;
		explosion.GetComponent<Explosion>().damage = bullet.GetComponent<Bullet>().damage * (.05f*(float)stacks);
	}

}

public class HazmatSuit : Item
{
	public override string name { get { return "HazmatSuit"; } }
	// Poison Immunity and health up
	public override void OnPickup()
	{
		PlayerStats.instance.Armor += 10;
		PlayerStats.instance.HazardResistance += 10;
	}
}

public class IceSkates : Item
{
	public override string name { get { return "IceSkates"; } }
	//Increases base movement speed(not dash)
	public override float getDashCD(int stacks)
	{
		return -0.20f * stacks;
	}
	public override void OnDashContact(int stacks, GameObject gameobject)
    {
		gameobject.gameObject.GetComponent<Enemy>().stun(.5f *(float)stacks);
	}
}

public class IonSplitter : Item
{
	public override string name { get { return "IonSplitter"; } }
	//Increases the number of bullets that come out of the gun (i.e like a shotgun)
    public override void OnFire(int stacks, Vector3 mousePosition)
    {
		Pointer pointer = GameObject.FindAnyObjectByType<Pointer>();
        Vector3 direction = pointer.transform.position - mousePosition;
        direction.Normalize();

        for (int i = 0; i < stacks * 2; i++)
        {
			Vector3 spreadTrans = Random.insideUnitCircle * (float)(1.0f * (float)stacks);

			Vector3 spawnLoc = pointer.transform.position - direction * (5.0f * (float)stacks) + spreadTrans;

			RocketLauncher.instance.CreateBullet(spawnLoc, 0.5f * RocketLauncher.instance.bulletSize, RocketLauncher.instance.damage * 0.05f, RocketLauncher.instance.projectileSpeed);
		}
    }
}

public class Petrifier : Item
{
	public override string name { get { return "Petrifier"; } }
	// When you enter a room, chance to freeze enemies for some amount of time.
	public override void OnHit(int stacks, Enemy enemy)
    {
		enemy.Poison();
		enemy.poisonDamage = 2.0f * (float)stacks;
    }
}

public class RocketBullets : Item
{
	public override string name { get { return "RocketBullets"; } }
	//Makes your bullets fly faster and do more damage
    public override void OnBulletUpdate(Rigidbody2D rigidbody, Vector3 target, int stacks)
    {
		if (target != null)
		{
			Vector2 tarDir = ((Vector2)target - rigidbody.position).normalized;
			Vector2 curDir = rigidbody.linearVelocity.normalized;

			float speed = rigidbody.linearVelocity.magnitude;

			Vector2 newDir = Vector2.Lerp(curDir, tarDir, 0.01f * (float)stacks).normalized;

			rigidbody.linearVelocity = newDir * speed;

        }

		

    }

    public override void OnBulletUpdate(int stacks, Bullet bullet)
    {
        bullet.damage += bullet.ogDamage * ((float)0.005f * (float)stacks);
    }
}

public class TrainingWheels : Item
{
	public override string name { get { return "TrainingWheels"; } }
	//Increases Invincibility frames for the player
	public override void OnPickup()
	{
		PlayerStats.instance.InvulnerabilitySeconds += 1.0f;
		PlayerStats.instance.DashInvulnerabilityRatio = Mathf.Lerp(PlayerStats.instance.DashInvulnerabilityRatio, 1.0f, 0.15f);
    }
}

public class TrumpCard : Item
{
	public override string name { get { return "TrumpCard"; } }
    //The last bullet fired does a lot more damage, is a lot bigger, and flies slower.
    public override void OnHit(int stacks, GameObject gameobject)
    {
		do
		{
			if (Random.value < stacks / 13f)
			{
				gameobject.GetComponent<Bullet>().damage *= 1.5f;
			}
			stacks = stacks - 13;
		} while (stacks > 0);
		
    }
}

public class BackFire : Item
{
	public float cooldown = 0;
	public float healingcooldown = 0;
	public override string name { get { return "BackFire"; } }
    //Makes a bullet appear behind the player opposite in direction when the player shoots
    public override void update(PlayerStats player, Firearm firearm, PlayerMovement mobil)
    {
		cooldown -= Time.deltaTime;
		healingcooldown -= Time.deltaTime;
		if (healingcooldown < 0 && cooldown >= 0)
		{
			player.health += 0.01f * player.maxHealth;
			if(player.health > player.maxHealth)
            {
                player.health = player.maxHealth;
            }	


            healingcooldown = 1.0f;
        }
    }
    public override void OnDamage(int stacks, ref float damage)
    {
        if(cooldown <= 0)
        {
			damage = damage * Mathf.Pow(0.85f, stacks);


			cooldown = 15;
		}
    }
}