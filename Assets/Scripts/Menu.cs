/******************************************************************************
Author: Patrick
Name of Class: I3E
Description of Class:
Date Created: 16/7/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject options;
    public GameObject howtoplay;
    public GameObject credits;

    public void Exit()
    {
        Debug.Log("quiting");
        Application.Quit();
    }
    public void StartGame()
    {
        Debug.Log("startgame");
        SceneManager.LoadScene(1);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);
    }
    public void Howtoplay()
    {
        mainmenu.SetActive(false);
        howtoplay.SetActive(true);
    }
    public void Credits()
    {
        mainmenu.SetActive(false);
        credits.SetActive(true);
    }
    public void Back()
    {
        options.SetActive(false);
        howtoplay.SetActive(false);
        credits.SetActive(false);
        mainmenu.SetActive(true);
    }
}
