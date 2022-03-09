using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatuses : MonoBehaviour
{
    private Enemy enemy;
    private float enemyMoveSpeedPrimal;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyMoveSpeedPrimal = enemy.MoveSpeed;
    }

    public void Slowed(bool isSlowed)
    {
        if (isSlowed) return;
        else
        {
            enemy.MoveSpeed = enemyMoveSpeedPrimal;
        }
    }

    public void Slowed(bool isSlowed, float slowPower)
    {
        if (!isSlowed) return;
        else
        {
            enemy.MoveSpeed = enemyMoveSpeedPrimal - (enemyMoveSpeedPrimal * slowPower);
        }
    }
}
