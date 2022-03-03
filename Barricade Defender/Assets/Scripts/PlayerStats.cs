using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int HpMax;
    public int HpCurrent;

    public int TakeDamage(int damage)
    {
        return HpCurrent -= damage;
    }

}
