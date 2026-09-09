using UnityEngine;

public class DashCooldownscrip : MonoBehaviour
{
    PlayerMovement PlayerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the dash cooldown UI based on PlayerMovement's current dash cooldown
        float progress = 1.0f - (PlayerMovement.dashCDCounter / PlayerMovement.dashCooldown);
        
        gameObject.GetComponent<UnityEngine.UI.Image>().fillAmount = Mathf.Clamp01(progress);

    }
}
