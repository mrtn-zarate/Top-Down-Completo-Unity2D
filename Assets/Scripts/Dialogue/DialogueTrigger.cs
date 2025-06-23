using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public DialogueLine[] dialogueLines;

	private bool playerInRange = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHitbox"))
		{
			collision.GetComponent<Player>().StartDialogue(dialogueLines);
		}
	}
}
