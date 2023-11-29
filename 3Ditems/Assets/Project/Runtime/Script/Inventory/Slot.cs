using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image slotImage;
    private Image itemImage;
    private TextMeshProUGUI itemAmount;

    private Color defaultColor = new Color32(255, 255, 255, 255);
    private Color highlightedColor = new Color32(200, 200, 200, 255);

    public SlotType slotType { get; set; }
    public ItemSlot item { get; private set; }
    public bool hasItem => item != null;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemAmount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // 슬롯에 아이템 설정
    public void SetItem(ItemSlot item)
    {
        this.item = item;
        UpdateUI();
    }


    // 슬롯에 아이템 추가
    public void AddItem(ItemSlot item, int amount)
    {
        item.amount -= amount;

        if (!hasItem)
        {
            SetItem(new ItemSlot(item.item, amount));
        }
        else
        {
            this.item.amount += amount;
            UpdateUI();
        }
    }

    public void removeItem()
    {
        GameManager.instance.CreateObejct(item.item.id);

        this.item.amount -= 1;
        if (item.amount <= 0)
        {
            item = null;
        }

        UpdateUI();
    }

    // 슬롯 초기화
    public void ResetItem()
    {
        item = null;
        UpdateUI();
    }


    private void UpdateUI()
    {
        // 슬롯에 해당 아이템의 이미지로 바꾼다.
        itemImage.gameObject.SetActive(hasItem);
        itemImage.sprite = item?.item.itemImage;

        // 스롯에 해당 아이템의 갯수로 바꾼다.
        itemAmount.gameObject.SetActive(hasItem && item.amount > 1);
        itemAmount.text = item?.amount.ToString();
    }

    // 클릭 했을때
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            rightClick();
        }
    }

    public void rightClick()
    {
        if(hasItem)
        {
            removeItem();
        }
    }

    public void LeftClick()
    {
        var currentItem = InventoryView.instance.currentItem;

        if (hasItem)
        {
            // 현재 마우스가 들고 있는 아이템이 없으면 해당 슬롯으로부터 아이템 가져오기
            // 또는 슬롯에 있는 아이템 하고 들고 있는 아이템이 다르다면 서로 위치 바꾸기
            if ((currentItem == null || item.item != currentItem.item))
            {
                InventoryView.instance.SetCurrentItem(item);
                ResetItem();
            }

            // 들고 있는 아이템이랑 현재 슬롯의 아이템이 같으면 해당 슬롯에 추가
            else
            {
                if (slotType == SlotType.none)
                {
                    AddItem(currentItem, currentItem.amount);
                    InventoryView.instance.CheckCurrentItem();
                }

                return;
            }
        }
        // 또는 current 아이템 초기화
        else
        {
            if(currentItem != null && slotType == SlotType.armor)
            {
                if (currentItem.item.ItemType != slotType)
                {
                    return;
                }
            }

            InventoryView.instance.ResetCurrentItem();
        }

        // 현재 들고 있는 아이템을 해당 슬롯으로 반환하기
        if (currentItem != null) SetItem(currentItem);
    }

    // 마우스가 들어올때
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 슬롯에 하이라이트 설정
        slotImage.color = highlightedColor;
    }

    // 마우스가 나갈때
    public void OnPointerExit(PointerEventData eventData)
    {
        // 슬롯에 디폴트 설정
        slotImage.color = defaultColor;
    }
}
