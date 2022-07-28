using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private int coinScore = 0;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private TextMeshProUGUI achievementText;
    [SerializeField] private CoinsCollectedAchievementSO coinachievementSO;


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
        Debug.Log(coinScore);
        coinScore += increament;
        RefreshUI();
        CheckForCoinAchievement();
    }

    private void RefreshUI()
    {
        scoreText.text = "Coins: " + coinScore;
    }

    public void CheckForCoinAchievement()
    {
        for(int i = 0; i < coinachievementSO.coinscollectedArray.Length; i++)
        {
            if (coinScore == coinachievementSO.coinscollectedArray[i].requirement)
            {
                string achievement = coinachievementSO.coinscollectedArray[i].coinsAchievementsType.ToString();
                StartCoroutine(UnlockedAchievement(achievement));
                return;
            }
        }
    }

    IEnumerator UnlockedAchievement(string achievement)
    {
        achievementText.text = "Acheivment Unlocked: " + achievement;
        achievementPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        achievementPanel.SetActive(false);
    }
}
