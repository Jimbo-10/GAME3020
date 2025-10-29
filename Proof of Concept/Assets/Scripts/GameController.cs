using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI healthText;

    public int score;
    public int health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeScore(int scoreChangeAmount)
    {
        score += scoreChangeAmount;
        string scoreMessage = "Score: " + score;
        scoreText.text = scoreMessage;
    }

    public void HealthChange(int healthChangeAmount)
    {
        health -= healthChangeAmount;
        string healthMessage = "Health: " + health;
        healthText.text = healthMessage;
    }
}
