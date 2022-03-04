using UnityEngine;
using UnityEngine.UI;

public class EnemyMelee : Enemy
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        isMoving = false;

        if (collision.gameObject.CompareTag("CampfireBase"))
        {
            target = collision.gameObject;
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (target == null)
        {
            attackTimer = Time.time + attackDelay;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isMoving = true;
        target = null;
        isAttacking = false;

        animator.SetBool("IsAttacking", false);
    }
}
