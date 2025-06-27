using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPanel : MonoBehaviour
{
	[SerializeField] private Button restartButton;

	void Start()
	{
		restartButton.onClick.AddListener(() => StartCoroutine(GotOMainMenu()));
	}

	private IEnumerator GotOMainMenu() {
		Time.timeScale = 1; // Pausan't el juego

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int) GlobalData.SceneName.MainMenu);
		while (!asyncOperation.isDone)
		{
			Debug.Log($"Viajando a la siguiente escena {asyncOperation.progress}%");
			yield return null;
		}
	}
}
