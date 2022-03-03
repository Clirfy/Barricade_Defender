using UnityEngine.UI;

public class BarricadeController : PlayerStats
{
    public Slider HpSlider;

    private void Start()
    {
        HpCurrent = HpMax;
        HpSlider.maxValue = HpMax;
    }

    private void Update()
    {
        Death();
        UpdateHpSlider();
    }

    private void Death()
    {
        if(HpCurrent <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void UpdateHpSlider()
    {
        HpSlider.value = HpCurrent;
    }
}
