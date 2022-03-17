using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpUpdater : MonoBehaviour
{
    private BaseCampfire baseCampfire;

    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private Slider hpSlider;


    private void Start()
    {
        baseCampfire = FindObjectOfType<BaseCampfire>();

        hpSlider.maxValue = baseCampfire.HpMax;
        UpdateHp(baseCampfire.HpCurrent, baseCampfire.HpMax);
        baseCampfire.OnHpChanged.AddListener(ListenOnHpChanged);
    }

    private void ListenOnHpChanged()
    {
        UpdateHp(baseCampfire.HpCurrent, baseCampfire.HpMax);
    }

    private void UpdateHp(int hpCurrent, int hpMax)
    {
        hpText.text = hpCurrent + "/" + hpMax;
        hpSlider.value = hpCurrent;
    }
}
