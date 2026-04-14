using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gameOverScoreText;
    [SerializeField] int score;
    [SerializeField] AudioClip clickSuccess;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    public void AddScore(int addition)
    {
        score += addition;
        if(gameOverScoreText != null)
            gameOverScoreText.text= "SCORE " + score;
        this.GetComponent<AudioSource>().PlayOneShot(clickSuccess);
        scoreText.text = "SCORE " + score;
    }

    
}
