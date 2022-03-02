using UnityEngine.UI;

public class BarricadeController : PlayerStats
{
    public Slider HpSlider;

    private void Start()
    {
        CurrentHp = MaxHp;
        HpSlider.maxValue = MaxHp;
    }

    private void Update()
    {
        Death();
        UpdateHpSlider();
    }

    private void Death()
    {
        if(CurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void UpdateHpSlider()
    {
        HpSlider.value = CurrentHp;
    }
}
