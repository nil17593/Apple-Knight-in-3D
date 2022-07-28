using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private int coinScore = 0;

    public static ScoreController instance;

    private void Awake()
    {
        instance = this;
    }
    //private void Start()
    //{
    //    RefreshUI();
    //}

    //the score will increase by 10 points after each key collect
    public void IncreaseScore(int increament)
    {
        coinScore += increament;
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = "Coins: " + coinScore;
    }
}
