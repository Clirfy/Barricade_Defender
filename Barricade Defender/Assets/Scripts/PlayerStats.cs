using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int HpMax;
    public int HpCurrent;

    protected int spCurrent; //shield points
    protected int spMax = 20;


    public int TakeDamage(int damage)
    {
        if (spCurrent > damage)
        {
            return spCurrent -= damage;
        }

        else if (spCurrent > 0 && spCurrent < damage)
        {
            int dmgUnblocked = damage - spCurrent;
            spCurrent = 0;
            return HpCurrent -= dmgUnblocked;
        }
        else
        {
            return HpCurrent -= damage;
        }
    }
}
