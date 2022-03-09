using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyDruidSkill : MonoBehaviour
{
    [HideInInspector]
    public float SlowPower;

    private List<Collider2D> targets = new List<Collider2D>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Add(collision);
            collision.GetComponent<EnemyStatuses>().Slowed(true, SlowPower);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStatuses>().Slowed(false);
        }
    }

    private void OnDestroy()
    {
        foreach (var item in targets)
        {
            if (item != null)
            {
                item.GetComponent<EnemyStatuses>().Slowed(false);
            }
        }
    }
}
