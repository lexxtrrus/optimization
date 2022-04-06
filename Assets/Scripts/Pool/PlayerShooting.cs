using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public bool spreadShot = false;

	[Header("General")]
	public Transform gunBarrel;
	public ParticleSystem shotVFX;
	public AudioSource shotAudio;
	public float fireRate = .1f;
	public int spreadAmount = 20;

	[Header("Bullets")]
	public GameObject bulletPrefab;

	float timer;
	private Queue<ProjectileBehaviour> pool = new Queue<ProjectileBehaviour>();
	private List<ProjectileBehaviour> activeBullets = new List<ProjectileBehaviour>();
	private ProjectileBehaviour projectile;


	IEnumerator Start()
	{
		GameObject bullet = new GameObject();
		
		for (int i = 0; i < 1000; i++)
		{
			bullet = Instantiate(bulletPrefab, transform);
			pool.Enqueue(bullet.GetComponent<ProjectileBehaviour>().Init(this));
			bullet.SetActive(false);

			if (i % 50 == 0) yield return null;
		}
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= fireRate)
		{
			Vector3 rotation = gunBarrel.rotation.eulerAngles;
			rotation.x = 0f;

			if (spreadShot)
				SpawnBulletSpread(rotation);
			else
				SpawnBullet(rotation);
			

			timer = 0f;

			if (shotVFX)
				shotVFX.Play();

			if (shotAudio)
				shotAudio.Play();
		}

		for (int i = 0; i < activeBullets.Count; i++)
		{
			if(activeBullets[i]) activeBullets[i].Tick();
		}
	}

	void SpawnBullet(Vector3 rotation)
	{
		projectile = pool.Dequeue();

		projectile.transform.position = gunBarrel.position;
		projectile.transform.rotation = Quaternion.Euler(rotation);
		projectile.gameObject.SetActive(true);
		
		activeBullets.Add(projectile);
	}

	void SpawnBulletSpread(Vector3 rotation)
	{
		int max = spreadAmount / 2;
		int min = -max;

		Vector3 tempRot = rotation;
		for (int x = min; x < max; x++)
		{
			tempRot.x = (rotation.x + 3 * x) % 360;

			for (int y = min; y < max; y++)
			{
				tempRot.y = (rotation.y + 3 * y) % 360;

				projectile = pool.Dequeue();

				projectile.transform.position = gunBarrel.position;
				projectile.transform.rotation = Quaternion.Euler(tempRot);
				projectile.gameObject.SetActive(true);
				
				activeBullets.Add(projectile);
			}
		}
	}

	public void BackToPool(ProjectileBehaviour bullet)
	{
		pool.Enqueue(bullet);
		activeBullets.Remove(bullet);
		bullet.gameObject.SetActive(false);
	}
}

