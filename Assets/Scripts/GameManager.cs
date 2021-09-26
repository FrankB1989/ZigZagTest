using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText, scoreAmount, bestScoreAmount, newBestScoreText;
    [SerializeField]
    private AudioClip _gameOverClip, _bestScoreGameOverClip;
    private AudioClip _gameOverClipSelected;

    private int score, bestScore;

    CinemachineBrain cameraBrain;

    #region Singleton
    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        } 
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _gameOverClipSelected = _gameOverClip;
        cameraBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    //Function that make all the logic when the player die
    public void GameOver()
    {
        cameraBrain.enabled = false;
        gameOverPanel.SetActive(true);
        scoreAmount.text = score.ToString();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            newBestScoreText.gameObject.SetActive(true);
            _gameOverClipSelected = _bestScoreGameOverClip;
        }
        bestScoreAmount.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        AudioSource.PlayClipAtPoint(_gameOverClipSelected, cameraBrain.transform.position);
    }

    //Restart level function
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Update text score when pickup object
    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }

    //Exit game (Only work in build)
    public void ExitGame()
    {
        Application.Quit();
        PlayerPrefs.DeleteKey("BestScore");
    }
}
