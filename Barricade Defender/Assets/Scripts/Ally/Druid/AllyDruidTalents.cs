using UnityEngine;
using UnityEngine.UI;

public class AllyDruidTalents : MonoBehaviour
{
    public GameObject TalentPanel;
    public Button stTierButton;
    public Button[] ndTierButton;
    public Button[] rdTierButton;

    private bool canUse2ndTier = false;
    private bool canUse3rdTier = false;
    private string rdSideTalent;
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

        if (canUse3rdTier == true && allyDruid.Level >= 15)
        {
            if (rdSideTalent == "left")
            {
                rdTierButton[0].interactable = true;
                rdTierButton[1].interactable = true;

                rdTierButton[2].interactable = false;
            }
            else if (rdSideTalent == "right")
            {
                rdTierButton[2].interactable = true;

                rdTierButton[0].interactable = false;
                rdTierButton[1].interactable = false;
            }
            else
            {
                Debug.LogWarning("incorrect string passed when advancing to third talent tier");
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

    private string Return3rdSideTalent(string side)
    {
        return rdSideTalent = side;
    }

    public void Click2ndTier(int talent)
    {
        switch (talent)
        {
            case 0:
                allyDruid.SpellDmgModifier = 8;
                Return3rdSideTalent("left");
                break;

            case 1:
                allyDruid.SkillTargetCount = 5;
                Return3rdSideTalent("right");
                break;

            default:
                break;
        }

        canUse2ndTier = false;
        canUse3rdTier = true;

        foreach (Button item in ndTierButton)
        {
            item.interactable = false;
        }
    }

    public void Click3rdTier(int talent)
    {
        switch (talent)
        {
            case 0:
                allyDruid.SpellDmgModifier = 16;
                break;

            case 1:
                allyDruid.SpellDmgModifier = 10;
                allyDruid.SkillTargetCount = 4;
                break;

            case 2:

                allyDruid.SkillTargetCount = 8;
                break;

            default:
                break;
        }

        canUse3rdTier = false;

        foreach (Button item in rdTierButton)
        {
            item.interactable = false;
        }
    }
}
