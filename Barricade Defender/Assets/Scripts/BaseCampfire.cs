using UnityEngine;
using UnityEngine.Events;

public class BaseCampfire : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnDeath;
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

        if (HpCurrent <= 0)
        {
            Death();
        }
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

    private void Death()
    {
        OnDeath.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }
}
