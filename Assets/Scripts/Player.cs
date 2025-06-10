using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region Public variables
	[SerializeField] private float moveVelocity;
	[SerializeField] private float interactionRadius;
	[SerializeField] private LayerMask collisionLayer;
	#endregion

	#region Private variables
	private Collider2D nextTile;
	private Vector3 destination;
	private Vector3 interactionPoint;
	private Vector3 lastInput;
	private float inputH;
	private float inputV;
	private bool isMoving = false;
	#endregion

	void Update()
	{
		if(inputV == 0)
		inputH = Input.GetAxisRaw("Horizontal");
		if(inputH == 0)
		inputV = Input.GetAxisRaw("Vertical");

		if (!isMoving && (inputH != 0 || inputV != 0))
		{
			lastInput = new Vector3(inputH, inputV, 0);
			destination = this.transform.position + lastInput;
			interactionPoint = destination;

			nextTile = CheckNextTile();
			if (!nextTile)
			{
				StartCoroutine(Move());
			}
		}
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
		Gizmos.DrawSphere(interactionPoint, interactionRadius);
	}
	#endregion
}
