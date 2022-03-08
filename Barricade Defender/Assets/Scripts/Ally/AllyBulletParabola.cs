using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBulletParabola : AllyBullet
{
    [SerializeField]
    private float arcHeight;

	protected override void UpdateArrowPos()
	{
		// Compute the next position, with arc added in
		float x0 = startPos.x;
		float x1 = TargetPos.transform.position.x;
		float dist = x1 - x0;
		float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
		float baseY = Mathf.Lerp(startPos.y, TargetPos.transform.position.y, (nextX - x0) / dist);
		float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
		var nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

		// Rotate to face the next position, and then move there
		transform.rotation = LookAt2D(nextPos - transform.position);
		transform.position = nextPos;
	}
}
