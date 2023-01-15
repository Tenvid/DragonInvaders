using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static void SetScoreText(float rawScore, TextMeshProUGUI scoreLabel)
    {
        string score = "Score: ";
        if(rawScore > 1000)
            score += rawScore * 0.001f + " K";
        else if (rawScore > 1000000)
            score += rawScore * 0.0000001f + " M";
        else
            score += rawScore;

        scoreLabel.text = score;
    }
}
