using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;

    void Awake()
    {
        ManageSingleton();   
    }

    private void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        score = score + value;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
