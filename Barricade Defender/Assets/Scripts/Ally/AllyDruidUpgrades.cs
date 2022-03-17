using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllyDruidUpgrades : MonoBehaviour
{
    private AllyDruid allyDruid;
    private BaseCampfire baseCampfire;

    [SerializeField]
    private int levelUpCost;
    [SerializeField]
    private int upgradeSkillCost;
    [SerializeField]
    private TextMeshProUGUI levelUpText;
    [SerializeField]
    private TextMeshProUGUI upgradeSkillText;
    [SerializeField]
    private TextMeshProUGUI mainPanelText;

    private void Start()
    {
        allyDruid = GetComponent<AllyDruid>();
        baseCampfire = FindObjectOfType<BaseCampfire>();

        UpdateTextInfo();
    }

    public void LevelUp()
    {
        if (levelUpCost <= baseCampfire.Money)
        {
            baseCampfire.TakeMoney(levelUpCost);
            levelUpCost = Mathf.RoundToInt(levelUpCost * 1.5f);
            allyDruid.Damage += 1;
            UpdateTextInfo();
        }
        else
        {
            Debug.LogWarning("not enough gold to level up");
        }
    }

    public void UpgradeSkill()
    {
        if (upgradeSkillCost <= baseCampfire.Money)
        {
            baseCampfire.TakeMoney(upgradeSkillCost);
            upgradeSkillCost *= 2;
            allyDruid.SlowPower += .1f;
            UpdateTextInfo();
        }
        else
        {
            Debug.LogWarning("not enough gold to upgrade skill");
        }
    }

    private void UpdateTextInfo()
    {
        levelUpText.text = levelUpCost.ToString();
        upgradeSkillText.text = upgradeSkillCost.ToString();
        mainPanelText.text = "Damage: " + allyDruid.Damage + "\nSlow Power: " + (allyDruid.SlowPower * 100) + "%";
    }
}
