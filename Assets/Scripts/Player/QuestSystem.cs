using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
	[Header("Lamp reference")]
	[SerializeField] private GameObject finalLampPosition;
	[SerializeField] private int LampRef;
	private bool isPilarReady;

	void Start()
	{
		if (!GlobalData.listOfQuest.Contains(this.gameObject.name)) return;

		GameObject[] allLamps = Resources.LoadAll<GameObject>("Lamps");		

		foreach (GameObject lamp in allLamps)
		{
			var lampData = lamp.GetComponent<InteractionObject>();
			Debug.Log("Check this lamp: " + lampData.name);

			foreach (var savedData in GlobalData.listOfLamps)
			{
				if (lampData.GetLampId() == savedData && lampData.GetLampId() == LampRef)
				{
					Debug.Log("Add finalquest");
					// AddToInventory(lamp);
					PutLamp(lamp);
				}
			}
		}
	}

	public void PutLamp(GameObject lampObject)
	{
		if (lampObject == null) return;
		Debug.Log(lampObject.name);
		var newLamp = Instantiate(lampObject, finalLampPosition.transform.position, Quaternion.identity, finalLampPosition.transform);
		Debug.Log(newLamp.name);
		Debug.Log(newLamp.gameObject.activeSelf);
		StartCoroutine(corutined(newLamp));

		newLamp.gameObject.SetActive(true);
		Debug.Log(newLamp.gameObject.activeSelf);
		Debug.Log("LAMPARA PUESTAAAA");
		isPilarReady = true;

		GlobalData.listOfQuest.Add(this.gameObject.name);
	}

	//used to tunr on the lamp when fininsh to instantiate
	private IEnumerator corutined(GameObject newObject)
	{
		yield return new WaitForSeconds(0.1f);
		newObject.gameObject.SetActive(true);
	}

	public bool IsPilarReady() => isPilarReady;

}
