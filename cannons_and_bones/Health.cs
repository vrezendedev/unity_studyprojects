using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField]bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    ScoreKeeper scoreKeeper;
    [SerializeField] int points;

    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            audioPlayer.PlayExplosionClip();
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(isPlayer == false)
        {
            scoreKeeper.ModifyScore(points);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    private void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake == true)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
