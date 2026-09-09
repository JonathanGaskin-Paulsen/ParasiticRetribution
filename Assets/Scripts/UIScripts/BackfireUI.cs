using UnityEngine;
using UnityEngine.UI;

public class BackfireUI : MonoBehaviour
{
   public PlayerStats player;

    [SerializeField] private Image fillImage;
    [SerializeField] private Image backgroundImage;

   public bool hasBackfire = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindFirstObjectByType<PlayerStats>();
        fillImage = gameObject.GetComponent<Image>();
        backgroundImage = transform.parent.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hasBackfire = player.items.Exists(item => item.name == "BackFire");

        int backfireindex = player.items.FindIndex(item => item.name == "BackFire");
        if (backfireindex != -1)
        {
            BackFire backfire = (BackFire)player.items[backfireindex].item;
            fillImage.fillAmount = 1f - backfire.cooldown / 15f;
        }


        if (hasBackfire)
        {
           Color temp = fillImage.color;
            temp.a = 1f;
            fillImage.color = temp;

            temp = backgroundImage.color;
            temp.a = 1f;
            backgroundImage.color = temp;


            


        }
        else
        {
            Color temp = fillImage.color;
            temp.a = 0f;
            fillImage.color = temp;
            temp = backgroundImage.color;
            temp.a = 0f;
            backgroundImage.color = temp;   
        }
    }
}
