using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public float InvulnerabilitySeconds;
    public float DashInvulnerabilityRatio;
    private float tempSeconds;
    public int currentLevel = 0;
    public bool Invincibility = false;
    public float Armor;
    public float HazardResistance;
    public float salvage;

    public List<ItemList> items = new List<ItemList>();

    public static PlayerStats instance;
    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (ItemList i in PlayerStats.instance.items)
        {
            i.item.update(PlayerStats.instance, RocketLauncher.instance, PlayerMovement.instance);

            if(i.name == "Alienade")
            {
                if(i.stacks > 3)
                {
                    i.item.onUse(PlayerStats.instance);
                }
            }

        }
        if (tempSeconds != 0)
        {
            tempSeconds += Time.deltaTime;
        }
        if (tempSeconds >= InvulnerabilitySeconds)
        {
            tempSeconds = 0;
        }
        processinput();
    }

    public void TakeDamage(float d)
    {

        if (tempSeconds == 0 && !Invincibility)
        {
            foreach (ItemList i in PlayerStats.instance.items)
            {
                i.item.OnDamage(i.stacks, ref d);
            }
            health -= (d * (100 / (100 + Armor)));
            tempSeconds += Time.deltaTime;
            StartCoroutine(Invulnerability());
            if (health <= 0)
            {

                while (health <= 0)
                {
                    bool hasAlienade = false;
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        if (items[i].name == "Alienade")
                        {
                            items[i].item.onUse(PlayerStats.instance);
                            hasAlienade = true;
                        }
                    }
                    if (!hasAlienade)
                    {
                        break;
                    }
                }

                if (health <= 0)
                {
                    PlayerStats.instance = null;
                    PlayerPrefs.SetInt("score", currentLevel);
                    gameObject.GetComponent<PlayerMovement>().sceneTransition.moveScene("DeathScreen", 5.0f);
                }
            }

        }
    }

    public void removePlayer()
    {
        Object.Destroy(gameObject);
    }

    public void TakePoisonDamage(float d)
    {
        if (!Invincibility)
        {
            if (tempSeconds == 0)
            {
                health -= (d * (100 / (100 + HazardResistance)));
                tempSeconds += Time.deltaTime;
                StartCoroutine(Poisoned());
                if (health <= 0)
                {

                    while (health <= 0)
                    {
                        bool hasAlienade = false;
                        for (int i = items.Count - 1; i >= 0; i--)
                        {
                            if (items[i].name == "Alienade")
                            {
                                items[i].item.onUse(PlayerStats.instance);
                                hasAlienade = true;
                            }
                        }
                        if (!hasAlienade)
                        {
                            break;
                        }
                    }

                    if (health <= 0)
                    {
                        PlayerStats.instance = null;
                        PlayerPrefs.SetInt("score", currentLevel);
                        SceneManager.LoadScene("DeathScreen");
                    }

                }
            }
        }
    }

    void processinput()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            for(int i = items.Count-1; i >= 0; i--) 
            {
                    items[i].item.onUse(PlayerStats.instance);
            }
        }
    }



    IEnumerator Invulnerability()
    {

        int numberOfFlashes = 3;

        SpriteRenderer spriteRend = gameObject.GetComponent<SpriteRenderer>();
        Shader shaderGUItext = Shader.Find("GUI/Text Shader");
        Shader shaderSpritesDefault = Shader.Find("Sprites/Default");
        Color color = Color.white;
        color.a = 0.5f;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.material.shader = shaderGUItext;
            spriteRend.color = color;

            yield return new WaitForSeconds(InvulnerabilitySeconds / (numberOfFlashes * 2));

            spriteRend.material.shader = shaderSpritesDefault;
            spriteRend.color = color;

            yield return new WaitForSeconds(InvulnerabilitySeconds / (numberOfFlashes * 2));
        }
        spriteRend.color = Color.white;

    }



    IEnumerator Poisoned()
    {
        int numberOfFlashes = 5;

        SpriteRenderer spriteRend = gameObject.GetComponent<SpriteRenderer>();
        Shader shaderGUItext = Shader.Find("GUI/Text Shader");
        Shader shaderSpritesDefault = Shader.Find("Sprites/Default");
        Color color = Color.green;
        color.a = 0.5f;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.material.shader = shaderGUItext;
            spriteRend.color = color;


            yield return new WaitForSeconds(InvulnerabilitySeconds / (numberOfFlashes * 2));

            spriteRend.material.shader = shaderSpritesDefault;
            spriteRend.color = color;

            yield return new WaitForSeconds(InvulnerabilitySeconds / (numberOfFlashes * 2));
        }
        spriteRend.color = Color.white;


    }
}
