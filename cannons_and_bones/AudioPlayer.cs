using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip[] shootingClips;
    [SerializeField] [Range(0f, 1f)] float shootingVolume;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] [Range(0f, 1f)] float explosionVolume;


    void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if(instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        if(shootingClips != null)
        {
            int random = Random.Range(0, shootingClips.Length);
            AudioSource.PlayClipAtPoint(shootingClips[random], Camera.main.transform.position, shootingVolume);
        }
    }

    public void PlayExplosionClip()
    {
        if(explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, explosionVolume);
        }
    }
}
