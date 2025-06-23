using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Create New Dialogue")]
public class SODialogue : ScriptableObject
{
	[System.Serializable]
	public class Dialogue
	{
		public int id;
		public string name;
		[TextArea(3, 10)]
		public string message;
		public GameObject objectReference;
	}

	public List<Dialogue> dialogues = new List<Dialogue>();
}
