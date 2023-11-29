using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class InventoryView : Singleton<InventoryView>
{
    [SerializeField] private GameObject inventoryObeject;

    [Space(20)]
    [SerializeField] private Image currentItemImage;
    [SerializeField] private TextMeshProUGUI currentItemAmount;

    public ItemSlot currentItem;
    public bool hasCurrentItem => currentItem != null;

    private void Awake()
    {
        inventoryObeject.SetActive(false);

        currentItemImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        currentItemToMousePos();
    }

    // ���� ��� �ִ� �������� ȭ����� ���콺 ��ġ�� ����
    private void currentItemToMousePos()
    {
        if (!hasCurrentItem)
        {
            return;
        }
        currentItemImage.transform.transform.position = Input.mousePosition;
    }

    // currnet �����ۿ� ������ ����
    public void SetCurrentItem(ItemSlot item)
    {
        currentItem = item;
        currentItemAmount.text = item.amount.ToString();
        currentItemImage.sprite = item.item.itemImage;

        currentItemImage.gameObject.SetActive(true);
    }

    public void CheckCurrentItem()
    {
        currentItemAmount.text = currentItem.amount.ToString();

        if (hasCurrentItem && currentItem.amount < 1) ResetCurrentItem();
    }

    public void ResetCurrentItem()
    {
        currentItem = null;
        currentItemImage.gameObject.SetActive(false);
    }

    // �κ��丮 â 
    bool isShowInventory = false;
    public void ShowInventory()
    {
        isShowInventory = !isShowInventory;
        inventoryObeject.SetActive(isShowInventory);
        InitInventorys();
    }

    // ó������ �κ��丮�� ���ٸ� �κ��丮â ����
    bool init = false;
    public void InitInventorys()
    {
        if(!init)
        {
            InventoryController.instance.InitInventory();

            init = true;
        }
    }
}
