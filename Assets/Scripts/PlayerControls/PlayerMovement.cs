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

    public float dashMultiplyer; //How fast the dash occurs
    public float dashLength = 0.25f; //Distance covered by dash
    public float dashCD = 1.0f; //Time before you can dash again

    
    private float dashCounter; // Where in the dash are we
    private float dashCDCounter; // Where in the cooldown are we

    private float dashActiveSpeed; // Internal variable for changing speed

    public bool Dashing;
    /*********************End Dash Functionalities *****************/
    
    
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
        ProcessInputs();
        ProcessDash();
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && (dashCDCounter + dashCounter <= 0)){
            AfterImage.instance.enable = true;
            Dashing = true;
            dashActiveSpeed = moveSpeed * dashMultiplyer;


            foreach (ItemList i in PlayerStats.instance.items)
            {
                dashM += i.item.getDashLength(i.stacks);
            }

            dashCounter = dashLength * dashM;
            playerStats.Armor += 300;

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
            if(dashCounter <= 0){
                float itemstats = 1;
                foreach (ItemList i in PlayerStats.instance.items)
                {
                   itemstats *= i.item.getDashCD(i.stacks);
                }
                foreach (ItemList i in PlayerStats.instance.items)
                {
                    dashM += i.item.getDashLength(i.stacks);
                }
                AfterImage.instance.enable = false;
                dashActiveSpeed = moveSpeed;
                dashCDCounter = ((dashLength * dashM * dashCD * itemstats ));
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
