using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHp;
    public int CurrentHp;
    public int Damage;

    [SerializeField]
    protected int moveSpeed;
}
