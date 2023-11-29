using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillViewController : Singleton<SkillViewController>
{
    [field: SerializeField] private int skillsSlotsSize;
    [field: SerializeField] private GameObject skillSlotsParent;
    [field: SerializeField] private GameObject skillsSlots;

    [field: Space(20)]
    [field: SerializeField] private int currentSKillSlotSize;
    [field: SerializeField] private GameObject currentSkillSlotParent;

    public SkillSlot[] slots { get; private set; }
    public SkillSlot[] currentSkillSlot { get; private set; }

    public void InitSkillViews()
    {
        CreateSlots();
        CreateSkillSlots();

        slots[0].SetItem(new ItemSlot(ItemManager.instance.items[0], 1));
    }

    private void CreateSlots()
    {
        slots = new SkillSlot[skillsSlotsSize];

        for(int i=0; i< skillsSlotsSize; i++)
        {
            var slot = Instantiate(skillsSlots);
            slots[i] = slot.AddComponent<SkillSlot>();
            slot.GetComponent<SkillSlot>().type = SkillSlot.slotType.haveSkillSlot;
            slot.transform.SetParent(skillSlotsParent.transform);
        }
    }

    private void CreateSkillSlots()
    {
        currentSkillSlot = new SkillSlot[currentSKillSlotSize];

        for(int i= 0;i<currentSKillSlotSize; i++)
        {
            var slot = Instantiate(skillsSlots);
            currentSkillSlot[i] = slot.AddComponent<SkillSlot>();
            slot.GetComponent<SkillSlot>().type = SkillSlot.slotType.selectSkillSlot;
            slot.transform.SetParent(currentSkillSlotParent.transform);
        }
    }
}
