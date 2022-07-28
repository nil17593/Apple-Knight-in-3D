using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject playerDiePanel;

    [Header("Character Customization")]
    [SerializeField] private GameObject characterCustomizationPanel;
    [SerializeField] private Button skinChangeButton; 
    [SerializeField] private Texture2D charactertex;
    [SerializeField] private Material playerMaterial;

    public bool canProgress;
    [Header("CharacterInitialSetup")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private Texture2D characterTex;

    [SerializeField] private TextMeshProUGUI scoreText;

    //[SerializeField] private int coinScore = 0;
    //[SerializeField] private GameObject achievementPanel;
    //[SerializeField] private TextMeshProUGUI achievementText;
    //[SerializeField] private CoinsCollectedAchievementSO coinachievementSO;
    //[SerializeField] private GameObject canvas;

    public static GameManager instance;

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}
        playerDiePanel.SetActive(false);
        canProgress = false;
        instance = this;
        player.GetComponent<PlayerController>().enabled = false;
        characterMaterial.mainTexture = characterTex;
    }

    private void Start()
    {
        instance = this;
    }
    public void OnChangeTexturesClicked()
    {
        canProgress = true;
        playerMaterial.mainTexture = charactertex;
        characterCustomizationPanel.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        skinChangeButton.gameObject.SetActive(false);
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void EnableRestartPanel()
    {
        playerDiePanel.gameObject.SetActive(true);
    }

    //public void IncreaseScore(int increament)
    //{
    //    Debug.Log(coinScore);
    //    coinScore += increament;
    //    RefreshUI();
    //    CheckForCoinAchievement();
    //}

    //private void RefreshUI()
    //{
    //    scoreText.text = "Coins: " + coinScore;
    //}

    //public void CheckForCoinAchievement()
    //{
    //    for (int i = 0; i < coinachievementSO.coinscollectedArray.Length; i++)
    //    {
    //        if (coinScore == coinachievementSO.coinscollectedArray[i].requirement)
    //        {
    //            string achievement = coinachievementSO.coinscollectedArray[i].coinsAchievementsType.ToString();
    //            StartCoroutine(UnlockedAchievement(achievement));
    //            return;
    //        }
    //    }
    //}

    //IEnumerator UnlockedAchievement(string achievement)
    //{
    //    achievementText.text = "Acheivment Unlocked: " + achievement;
    //    achievementPanel.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    achievementPanel.SetActive(false);
    //}
}
