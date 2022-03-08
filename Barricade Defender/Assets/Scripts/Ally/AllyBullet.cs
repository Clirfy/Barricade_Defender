using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBullet : MonoBehaviour
{
	[HideInInspector]
	public int Damage;
	[HideInInspector]
	public GameObject TargetPos;
	public float speed = 10;

	protected Vector3 startPos;

	protected void Start()
	{
		startPos = transform.position;
	}

	protected void Update()
	{
		if (TargetPos != null)
		{
			UpdateArrowPos();
			TargetHit();
		}

		else
		{
			Destroy(gameObject);
		}
	}

	protected virtual void UpdateArrowPos()
	{
		//implement in inherited classes
	}

	protected void TargetHit()
	{
		if (Vector2.Distance(transform.position, TargetPos.transform.position) <= .2f)
		{
			TargetPos.GetComponent<Enemy>().TakeDamage(Damage);
			Destroy(gameObject);
		}
	}

	// This is a 2D version of Quaternion.LookAt; it returns a quaternion
	// that makes the local +X axis point in the given forward direction.
	// 
	// forward direction
	// Quaternion that rotates +X to align with forward
	protected static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}
}
