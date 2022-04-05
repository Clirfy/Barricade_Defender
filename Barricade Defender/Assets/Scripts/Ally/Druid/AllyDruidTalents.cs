using UnityEngine;
using UnityEngine.UI;

public class AllyDruidTalents : MonoBehaviour
{
    public GameObject TalentPanel;
    public Button stTierButton;
    public Button[] ndTierButton;

    private bool canUse2ndTier = false;
    private AllyDruid allyDruid;
    private AllyDruidUpgrades allyUpgrades;

    private void Start()
    {
        allyDruid = GetComponentInParent<AllyDruid>();
        allyUpgrades = GetComponentInParent<AllyDruidUpgrades>();

        allyUpgrades.OnLevel5.AddListener(ListenOnLevel5);

        allyDruid.SpellDmgModifier = 2;
    }

    private void Update()
    {
        if (canUse2ndTier == true && allyDruid.Level >= 10)
        {
            foreach (Button item in ndTierButton)
            {
                item.interactable = true;
            }
        }
    }

    public void ClickSwapTalentPanelDisplay()
    {
        TalentPanel.SetActive(!TalentPanel.activeSelf);
    }

    private void ListenOnLevel5()
    {
        stTierButton.interactable = true;
    }

    public void Click1stTier()
    {
        stTierButton.interactable = false;

        allyDruid.SpellDmgModifier = 4;
        allyDruid.SkillTargetCount = 3;
        canUse2ndTier = true;
    }

    public void Click2ndTier(int talent)
    {
        switch (talent)
        {
            case 0:
                allyDruid.SpellDmgModifier = 8;
                break;

            case 1:
                allyDruid.SkillTargetCount = 5;
                break;

            default:
                break;
        }

        canUse2ndTier = false;

        foreach (Button item in ndTierButton)
        {
            item.interactable = false;
        }
    }
}
