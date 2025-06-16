using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

	[SerializeField] private GlobalData.SceneName toScene;
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

			sceneChanger.LoadSceneAsync((int)toScene);
		}
	}
}
