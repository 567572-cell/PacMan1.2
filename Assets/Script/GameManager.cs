using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    int pelletsLeft;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPellet()
    {
        pelletsLeft++;
    }

    public void PelletEaten(int points)
    {
        score += points;
        pelletsLeft--;

        if (pelletsLeft == 0)
            Debug.Log("Level complete!");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 300, 40), "Score: " + score);
    }
}