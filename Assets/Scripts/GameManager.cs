using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject secreteArea;
    [SerializeField] private GameObject secreteArea2;

    [Header("Character Customization")]
    [SerializeField] private Button skinChangeButton; 
    [SerializeField] private Texture2D charactertex;
    [SerializeField] private Material playerMaterial;
    

    [Header("CharacterInitialSetup")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private Texture2D characterTex;

    private void Awake()
    {
        player.GetComponent<PlayerController>().enabled = false;
        characterMaterial.mainTexture = characterTex;
    }

    public void OnChangeTexturesClicked()
    {
        playerMaterial.mainTexture = charactertex;
        player.GetComponent<PlayerController>().enabled = true;
        skinChangeButton.gameObject.SetActive(false);
    }
}
