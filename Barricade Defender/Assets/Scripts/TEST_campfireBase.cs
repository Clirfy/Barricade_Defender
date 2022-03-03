using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEST_campfireBase : PlayerStats
{
    public Slider HpSlider;
    public Text HpText;

    private void Start()
    {
        HpCurrent = HpMax;
        HpSlider.maxValue = HpMax;
    }

    private void Update()
    {
        UpdateHpSlider();
    }

    private void UpdateHpSlider()
    {
        HpSlider.value = HpCurrent;
        HpText.text = HpCurrent + " / " + HpMax;
    }
}
