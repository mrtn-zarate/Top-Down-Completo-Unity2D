using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

	[SerializeField] private GlobalData.SceneName toScene;


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHitbox"))
		{
			
		}
	}
}
