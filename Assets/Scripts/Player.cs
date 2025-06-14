using System.Collections;
using UnityEngine;

/// <summary>
/// Component used to control the player and all the basic behaviours
/// </summary>
[RequireComponent(typeof(LifeSystem))]
public class Player : MonoBehaviour
{
	#region Public variables
	[Header("Movement Parameters")]
	[SerializeField] private float moveVelocity;
	[SerializeField] private float interactionRadius;
	[SerializeField] private LayerMask collisionLayer;

	[Header("Attack Parameters")]
	[SerializeField] private Transform attackPoint;
	[SerializeField] private float radioAttack;
	[SerializeField] private float attackDamage;
	[SerializeField] private LayerMask layerToAttack;
	#endregion

	#region Private variables
	private Animator anim;
	private Collider2D nextTile;
	private Vector3 destination;
	private Vector3 interactionPoint;
	private Vector3 lastInput;
	private float inputH;
	private float inputV;
	private bool inputAttack;
	private bool isMoving = false;
	#endregion

	void Start()
	{
		anim = this.GetComponentInChildren<Animator>();
	}

	void Update()
	{
		ReadingInputs();
		MovementsAndAnimations();
		AttackSystem();
	}

	private void AttackSystem()
	{
		if (inputAttack)
		{
			anim.SetTrigger("isAttacking");

			Collider2D[] colliders = Physics2D.OverlapCircleAll(interactionPoint, radioAttack, layerToAttack);

			foreach (Collider2D item in colliders)
			{
				LifeSystem lf = item.gameObject.GetComponent<LifeSystem>();
				lf.ReceiveDamage(attackDamage);
			}
		}
	}

	private void MovementsAndAnimations()
	{
		if (!isMoving && (inputH != 0 || inputV != 0))
		{
			anim.SetBool("isRunning", true);
			anim.SetFloat("inputH", inputH);
			anim.SetFloat("inputV", inputV);

			lastInput = new Vector3(inputH, inputV, 0);
			destination = this.transform.position + lastInput;
			interactionPoint = destination;

			nextTile = CheckNextTile();
			if (!nextTile)
			{
				StartCoroutine(Move());
			}
		}
		else if (inputH == 0 && inputV == 0)
		{
			anim.SetBool("isRunning", false);
		}
	}

	private void ReadingInputs()
	{
		if (inputV == 0)
			inputH = Input.GetAxisRaw("Horizontal");
		if (inputH == 0)
			inputV = Input.GetAxisRaw("Vertical");

		inputAttack = Input.GetButtonDown("Attack");
	}

	#region Coroutines
	/// <summary>
	/// Player movement using a coroutine, this will now updated until complete the destination route
	/// </summary>
	/// <returns></returns>
	private IEnumerator Move()
	{
		isMoving = true;

		while (this.transform.position != destination)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, destination, moveVelocity);
			yield return null;
		}

		interactionPoint = this.transform.position + lastInput;
		isMoving = false;
	}
	#endregion

	private Collider2D CheckNextTile()
	{
		return Physics2D.OverlapCircle(interactionPoint, interactionRadius, collisionLayer);
	}

	#region Testing
	private void OnDrawGizmos()
	{
		// Gizmos.DrawSphere(interactionPoint, interactionRadius);
		Gizmos.DrawSphere(interactionPoint, radioAttack);
	}
	#endregion
}
