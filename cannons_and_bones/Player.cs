using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    Vector2 playerInput;

    //Boundaries
    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    //Shooter
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();    
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        PlayerMovement();
    }

    private void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    private void PlayerMovement()
    {
        Vector2 delta = playerInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight); 
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
    }
}
