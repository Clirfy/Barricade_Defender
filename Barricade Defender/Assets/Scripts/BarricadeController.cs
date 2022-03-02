using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    public int MaxHp;
    public int CurrentHp;

    private void Start()
    {
        CurrentHp = MaxHp;
    }

    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if(CurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int TakeDamage(int damage)
    {
        return CurrentHp -= damage;
    }
}
