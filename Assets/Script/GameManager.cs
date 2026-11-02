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
    }
}