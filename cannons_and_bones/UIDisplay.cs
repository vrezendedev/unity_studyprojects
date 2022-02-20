using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper ScoreKeeper;

    void Awake()
    {
        ScoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();    
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = ScoreKeeper.GetScore().ToString("000000000");
    }
}
