using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    int pelletsLeft;
    public TMP_Text pelletText;
    int totalPellets;
    int pelletsEaten;
    public int lives = 3;


    void Awake()
    {
        Instance = this;
    }

    public void RegisterPellet()
    {
        pelletsLeft++;
        totalPellets++;
        UpdateUI();
    }

    public void PelletEaten(int points)
    {
        score += points;
        pelletsLeft--;
        pelletsEaten++;
        UpdateUI();

        if (pelletsLeft == 0)
            Debug.Log("Level complete!");
    }

    void UpdateUI()
    {
        pelletText.text = "Pellets: " + pelletsEaten + " / " + totalPellets;
        pelletText.text = "Pellets: " + pelletsEaten + " / " + totalPellets + "   Lives: " + lives;
    }

    public void PacmanCaught()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
            GameOver();
        else
            ResetPositions();
    }

    void ResetPositions()
    {
        FindFirstObjectByType<PacmanController>().ResetToStart();

        foreach (Ghost g in FindObjectsByType<Ghost>())
            g.ResetToStart();
    }

    void GameOver()
    {
        pelletText.text = "GAME OVER";
        Time.timeScale = 0f;
    }
}