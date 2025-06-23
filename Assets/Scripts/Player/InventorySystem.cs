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

	void Start()
	{
		// 	inventorySpace1.sprite = null;
		// 	inventorySpace2.sprite = null;
		inventorySpace1.gameObject.SetActive(false);
		inventorySpace2.gameObject.SetActive(false);
	}

	public void SetInventory()
	{
		//Cuando cambiemos de escena, hay que rellenar el inventario
		int savedID1 = PlayerPrefs.GetInt(inventorySpace1.name, -1);
		int savedID2 = PlayerPrefs.GetInt(inventorySpace2.name, -1);

		SODialogue[] allSO = Resources.LoadAll<SODialogue>("Dialogues");

		// if (savedID1 != -1)
		// {
		// 	var
		// }
	}

	public void AddToInventory(GameObject inventoryObject)
	{

		InteractionObject newObject = inventoryObject.GetComponent<InteractionObject>();

		//Obtain the image reference
		Sprite imageReference = inventoryObject.GetComponent<SpriteRenderer>().sprite;

		//Obtain unique id
		int objectID = newObject.GetSO().dialogues[0].id;

		//Show object in the inventory
		if (!inventorySpace1.gameObject.activeInHierarchy)
		{
			inventorySpace1.sprite = imageReference;
			inventorySpace1.gameObject.SetActive(true);
			PlayerPrefs.SetInt(inventorySpace1.name, objectID);
		}
		else if (!inventorySpace2.gameObject.activeInHierarchy)
		{
			inventorySpace2.sprite = imageReference;
			inventorySpace2.gameObject.SetActive(true);
			PlayerPrefs.SetInt(inventorySpace2.name, objectID);

		}

		//Save the current object
		if (inventoryObjectReference1 == null) inventoryObjectReference1 = inventoryObject;
		if (inventoryObjectReference2 == null) inventoryObjectReference2 = inventoryObject;

		Destroy(inventoryObject);
		PlayerPrefs.Save();
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
