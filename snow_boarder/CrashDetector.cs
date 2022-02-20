using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float waitReload = 1f;
    bool alreadyPlayed = false;

    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashAudio;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Solid" && alreadyPlayed == false)
        {
            alreadyPlayed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashAudio);
            Invoke("ReloadScene", waitReload);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
