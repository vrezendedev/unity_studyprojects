using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField]float waitReload = 1f;
    [SerializeField] ParticleSystem finishEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", waitReload);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
