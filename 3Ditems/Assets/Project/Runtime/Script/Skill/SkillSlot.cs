using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum slotType
    { 
        haveSkillSlot,
        selectSkillSlot
    }
    public slotType type;
    [SerializeField] private Image slotImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemAmount;

    private Color defaultColor = new Color32(255, 255, 255, 255);
    private Color highlightedColor = new Color32(200, 200, 200, 255);

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

        if (!hasItem && type == slotType.selectSkillSlot)
        {
            SetItem(new ItemSlot(item.item, amount));
        }
        else
        {
            this.item.amount += amount;
            UpdateUI();
        }
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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
    }

    public void LeftClick()
    {
        var currentSkill = SkillView.instance.currentSkil;

        if (hasItem)
        {
            // ���� ���콺�� ��� �ִ� �������� ������ �ش� �������κ��� ������ ��������
            // �Ǵ� ���Կ� �ִ� ������ �ϰ� ��� �ִ� �������� �ٸ��ٸ� ���� ��ġ �ٲٱ�
            if (currentSkill == null || item.item != currentSkill.item)
            {
                SkillView.instance.SetCurrentSkill(item);
                ResetItem();
            }
            // ��� �ִ� �������̶� ���� ������ �������� ������ �ش� ���Կ� �߰�
            else
            {
                AddItem(currentSkill, currentSkill.amount);
                SkillView.instance.CheckCurrentItem();

                return;
            }
        }
        // �Ǵ� current ������ �ʱ�ȭ
        else SkillView.instance.ResetCurrentItem();

        // ���� ��� �ִ� �������� �ش� �������� ��ȯ�ϱ�
        if (currentSkill != null) SetItem(currentSkill);
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
