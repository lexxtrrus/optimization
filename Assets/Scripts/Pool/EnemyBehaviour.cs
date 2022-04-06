using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehaviour : MonoBehaviour
{
	[Header("Movement")]
	public float speed = 2f;

	[Header("Life Settings")]
	public float enemyHealth = 1f;

	[Header("Bullet Impact")]
	public GameObject bulletHitPrefab;

	private string bullet = "Bullet";
	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (!Settings.IsPlayerDead())
		{
			transform.LookAt(Settings.PlayerPosition);
		}
		Vector3 movement = transform.forward * speed * Time.deltaTime;
		_rigidbody.MovePosition(transform.position + movement);
	}

	void OnTriggerEnter(Collider theCollider)
	{
		if (!theCollider.CompareTag(bullet))
			return;

		enemyHealth--;

		if(enemyHealth <= 0)
		{
			Instantiate(bulletHitPrefab, this.transform);
			Destroy(gameObject,0.1f);
		}
	}

}
