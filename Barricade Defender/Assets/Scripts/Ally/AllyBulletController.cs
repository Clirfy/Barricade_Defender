using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBulletController : MonoBehaviour
{
	public int Damage;
	public GameObject TargetPos;
	public float speed = 10;

	[SerializeField]
	private float arcHeight = 1;
	private Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
	}

	void Update()
	{
		if (TargetPos != null)
		{
			UpdateArrowPos();
		}

		else
		{
			Destroy(gameObject);
		}
	}

	private void UpdateArrowPos()
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

	// This is a 2D version of Quaternion.LookAt; it returns a quaternion
	// that makes the local +X axis point in the given forward direction.
	// 
	// forward direction
	// Quaternion that rotates +X to align with forward
	static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<Enemy>().TakeDamage(Damage);
			Destroy(gameObject);
		}
	}
}
