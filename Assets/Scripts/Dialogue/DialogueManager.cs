using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance;

	[SerializeField] private GameObject dialogueBox;
	[SerializeField] private TextMeshProUGUI speakerText;
	[SerializeField] private TextMeshProUGUI dialogueText;

	private Queue<DialogueLine> lines = new Queue<DialogueLine>();
	private bool isDialogueActive = false;
	[HideInInspector] public UnityEvent OnFinishDialogue = new UnityEvent();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		dialogueBox.SetActive(false);
	}

	private void Update()
	{
		if (isDialogueActive && Input.GetButtonDown("Attack"))
		{
			Debug.Log("Nueva linea");
			Debug.Log("total de dialogues: " + lines.Count);
			ShowNextLine();
		}
	}

	public void StartDialogue(DialogueLine[] dialogueLines)
	{
		dialogueBox.SetActive(true);
		lines.Clear();
		foreach (DialogueLine line in dialogueLines)
		{
			lines.Enqueue(line);
		}

		dialogueBox.SetActive(true);
		isDialogueActive = true;
		ShowNextLine();
	}

	private void ShowNextLine()
	{
		if (lines.Count == 0)
		{
			EndDialogue();
			return;
		}

		Debug.Log("Aqui te va la nueva linea ---");
		DialogueLine currentLine = lines.Dequeue();
		speakerText.text = currentLine.speaker;
		dialogueText.text = currentLine.text;
	}

	public void EndDialogue()
	{
		dialogueBox.SetActive(false);
		isDialogueActive = false;
		OnFinishDialogue?.Invoke();
	}

}
