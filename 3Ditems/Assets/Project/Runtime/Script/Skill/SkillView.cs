using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class SkillView : Singleton<SkillView>
{
    [field: SerializeField] private GameObject skillObject { get; set; }

    [field: SerializeField] private Image currentSkillImage;
    [field: SerializeField] private TextMeshProUGUI currentSkillsText;
    public ItemSlot currentSkil;
    public bool hasCurrentSkill => currentSkil != null;
    bool isShowSkills = false;

    private void Awake()
    {
        skillObject.SetActive(isShowSkills);
        currentSkillImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!hasCurrentSkill) return;
        currentSkillImage.transform.position = Input.mousePosition;
    }

    public void SetCurrentSkill(ItemSlot skill)
    {
        currentSkil = skill;
        currentSkillsText.text = skill.amount.ToString();
        currentSkillImage.sprite = skill.item.itemImage;

        currentSkillImage.gameObject.SetActive(true);
    }

    public void CheckCurrentItem()
    {
        currentSkillsText.text = currentSkil.amount.ToString();

        if (hasCurrentSkill && currentSkil.amount < 1) ResetCurrentItem();
    }

    public void ResetCurrentItem()
    {
        currentSkil = null; 
        currentSkillImage.gameObject.SetActive(false);
    }

    public void ShowSkillsView()
    {
        isShowSkills = !isShowSkills;
        skillObject.SetActive(isShowSkills);
        InitSkillsViews();
    }

    private bool init = false;
    public void InitSkillsViews()
    {
        if(!init)
        {
            SkillViewController.instance.InitSkillViews();
            init = true;
        }
    }
}
