using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
	[Header("Life Parameters")]
	[SerializeField] private ParticleSystem heartParticles;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHitbox"))
		{
			collision.GetComponent<LifeSystem>().AddLife(25);
			Instantiate(heartParticles, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
