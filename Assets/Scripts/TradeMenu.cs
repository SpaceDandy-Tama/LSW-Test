using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeMenu : MonoBehaviour
{
    public static TradeMenu Instance;

    public Button ClothingRackButton;
    public Button CloseButton;
    public Text InventoryMoney;

    public ProductSlot[] ProductSlots;
    public ProductSlot[] InventorySlots;

    private int PlayerMoney = 1000;
    public int Money
    #region Property
    {
        get => PlayerMoney;
        set
        {
            PlayerMoney = value;
            InventoryMoney.text = PlayerMoney + " $";
        }
    }
    #endregion

    private void Start()
    {
        Instance = this;
        Money = 5000;

        gameObject.SetActive(false);
        CloseButton.onClick.AddListener(delegate { OnCloseButtonClicked(); });
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
        ClothingRackButton.interactable = true;
    }
}
