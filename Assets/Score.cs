using UnityEngine;
using UnityEngine.UI;


public class Score   : MonoBehaviour
{
    public Text scoreText; // Referencia al objeto de texto en el Canvas
    private int score = 0; // Puntaje inicial

    public void AddToScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    private void Update()
    {
        

    }
}
