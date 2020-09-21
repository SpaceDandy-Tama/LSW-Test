using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public string Name;
    public int Price;

    public ProductSlot OccupiedSlot;
    public bool PlayerOwned = false;

    private bool Dragging;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Instance.NameText.text = Name;
        Tooltip.Instance.PriceText.text = Price.ToString() + " $";
        Tooltip.Instance.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!PlayerOwned && TradeMenu.Instance.Money < Price)
            return;

        Dragging = true;
        transform.SetAsLastSibling();
        OccupiedSlot.Product = null;
        OccupiedSlot = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Dragging)
            return;

        Dragging = false;

        float shortestDist = float.MaxValue;
        ProductSlot closestSlot = null;
        bool isInventorySlot = false;

        for(int i = 0; i < TradeMenu.Instance.ProductSlots.Length; i++)
        {
            if (TradeMenu.Instance.ProductSlots[i].Product != null)
                continue;

            float dist = Vector2.Distance(transform.position, TradeMenu.Instance.ProductSlots[i].transform.position);
            if(dist < shortestDist)
            {
                shortestDist = dist;
                closestSlot = TradeMenu.Instance.ProductSlots[i];
            }
        }

        for (int i = 0; i < TradeMenu.Instance.InventorySlots.Length; i++)
        {
            if (TradeMenu.Instance.InventorySlots[i].Product != null)
                continue;

            float dist = Vector2.Distance(transform.position, TradeMenu.Instance.InventorySlots[i].transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closestSlot = TradeMenu.Instance.InventorySlots[i];
                isInventorySlot = true;
            }
        }

        OccupiedSlot = closestSlot;
        OccupiedSlot.Product = this;
        transform.position = OccupiedSlot.transform.position;

        if (isInventorySlot)
        {
            transform.localScale = Vector3.one * 0.77f;
            if (!PlayerOwned)
            {
                TradeMenu.Instance.Money -= Price;
                PlayerOwned = true;
            }
        }
        else
        {
            transform.localScale = Vector3.one;
            if (PlayerOwned)
            {
                TradeMenu.Instance.Money += Price / 2;
                PlayerOwned = false;
            }
        }
    }

    private void Update()
    {
        if (Dragging)
        {
            transform.position = Input.mousePosition;
            transform.localScale = (Vector3.one * (0.77f + (transform.position.y / Screen.height) * 0.23f));
        }
    }
}
