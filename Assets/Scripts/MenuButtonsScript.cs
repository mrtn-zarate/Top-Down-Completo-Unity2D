using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject transitionPanel;

    private SceneChanger sceneChanger;

	void Awake()
	{
		sceneChanger = FindAnyObjectByType<SceneChanger>();
	}

	void Start()
    {
        transitionPanel.SetActive(false);
    }
    public void mainmenu_button()
    {
        transitionPanel.SetActive(true);
        Invoke("gotomainmenu", 1f);
        // gotomainmenu();
    }
    public void credits_button()
    {
        transitionPanel.SetActive(true);
        Invoke("gotocredits", 1f);
        // gotocredits();
    }
    public void startthegame_button()
    {
        transitionPanel.SetActive(true);
        Invoke("gotonextlevel", 1f);
        // gotonextlevel();
    }
    void gotonextlevel()
    {
        // sceneChanger.LoadSceneAsync((int)GlobalData.SceneName.Overworld);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene((int)GlobalData.SceneName.Overworld);
    }
    public void gotomainmenu()
    {

        // sceneChanger.LoadSceneAsync((int)GlobalData.SceneName.MainMenu);
        // SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene((int)GlobalData.SceneName.MainMenu);
    }
    public void gotocredits()
    {
        // sceneChanger.LoadSceneAsync((int)GlobalData.SceneName.Credits);
        // SceneManager.LoadScene("Credits");
        SceneManager.LoadScene((int)GlobalData.SceneName.Credits);
    }
    public void quitgame()
    {
        Application.Quit();
    }
}
