using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireHeal : MonoBehaviour
{
    private PlayerController player;
    private bool canHeal = false;
    private float healTimer;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        healTimer = .2f;
    }

    private void Update()
    {
        if (canHeal  && player.HpCurrent < player.HpMax)
        {
            healTimer -= Time.deltaTime;

            if (healTimer <= 0f)
            {
                int heal = player.HpMax / 100;

                if (heal == 0) heal = 1;

                player.HpCurrent += heal;
                Debug.Log("healed player for " + heal + " hp");
                healTimer = .2f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canHeal = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canHeal = false;
    }
}
