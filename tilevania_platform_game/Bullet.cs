using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    PlayerMovement player;

    [SerializeField] float bulletSpeed = 0f;
    float bulletDirection;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        bulletDirection = player.transform.localScale.x * bulletSpeed;
        transform.localScale = new Vector2(player.transform.localScale.x, transform.localScale.y);
    }

    
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(bulletDirection, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.1f);
    }
}
