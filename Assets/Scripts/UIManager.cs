using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: " + GameManager.instance.score.ToString("0000");
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.instance.score.ToString("0000");
    }

    public void ResetLevel()
    {
        Debug.Log("Respawning");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
