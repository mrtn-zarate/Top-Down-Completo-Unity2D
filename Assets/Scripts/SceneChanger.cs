using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Global component used to control the async load scene 
/// </summary>
public class SceneChanger : MonoBehaviour
{

	public static SceneChanger Instance { get; private set; }
	public UnityEvent OnSceneLoadComplete = new UnityEvent();

	// [SerializeField] private GameObject transitionPanel;
	// [SerializeField] private List<Animator> animTransition;

	void Awake()
	{
		if (Instance != null)
		{
			Debug.Log("Este controlador de escenas ya existe, cudao pa >:v");
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		// transitionPanel.SetActive(false);
	}

	public void LoadSceneAsync(string sceneName)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	public void LoadSceneAsync(int sceneNumber)
	{
		StartCoroutine(LoadSceneCoroutine(sceneNumber));
	}

	private IEnumerator LoadSceneCoroutine(string sceneName)
	{
		// transitionPanel.SetActive(true);
		// foreach (Animator anim in animTransition)
		// {
		// 	anim.SetBool("isEntrance", true);
		// 	anim.SetTrigger("transition");	
		// }

													// yield return new WaitForSeconds(2f);

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncOperation.isDone)
		{
			Debug.Log($"Viajando a la siguiente escena {asyncOperation.progress}%");
			yield return null;
		}

		// foreach (Animator anim in animTransition)
		// {
		// 	anim.SetBool("isEntrance", false);
		// 	anim.SetTrigger("transition");
		// }
												// yield return new WaitForSeconds(2f);
		// transitionPanel.SetActive(false);
		OnSceneLoadComplete?.Invoke();
	}

	private IEnumerator LoadSceneCoroutine(int sceneNumber)
	{
		// transitionPanel.SetActive(true);
		// foreach (Animator anim in animTransition)
		// {
		// 	anim.SetBool("isEntrance", true);
		// 	anim.SetTrigger("transition");	
		// }
														// yield return new WaitForSeconds(2f);

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber);
		while (!asyncOperation.isDone)
		{
			Debug.Log($"Viajando a la siguiente escena {asyncOperation.progress}%");
			yield return null;
		}

		// foreach (Animator anim in animTransition)
		// {
		// 	anim.SetBool("isEntrance", false);
		// 	anim.SetTrigger("transition");
		// }
														// yield return new WaitForSeconds(2f);
		// transitionPanel.SetActive(false);
		OnSceneLoadComplete?.Invoke();
	}
}
