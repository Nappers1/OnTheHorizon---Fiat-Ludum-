using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    public void AddScore(int addition)
    {
        score += addition;
        scoreText.text = "SCORE " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
