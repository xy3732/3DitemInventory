using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;

    [SerializeField] private GameObject inventorySlot;
    
    [field: Header("Inventory")]
    [field: SerializeField] private int inventorySize { get; set; }
    [field: SerializeField] private GameObject inventroyParent { get; set; }

    [Header("armor")]
    [SerializeField] private int armorSlotSize;
    [SerializeField] private GameObject aromorParent;

    private Slot[] itemSlots { get; set; }
    private Slot[] armorSlots { get; set; }

    private void Awake()
    {
        instance = this;
    }

    public void InitInventory()
    {
        // 슬롯 생성 
        CreateSlots();

        // 처음 인벤토리에 들고 있을 아이템
        itemSlots[0].SetItem(new ItemSlot(ItemManager.instance.items[0],5));
        itemSlots[1].SetItem(new ItemSlot(ItemManager.instance.items[1],10));
        itemSlots[2].SetItem(new ItemSlot(ItemManager.instance.items[0], 11));
    }

    public void addItems(int num)
    {
       for(int i= 0; i< itemSlots.Length; i++)
        {
            if(itemSlots[i].item == null)
            {
                itemSlots[i].SetItem(new ItemSlot(ItemManager.instance.items[num], 1));
                break;
            }
        }
    }

    // 인벤토리 슬롯을 정해진 갯수만큼 생성
    private void CreateSlots()
    {
        itemSlots = new Slot[inventorySize];
        armorSlots = new Slot[armorSlotSize];

        for (int i = 0; i < inventorySize; i++)
        {
            createSlot(i, itemSlots, inventroyParent, SlotType.none);
        }

        for(int i =0; i< armorSlotSize; i++)
        {
            createSlot(i, armorSlots, aromorParent, SlotType.armor);
        }
    }

    private void createSlot(int arr, Slot[] slots, GameObject parent, SlotType type)
    {
        var slot = Instantiate(inventorySlot);
        slots[arr] = slot.AddComponent<Slot>();

        slot.GetComponent<Slot>().slotType = type;
        slot.transform.SetParent(parent.transform);
        slot.transform.localScale = new Vector3(0.54f, 0.54f, 0.54f);
    }
}
