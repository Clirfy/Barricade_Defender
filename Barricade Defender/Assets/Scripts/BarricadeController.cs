using UnityEngine;
using UnityEngine.UI;

public class BarricadeController : PlayerStats
{
    public Slider HpSlider;
    public Sprite[] PalisadeSprites;
    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        HpCurrent = HpMax;
        HpSlider.maxValue = HpMax;
    }

    private void Update()
    {
        Death();
        UpdateHpSlider();
        UpdateBarricadeSprite();
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

    private void UpdateBarricadeSprite()
    {
        if (HpCurrent >= HpMax * .66f)
        {
            SpriteRenderer.sprite = PalisadeSprites[0];
        }

        if (HpCurrent >= HpMax * .33f && HpCurrent < HpMax * .66f)
        {
            SpriteRenderer.sprite = PalisadeSprites[1];
        }

        if (HpCurrent < HpMax * .33f)
        {
            SpriteRenderer.sprite = PalisadeSprites[2];
        }
    }
}
