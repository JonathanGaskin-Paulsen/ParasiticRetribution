using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidBodyPlayer;
    public Vector2 moveDir;
    public PlayerStats playerStats;
    private Vector2 moveInput;

    /***************** Dash Functionalities **********************/

    public float dashMultiplyer; //How much faster the dash is than normal movement
    public float dashLength; //How long the dash occurs
    public float dashCD; //Time before you can dash again

    
    private float dashCounter; // Where in the dash are we
    public float dashCDCounter; // Where in the cooldown are we

    public float dashCooldown; // Public variable for UI to access

    private float dashActiveSpeed; // Internal variable for changing speed

    public bool Dashing; // Are we currently dashing?
    /*********************End Dash Functionalities *****************/

    public SceneTransition sceneTransition;

    public static PlayerMovement instance;

    
    void Awake(){
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dashActiveSpeed = moveSpeed;
        rigidBodyPlayer.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneTransition == null)
        {
            sceneTransition = FindFirstObjectByType<SceneTransition>();
        }
        else
        {
            if (!sceneTransition.isTransitioning)
            {
                ProcessInputs();
                ProcessDash();
            }
            else
            {
                rigidBodyPlayer.linearVelocity = Vector2.zero;
                moveDir = Vector2.zero;
            }
        }
    }

    //Fixed Update is called at a fixed framerate frame independent of device fps
    void FixedUpdate(){
        Move();
    }

    void ProcessInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveDir = moveInput.normalized;
    }

    void Move(){
        if (!Dashing)
        {
            dashActiveSpeed = moveSpeed;
        }
        rigidBodyPlayer.linearVelocity = moveDir * dashActiveSpeed;
        
    }
    void ProcessDash(){
        float dashM = 1;

        if (Input.GetKeyDown(KeyCode.LeftShift) && (dashCDCounter <= 0)){
            AfterImage.instance.enable = true;
            Dashing = true;
            dashActiveSpeed = moveSpeed * dashMultiplyer;


            foreach (ItemList i in PlayerStats.instance.items)
            {
                dashM += i.item.getDashLength(i.stacks);
            }

            dashCounter = dashLength * dashM;
            playerStats.Armor += 300;
            playerStats.Invincibility = true;

            foreach (ItemList i in PlayerStats.instance.items)
            {
                if (i.name == "Accelerant")
                {
                    AfterImage.instance.lifetime = 0.30f;
                    AfterImage.instance.color = true;
                }
            }
        }
        //Input Recieved, begin waiting until cooldowns finished or current dash finishes
        if(dashCounter > 0){
            dashCounter -= Time.deltaTime;
            if (dashCounter <= (dashLength*dashM)*(1.0 - PlayerStats.instance.DashInvulnerabilityRatio))
            {
                playerStats.Invincibility = false;
            }
            if(dashCounter <= 0){
                float itemstats = 1;
                foreach (ItemList i in PlayerStats.instance.items)
                {
                   itemstats += i.item.getDashCD(i.stacks);
                }

                itemstats = Mathf.Max(0.1f, itemstats);


                AfterImage.instance.enable = false;
                dashActiveSpeed = moveSpeed;
                dashCooldown = ( dashCD * itemstats);
                dashCDCounter = dashCooldown;
                playerStats.Armor -= 300;
                AfterImage.instance.lifetime = 0.15f;
                AfterImage.instance.color = false;
                Dashing = false;
            }
        }
        if(dashCDCounter > 0){
            dashCDCounter -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && Dashing) {
            foreach (ItemList i in PlayerStats.instance.items)
            {
                i.item.OnDashContact(i.stacks, collision.gameObject);
            }
        }
    }

}
