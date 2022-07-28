using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject secreteArea;
    [SerializeField] private GameObject secreteArea2;

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

    [SerializeField] private GameObject playerDiePanel;


    public static GameManager instance;
    private void Awake()
    {
        playerDiePanel.SetActive(false);
        canProgress = false;
        instance = this;
        player.GetComponent<PlayerController>().enabled = false;
        characterMaterial.mainTexture = characterTex;
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
}
