using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementalCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementalQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float) questionsSeen * 100);
    }

}
