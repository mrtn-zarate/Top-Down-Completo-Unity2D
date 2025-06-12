using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject transitionPanel;
    void Start()
    {
        transitionPanel.SetActive(false);
    }
    public void mainmenu_button()
    {
        transitionPanel.SetActive(true);
        Invoke("gotomainmenu", 1f);
    }
    public void credits_button()
    {
        transitionPanel.SetActive(true);
        Invoke("gotocredits", 1f);
    }
    public void startthegame_button()
    {
        transitionPanel.SetActive(true);
        Invoke("gotonextlevel", 1f);
    }
    void gotonextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void gotomainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void gotocredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void quitgame()
    {
        Application.Quit();
    }
}
