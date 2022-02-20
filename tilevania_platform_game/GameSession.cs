using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int numGameSessions;
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerPoints = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerPoints.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

   IEnumerator TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        yield return new WaitForSecondsRealtime(1f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void IncreaseScore(int points)
    {
        playerPoints = playerPoints + points;
        scoreText.text = playerPoints.ToString();
    }
}
