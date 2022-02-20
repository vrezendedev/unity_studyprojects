using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    [SerializeField]GameObject bullet;
    [SerializeField] Transform myBulletSpawn;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] Vector2 deathBounce = new Vector2(10f, 10f);
    Vector2 moveInput;

    float gravityScaleAtStart;
    bool isAlive = true;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        if(isAlive == true)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    private void OnMove(InputValue value)
    {
        if(isAlive == true)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) && isAlive == true)
        {
            myRigidbody.velocity = myRigidbody.velocity + new Vector2(0f, jumpSpeed);
        } 
    }

    private void OnFire(InputValue value)
    {
        if(isAlive == true)
        {
            if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
                {
                    Instantiate(bullet, myBulletSpawn.position, transform.rotation);
                }
            }
            else
            {
                Instantiate(bullet, myBulletSpawn.position, transform.rotation);
            }
            
        }

    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2((moveInput.x * runSpeed) , myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerIsRunning = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerIsRunning);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed == true)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) == true)
        {
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, (moveInput.y * climbSpeed));
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0;

            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
        }
        
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myFeetCollider.enabled = false;
            myBodyCollider.size = new Vector2(myBodyCollider.size.x, 0.75f);
            myRigidbody.velocity = deathBounce;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        } 
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            isAlive = false;
            myFeetCollider.enabled = false;
            myRigidbody.drag = 20f;
            myBodyCollider.size = new Vector2(myBodyCollider.size.x, 0.75f);
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    public bool CheckAlive()
    {
        return isAlive;
    }

}
