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

    // ���Կ� ������ ����
    public void SetItem(ItemSlot item)
    {
        this.item = item;
        UpdateUI();
    }


    // ���Կ� ������ �߰�
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

    // ���� �ʱ�ȭ
    public void ResetItem()
    {
        item = null;
        UpdateUI();
    }


    private void UpdateUI()
    {
        // ���Կ� �ش� �������� �̹����� �ٲ۴�.
        itemImage.gameObject.SetActive(hasItem);
        itemImage.sprite = item?.item.itemImage;

        // ���Կ� �ش� �������� ������ �ٲ۴�.
        itemAmount.gameObject.SetActive(hasItem && item.amount > 1);
        itemAmount.text = item?.amount.ToString();
    }

    // Ŭ�� ������
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
            // ���� ���콺�� ��� �ִ� �������� ������ �ش� �������κ��� ������ ��������
            // �Ǵ� ���Կ� �ִ� ������ �ϰ� ��� �ִ� �������� �ٸ��ٸ� ���� ��ġ �ٲٱ�
            if ((currentItem == null || item.item != currentItem.item))
            {
                InventoryView.instance.SetCurrentItem(item);
                ResetItem();
            }

            // ��� �ִ� �������̶� ���� ������ �������� ������ �ش� ���Կ� �߰�
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
        // �Ǵ� current ������ �ʱ�ȭ
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

        // ���� ��� �ִ� �������� �ش� �������� ��ȯ�ϱ�
        if (currentItem != null) SetItem(currentItem);
    }

    // ���콺�� ���ö�
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���Կ� ���̶���Ʈ ����
        slotImage.color = highlightedColor;
    }

    // ���콺�� ������
    public void OnPointerExit(PointerEventData eventData)
    {
        // ���Կ� ����Ʈ ����
        slotImage.color = defaultColor;
    }
}
