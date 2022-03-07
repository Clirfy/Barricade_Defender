using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTest : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float attackDelay;

    private float attackTimer;

    private void Update()
    {
        if (Time.time >= attackTimer)
        {
            attackTimer = Time.time + attackDelay;
            Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        }
    }
}
