using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base component used to control all basic behaviour of enemies
/// </summary>
[RequireComponent(typeof(Animator), typeof(LifeSystem))]
public class EnemyBase : MonoBehaviour
{

	#region Public variables
	[SerializeField] private float walkVelocity = 1;
	[SerializeField] private float attackDamage = 10;
	[SerializeField] private Transform[] waypoints;
	#endregion

	#region Private variables
	private Vector3 currentDestination;
	private int currentIndex;
	private bool isWalking;
	private bool isDead = false;

	private Animator anim;
	private LifeSystem lifeSystem;
	#endregion

	void Awake()
	{
		anim = this.GetComponent<Animator>();
		lifeSystem = this.GetComponent<LifeSystem>();
	}

	void Start()
	{
		// lifeSystem.OnReceiveDamage.AddListener((value) => ReceiveDamage());
		// lifeSystem.OnDeath.AddListener(() => Dead());

		// currentDestination = waypoints[currentIndex].position;
		// FocusToDestination();
	}

	private void OnEnable()
	{
		lifeSystem.OnReceiveDamage.AddListener((value) => ReceiveDamage());
		lifeSystem.OnDeath.AddListener(() => Dead());

		currentDestination = waypoints[currentIndex].position;
		FocusToDestination();

		StartCoroutine(Patrol());
	}

	#region Private Methods

	private void ReceiveDamage()
	{
		anim.SetTrigger("hit");
	}

	private void Dead()
	{
		anim.SetTrigger("isDead");
		isDead = true;
	}

	private void SetNewDestination()
	{
		currentIndex++;
		if (currentIndex >= waypoints.Length) currentIndex = 0;

		currentDestination = waypoints[currentIndex].position;
		FocusToDestination();
	}

	protected void SetNewDestination(Vector3 toPosition)
	{
		currentDestination = toPosition;
		FocusToDestination();
	}

	private void FocusToDestination()
	{
		if (currentDestination.x > this.transform.position.x)
		{
			this.transform.localScale = Vector3.one;
		}
		else
		{
			this.transform.localScale = new Vector3(-1, 1, 1);
		}
	}
	#endregion

	#region Triggers
	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerDetection"))
		{
			//Corremos hacia el jugador
			Debug.Log("Player Detectado");
		}
		else if (collision.gameObject.CompareTag("PlayerHitbox"))
		{
			// Debug.Log("Player Atacado");
			LifeSystem lf = collision.gameObject.GetComponent<LifeSystem>();
			lf.ReceiveDamage(attackDamage);
		}
	}
	#endregion

	#region Coroutines
	private IEnumerator Patrol()
	{
		while (!isDead)
		{
			while ((this.transform.position != currentDestination) && !isDead)
			{
				anim.SetBool("isWalking", true);

				this.transform.position = Vector3.MoveTowards(
					this.transform.position,
					currentDestination,
					walkVelocity * Time.deltaTime
				);
				yield return null;
			}

			SetNewDestination();
		}

		yield return new WaitForSeconds(0.5f);
		Destroy(this.transform.parent.gameObject);
	}

	#endregion
}
