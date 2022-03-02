using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHp;
    public int CurrentHp;

    public int TakeDamage(int damage)
    {
        return CurrentHp -= damage;
    }

}
