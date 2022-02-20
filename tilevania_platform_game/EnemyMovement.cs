using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f; 

    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider2D;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag != "Hazards")
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f); //the Mathf.Sign returns 1 if the number is >=0, or -1 if the number is < 0
    }
}
