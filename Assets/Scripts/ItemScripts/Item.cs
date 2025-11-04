using System.Collections;
using System.Collections.Generic;
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
	public virtual void OnDamage(int stacks)
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
}

public class Accelerant : Item
{
	//Greatly Increases Sprint Speed
	public override string name { get { return "Accelerant"; } }


    public override float getDashCD(int stacks)
    {
		return Mathf.Pow(0.95f,stacks);
    }
    public override float getDashLength(int stacks)
    {
		return 0.5f * (float)stacks;
    }
}

public class Alienade : Item
{
	public override string name { get { return "Alienade"; } }
	// Reduces Dash cooldown- gatorade but alien
	public override void OnPickup()
	{
		PlayerStats.instance.health += 0.25f * PlayerStats.instance.maxHealth;
		if (PlayerStats.instance.health > PlayerStats.instance.maxHealth)
		{
			PlayerStats.instance.health = PlayerStats.instance.maxHealth;
		}
	}
}

public class AmmoBelt : Item
{
	public override string name { get { return "AmmoBelt"; } }
	//This item doubles the magazine capacity of the player's currently equipped weapon and Decreases reload speed.

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
    public override void OnHit(int stacks, GameObject gameobject)
	{
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
		explosion.transform.localScale = new Vector3(1f+(.1f*(float)stacks), 1f + (.1f * (float)stacks), 1f + (.1f * (float)stacks));
		explosion.transform.position = gameobject.transform.position;
		explosion.GetComponent<Explosion>().damage = gameobject.GetComponent<Bullet>().damage * (.1f*(float)stacks);
	}

}

public class HazmatSuit : Item
{
	public override string name { get { return "HazmatSuit"; } }
	// Poison Immunity and health up
	public override void OnPickup()
	{
		PlayerStats.instance.Armor += 5;
		PlayerStats.instance.HazardResistance += 15;
	}
}

public class IceSkates : Item
{
	public override string name { get { return "IceSkates"; } }
	//Increases base movement speed(not dash)
	public override void OnPickup()
	{
		PlayerMovement.instance.moveSpeed += 2f;
	}
	public override float getDashCD(int stacks)
	{
		return Mathf.Pow(.67f, stacks);
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
       for(int i = 0; i < stacks * 2; i++)
        {
			Vector3 spreadTrans = Random.insideUnitCircle * (float)(.5f * (float)stacks);
			spreadTrans += mousePosition;
			RocketLauncher.instance.CreateBullet(spreadTrans, 0.5f * RocketLauncher.instance.bulletSize, RocketLauncher.instance.damage * 0.05f, RocketLauncher.instance.projectileSpeed);
		}
    }
}

public class Petrifier : Item
{
	public override string name { get { return "Petrifier"; } }
	// When you enter a room, chance to freeze enemies for some amount of time.
	public override void OnRoomEnter(int stacks, Enemy[] enemies)
    {
		if (enemies.Length > 1)
		{
			foreach (Enemy e in enemies)
			{
				if (Random.Range(0, 10) == 8)
				{
					e.gameObject.GetComponent<Enemy>().stun(5f + (2 * (float)stacks-1));
				}

			}

		}
	}
}

public class RocketBullets : Item
{
	public override string name { get { return "RocketBullets"; } }
	//Makes your bullets fly faster and do more damage
	public override void OnPickup()
	{
		RocketLauncher.instance.projectileSpeed += 5.0f;

	}
}

public class TrainingWheels : Item
{
	public override string name { get { return "TrainingWheels"; } }
	//Increases Invincibility frames for the player
	public override void OnPickup()
	{
		PlayerStats.instance.InvulnerabilitySeconds += 1.0f;
	}
}

public class TrumpCard : Item
{
	public override string name { get { return "TrumpCard"; } }
    //The last bullet fired does a lot more damage, is a lot bigger, and flies slower.
    public override void OnHit(int stacks, GameObject gameobject)
    {
        
		if (Random.value < stacks/52f)
		{
			gameobject.GetComponent<Bullet>().damage *= 2;
		}
		
    }
}

public class BackFire : Item
{
	float cooldown = 0;
	public override string name { get { return "BackFire"; } }
    //Makes a bullet appear behind the player opposite in direction when the player shoots
    public override void update(PlayerStats player, Firearm firearm, PlayerMovement mobil)
    {
		cooldown -= Time.deltaTime;
    }
    public override void OnDamage(int stacks)
    {
        if(cooldown <= 0)
        {
			GameObject AOE = GameObject.Instantiate(Resources.Load("PS", typeof(GameObject))) as GameObject;
			var dr = AOE.GetComponent<ParticleSystem>().main;
			dr.duration = 3*stacks;
			AOE.GetComponent<BackFireShieldEffect>().duration = 5 + 3 * stacks;
			AOE.GetComponent<CircleCollider2D>().radius += 0.1f * stacks;
			var sh = AOE.GetComponent<ParticleSystem>().shape;
			sh.radius += 0.1f * stacks;
			cooldown = 5 + 3 * stacks + 10;
		}
    }
}