using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
	[Header("Lamp reference")]
	[SerializeField] private GameObject finalLampPosition;
	private bool isPilarReady;


	public void PutLamp(GameObject lampObject)
	{
		if (lampObject == null) return;
		Instantiate(lampObject, finalLampPosition.transform.position, Quaternion.identity, finalLampPosition.transform);
		Debug.Log("LAMPARA PUESTAAAA");
		isPilarReady = true;
	}

}
