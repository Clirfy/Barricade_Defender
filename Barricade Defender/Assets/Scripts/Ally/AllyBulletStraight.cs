using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBulletStraight : AllyBullet
{
    protected override void UpdateArrowPos()
    {
        base.UpdateArrowPos();

		Vector3 nextPos = Vector3.MoveTowards(transform.position, TargetPos.transform.position, speed * Time.deltaTime);

		// Rotate to face the next position, and then move there
		transform.rotation = LookAt2D(nextPos - transform.position);
		transform.position = nextPos;
	}
}
