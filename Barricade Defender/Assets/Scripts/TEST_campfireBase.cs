using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEST_campfireBase : PlayerStats
{
    public Slider HpSlider;
    private void Start()
    {
        CurrentHp = MaxHp;
        HpSlider.maxValue = MaxHp;
    }

    private void Update()
    {
        UpdateHpSlider();
    }

    private void UpdateHpSlider()
    {
        HpSlider.value = CurrentHp;
    }
}
