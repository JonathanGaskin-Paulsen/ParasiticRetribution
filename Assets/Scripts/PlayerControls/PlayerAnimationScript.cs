using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    public Animator PlayerAnimator;

    public PlayerAnimationScript instance;


    void Awake(){
        if (instance == null) {
            instance = this;
        }
    }
    void FixedUpdate()
    {
        Vector2 moveDir = PlayerMovement.instance.moveDir;
        if (moveDir.x < 0)
        {
            PlayerAnimator.SetBool("isMovingLeft", true);
            PlayerAnimator.SetBool("isMovingRight", false);
        }
        if (moveDir.x > 0)
        {
            PlayerAnimator.SetBool("isMovingRight", true);
            PlayerAnimator.SetBool("isMovingLeft", false);
        }
        if(moveDir.x == 0){
            PlayerAnimator.SetBool("isMovingLeft", false);
            PlayerAnimator.SetBool("isMovingRight", false);
        }
        if (moveDir.y < 0)
        {
            PlayerAnimator.SetBool("isMovingDown", true);
            PlayerAnimator.SetBool("isMovingUp", false);
        }
        if (moveDir.y > 0)
        {
            PlayerAnimator.SetBool("isMovingUp", true);
            PlayerAnimator.SetBool("isMovingDown", false);
        }
        if(moveDir.y == 0){
            PlayerAnimator.SetBool("isMovingDown", false);
            PlayerAnimator.SetBool("isMovingUp", false);
        }
    }
}
