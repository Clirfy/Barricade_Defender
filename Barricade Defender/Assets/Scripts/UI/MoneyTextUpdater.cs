using TMPro;
using UnityEngine;

public class MoneyTextUpdater : MonoBehaviour
{
    private BaseCampfire baseCampfire;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void Start()
    {
        baseCampfire = FindObjectOfType<BaseCampfire>();

        baseCampfire.OnMoneyChanged.AddListener(ListenOnMoneyChanged);
        UpdateMoney(baseCampfire.Money);
    }

    private void ListenOnMoneyChanged()
    {
        UpdateMoney(baseCampfire.Money);
    }

    private void UpdateMoney(int ammount)
    {
        moneyText.text = "Gold: " + ammount.ToString();
    }
}
