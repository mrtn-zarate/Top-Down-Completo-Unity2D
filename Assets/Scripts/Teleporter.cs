using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

	[Header("Scene name reference")]
	[SerializeField] private GlobalData.SceneName toScene;

	[Header("Next player position")]
	[SerializeField] private Vector3 newPlayerPosition;
	[SerializeField] private Vector2 newPlayerRotation;
	
	private SceneChanger sceneChanger;

	void Awake()
	{
		sceneChanger = FindObjectOfType<SceneChanger>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHitbox"))
		{
			Debug.Log("El player ha llegado");
			GlobalData.playerStartPosition = newPlayerPosition;
			GlobalData.PlayerStartRotation = newPlayerRotation;

			sceneChanger.LoadSceneAsync((int)toScene);
		}
	}
}
