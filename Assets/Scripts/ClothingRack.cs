using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothingRack : MonoBehaviour
{
    public Button ClothingRackButton;
    public Button BrowseButton;
    public Text BrowseButtonText;
    public GameObject TradeMenu;

    private void Start()
    {
        BrowseButton.interactable = false;
        BrowseButtonText.gameObject.SetActive(false);

        ClothingRackButton.onClick.AddListener(delegate { OnClothingRackClicked(); });
        BrowseButton.onClick.AddListener(delegate { OnBrowseButtonClicked(); });
    }

    private void OnClothingRackClicked()
    {
        BrowseButton.interactable = true;
        BrowseButtonText.gameObject.SetActive(true);
    }

    private void OnBrowseButtonClicked()
    {
        BrowseButton.interactable = false;
        BrowseButtonText.gameObject.SetActive(false);

        ClothingRackButton.interactable = false;
        TradeMenu.SetActive(true);
    }
}
