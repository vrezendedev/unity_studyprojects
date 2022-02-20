using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] float baseFiringRate;
    
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance;
    [SerializeField] float minimumFiringRate;

    [HideInInspector]public bool isFiring;
    Coroutine firingCoroutine;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring == true && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(isFiring == false && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }

    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
