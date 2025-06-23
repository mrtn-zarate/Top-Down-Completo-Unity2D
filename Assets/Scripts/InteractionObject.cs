using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{

	[SerializeField] private int idLamp;
	[SerializeField] private GameObject objectReference;
	[SerializeField] private SODialogue currentDialogue;

	void Start()
	{
		foreach (int value in GlobalData.listOfLamps)
		{
			if (value == idLamp) this.gameObject.SetActive(false);
		}
	}
	public SODialogue GetSO()
	{
		return currentDialogue;
	}

	public int GetLampId() => idLamp;

	public void GetLamp()
	{
		GlobalData.listOfLamps.Add(idLamp);
		this.gameObject.SetActive(false);
	}
}
