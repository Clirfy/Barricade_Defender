using UnityEngine;

public class BaseCampfire : MonoBehaviour
{
    public int HpMax;
    public int Money;

    private int HpCurrent;

    private void Start()
    {
        HpCurrent = HpMax;
    }

    public int GetMoney(int ammount)
    {
        return Money += ammount;
    }

    public int TakeMoney(int ammount)
    {
        if (ammount < Money)
        {
            return Money -= ammount;
        }
        else
        {
            Debug.LogWarning("unable to spend money; insufficent money");
            return 0;
        }
    }
}
