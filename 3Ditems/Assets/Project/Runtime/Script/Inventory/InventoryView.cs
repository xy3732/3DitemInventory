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

    // 현재 들고 있는 아이템을 화면상의 마우스 위치로 변경
    private void currentItemToMousePos()
    {
        if (!hasCurrentItem)
        {
            return;
        }
        currentItemImage.transform.transform.position = Input.mousePosition;
    }

    // currnet 아이템에 아이템 설정
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

    // 인벤토리 창 
    bool isShowInventory = false;
    public void ShowInventory()
    {
        isShowInventory = !isShowInventory;
        inventoryObeject.SetActive(isShowInventory);
        InitInventorys();
    }

    // 처음으로 인벤토리를 연다면 인벤토리창 생성
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
