using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
	[Header("Inventory List")]
	[SerializeField] private Image inventorySpace1;
	[SerializeField] private Image inventorySpace2;

	private GameObject inventoryObjectReference1;
	private GameObject inventoryObjectReference2;
	private Dictionary<string, GameObject> listOfLampsRef = new Dictionary<string, GameObject>();

	void Awake()
	{
		inventorySpace1.gameObject.SetActive(false);
		inventorySpace2.gameObject.SetActive(false);
	}

	void Start()
	{
		// 	inventorySpace1.sprite = null;
		// 	inventorySpace2.sprite = null;
	}

	public void SetInventory()
	{
		//Cuando cambiemos de escena, hay que rellenar el inventario
		// int savedID1 = PlayerPrefs.GetInt(inventorySpace1.name, -1);
		// int savedID2 = PlayerPrefs.GetInt(inventorySpace2.name, -1);

		// if (savedID1 != -1)
		// {
		// 	var
		// }

		Debug.Log("Set inventory data");
		GameObject[] allLamps = Resources.LoadAll<GameObject>("Lamps");

		foreach (GameObject lamp in allLamps)
		{
			var lampData = lamp.GetComponent<InteractionObject>();
			Debug.Log("Check this lamp: " + lampData.name);

			foreach (var savedData in GlobalData.listOfLamps)
			{
				if (lampData.GetLampId() == savedData)
				{
					Debug.Log("Add to inventory uwu");
					AddToInventory(lamp);
				}
			}
		}
	}

	public void AddToInventory(GameObject inventoryObject)
	{

		InteractionObject newObject = inventoryObject.GetComponent<InteractionObject>();
		Debug.Log("Ref interaction object: " + newObject.name);

		//Obtain the image reference
		Sprite imageReference = inventoryObject.GetComponent<SpriteRenderer>().sprite;
		Debug.Log("sprite interaction object: " + imageReference.name);

		//Obtain unique id
		int objectID = newObject.GetSO().dialogues[0].id;

		GameObject currentSpaceSelection = null;

		//Show object in the inventory
		if (!inventorySpace1.gameObject.activeInHierarchy)
		{
			Debug.Log("inventory space 1 available: ");
			inventorySpace1.sprite = imageReference;
			inventorySpace1.gameObject.SetActive(true);
			PlayerPrefs.SetInt(inventorySpace1.name, objectID);
			currentSpaceSelection = inventorySpace1.gameObject;
		}
		else if (!inventorySpace2.gameObject.activeInHierarchy)
		{
			Debug.Log("inventory space 1 available: ");
			inventorySpace2.sprite = imageReference;
			inventorySpace2.gameObject.SetActive(true);
			PlayerPrefs.SetInt(inventorySpace2.name, objectID);
			currentSpaceSelection = inventorySpace2.gameObject;

		}

		//Save the current object
		if (inventoryObjectReference1 == null) inventoryObjectReference1 = inventoryObject;
		if (inventoryObjectReference2 == null) inventoryObjectReference2 = inventoryObject;

		// Destroy(inventoryObject);
		PlayerPrefs.Save();

		if (currentSpaceSelection != null) {
			listOfLampsRef.Add(inventoryObject.name, currentSpaceSelection);
		}
	}

	public void RemoveFromInventory(GameObject inventoryObject)
	{
		foreach (var obj in listOfLampsRef)
		{
			if (obj.Key == inventoryObject.name)
			{
				Image spaceComponent = obj.Value.GetComponent<Image>();
				spaceComponent.sprite = null;
				spaceComponent.gameObject.SetActive(false);

				

				listOfLampsRef.Remove(obj.Key);
			}
		}
	}

	public GameObject GetLampFromInventory()
	{
		//Check if any of camps of inventory has a value
		if (inventorySpace1.gameObject.activeInHierarchy)
		{
			Debug.Log("El primer espacio tiene algho, se lo woa pasar");
			inventorySpace1.gameObject.SetActive(false);

			Debug.Log(inventoryObjectReference1.name);
			return inventoryObjectReference1;
		}
		else if (inventorySpace2.gameObject.activeInHierarchy)
		{
			inventorySpace2.gameObject.SetActive(false);
			return inventoryObjectReference2;
		}

		Debug.Log("Player does not have a lamp right now");
		return null;
	}
}
