using UnityEngine;

public class FeetHitbox : MonoBehaviour
{
    public PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      playerStats = GameObject.FindAnyObjectByType<PlayerStats>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        playerStats.TakePoisonDamage(damage);
    }
}
