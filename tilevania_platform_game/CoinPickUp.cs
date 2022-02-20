using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] int coinPoints = 100;
    [SerializeField]AudioClip audioPickUp;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && wasCollected == false && FindObjectOfType<PlayerMovement>().CheckAlive() == true)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().IncreaseScore(coinPoints);
            Destroy(gameObject, 0.05f);
            AudioSource.PlayClipAtPoint(audioPickUp, Camera.main.transform.position, 0.10f);
        }
    }
}
