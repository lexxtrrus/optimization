using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
	[Header("Movement")]
	public float speed = 50f;

	private float lifeDuration;
	private PlayerShooting _shooting;
	private Rigidbody _rigidbody;

	private string enemy = "Enemy";
	private string environment = "Environment";

	public ProjectileBehaviour Init(PlayerShooting shooting)
	{
		lifeDuration = 0f;
		_shooting = shooting;
		_rigidbody = GetComponent<Rigidbody>();
		return this;
	}

	public void Tick()
	{
		Vector3 movement = transform.forward * speed * Time.deltaTime;
		_rigidbody.MovePosition(transform.position + movement);

		lifeDuration += Time.deltaTime;
		if (lifeDuration >= 0.5f)
		{
			RemoveProjectile();
		}
	}

	void OnTriggerEnter(Collider theCollider)
	{
		if (theCollider.CompareTag(enemy) || theCollider.CompareTag(environment))
		{
			RemoveProjectile();
		}
	}

	private void RemoveProjectile()
	{
		lifeDuration = 0f;
		_shooting.BackToPool(this);
	}
}
