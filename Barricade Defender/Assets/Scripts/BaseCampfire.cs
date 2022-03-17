using UnityEngine;
using UnityEngine.Events;

public class BaseCampfire : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnHpChanged;
    [HideInInspector]
    public UnityEvent OnMoneyChanged;
    public int HpMax;
    public int Money;

    public int HpCurrent;

    private void Awake()
    {
        HpCurrent = HpMax;
    }

    public void GetMoney(int ammount)
    {
        Money += ammount;
        OnMoneyChanged.Invoke();
    }

    public void TakeMoney(int ammount)
    {
        if (ammount <= Money)
        {
            Money -= ammount;
            OnMoneyChanged.Invoke();
        }
        else
        {
            Debug.LogWarning("unable to spend money; insufficent money");
        }
    }

    public void TakeDamage(int damage)
    {
        HpCurrent -= damage;
        OnHpChanged.Invoke();
    }

    public void RestoreHealth(int ammount)
    {
        HpCurrent += ammount;

        if (HpCurrent > HpMax)
        {
            HpCurrent = HpMax;
        }

        OnHpChanged.Invoke();
    }
}
