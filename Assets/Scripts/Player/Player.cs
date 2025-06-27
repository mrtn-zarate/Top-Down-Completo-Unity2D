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

	[Header("Talk parameters")]
	[SerializeField] private Animator animTalk;
	#endregion

	#region Private variables
	private LifeSystem lifeSystem;
	private InventorySystem inventorySystem;
	private DialogueManager dialogueManager;
	private PlayerLifeCanvas playerLifeCanvas;
	private Animator anim;
	private Collider2D nextTile;
	private Vector3 destination;
	private Vector3 interactionPoint;
	private Vector3 lastInput;
	private float inputH;
	private float inputV;
	private bool inputAttack;
	private bool isInteracting = false;
	private bool isDialogue = false;
	private bool isDialogueControl = false;
	private bool isMoving = false;
	private bool isDead = false;
	private bool canMove = false;
	private GameObject currentObject = null;
	private QuestSystem currentQuestSystem = null;
	private DialogueLine[] currentDialogue = null;
	#endregion

	void Awake()
	{
		anim = this.GetComponentInChildren<Animator>();
		lifeSystem = this.GetComponent<LifeSystem>();
		inventorySystem = this.GetComponent<InventorySystem>();
		dialogueManager = this.GetComponentInChildren<DialogueManager>();
		playerLifeCanvas = this.GetComponentInChildren<PlayerLifeCanvas>();
	}

	void Start()
	{
		lifeSystem.OnReceiveDamage.AddListener((value) => ReceiveDamage());
		lifeSystem.OnDeath.AddListener(() => Dead());
		dialogueManager.OnFinishDialogue.AddListener(() => FinishDialogue());

		inventorySystem.SetInventory();
		lifeSystem.CheckCurrentLife();
		playerLifeCanvas.SetVisualLife(lifeSystem.GetCurrentLife());
		animTalk.gameObject.SetActive(false);

		//Move player to last entrance position saved
		this.transform.position = GlobalData.playerStartPosition;
		this.inputH = GlobalData.PlayerStartRotation.x;
		this.inputV = GlobalData.PlayerStartRotation.y;

		StartCoroutine(WaitForASecToMove());
	}

	void Update()
	{
		if (!canMove) return;

		if (!isDead)
		{
			if (!isDialogueControl)
			{
				ReadingInputs();
				MovementsAndAnimations();
				AttackSystem();
			}
		}
	}

	#region Private Methods

	private void AttackSystem()
	{
		if (inputAttack)
		{

			if (isInteracting)
			{
				if (isDialogue)
				{
					dialogueManager.StartDialogue(currentDialogue);
					isDialogueControl = true;
					return;
				}

				//Triggereo con un quest
				if (currentQuestSystem != null)
				{
					Debug.Log("Poner la lamparaaa");
					currentQuestSystem.PutLamp(inventorySystem.GetLampFromInventory());
					return;
				}

				//Activar mensaje de dialogo

				if (currentObject)
				{
					// Destroy(currentObject);
					inventorySystem.AddToInventory(currentObject);
					currentObject.GetComponent<InteractionObject>().GetLamp();
				}
				return;
			}

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

	private void ReceiveDamage()
	{
		anim.SetTrigger("isHitting");
	}

	private void Dead()
	{
		isDead = true;
		anim.SetTrigger("isDead");
	}

	public void StartDialogue(DialogueLine[] lines)
	{
		currentDialogue = lines;
		isDialogue = true;
	}

	private void FinishDialogue()
	{
		isDialogue = false;
		isDialogueControl = false;
	}

	private Collider2D CheckNextTile()
	{
		return Physics2D.OverlapCircle(interactionPoint, interactionRadius, collisionLayer);
	}
	#endregion

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

	private IEnumerator WaitForASecToMove()
	{
		yield return new WaitForSeconds(0.5f);
		canMove = true;
	}
	#endregion

	#region Triggers
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("InteractionPoint") ||
			collision.CompareTag("InteractionObject") ||
			collision.CompareTag("InteractionPilar"))
		{
			animTalk.gameObject.SetActive(true);
			animTalk.SetBool("isQuestion", true);
			isInteracting = true;

			if (collision.CompareTag("InteractionObject"))
			{
				Debug.Log("Guardar este objeto: " + collision.gameObject.name);
				currentObject = collision.gameObject;
			}

			if (collision.CompareTag("InteractionPilar")) currentQuestSystem = collision.GetComponent<QuestSystem>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("InteractionPoint") ||
			collision.CompareTag("InteractionObject") ||
			collision.CompareTag("InteractionPilar"))
		{
			animTalk.gameObject.SetActive(false);
			animTalk.SetBool("isQuestion", false);
			isInteracting = false;

			if (collision.CompareTag("InteractionObject")) currentObject = null;
			if (collision.CompareTag("InteractionPilar")) currentQuestSystem = null;
		}
	}
	#endregion

	#region Testing
	private void OnDrawGizmos()
	{
		// Gizmos.DrawSphere(interactionPoint, interactionRadius);
		Gizmos.DrawSphere(interactionPoint, radioAttack);
	}
	#endregion
}
