using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour 
{
    public PlayerMain player;
    public GameObject gameover;
    public TMP_Text pointsText;
    public TMP_Text bestScoreText;

    private float bestScore;
    private float score;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        score = Mathf.Round(player.score);
        bestScore = Mathf.Round(player.highScore);
        // bestScore = PlayerPrefs.GetFloat("HighScore", 0); // Load the best score from player preferences
    }

    private void Start()
    {
        pointsText.text = Mathf.Round(score).ToString();

        if (score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetFloat("HighScore", bestScore); // Save the new best score in player preferences
            //UpdateBestScoreText();
        }

        UpdateBestScoreText();

        gameover.SetActive(false);
    }

    //public void Setup(float score)
    //{
        
    //    gameObject.SetActive(true);
        
    //    if (score > bestScore)
    //    {
    //        bestScore = score;

    //        PlayerPrefs.SetFloat("HighScore", bestScore); // Save the new best score in player preferences
    //        UpdateBestScoreText();
    //    }
    //}

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MenuMain");
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "BEST: " + bestScore;
    }
}
