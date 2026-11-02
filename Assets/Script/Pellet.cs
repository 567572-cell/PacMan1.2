using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;

    void Start()
    {
        GameManager.Instance.RegisterPellet();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PelletEaten(points);
            Destroy(gameObject);
        }
    }
}