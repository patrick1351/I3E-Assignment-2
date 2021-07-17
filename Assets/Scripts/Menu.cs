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
    public void Exit()
    {
        Debug.Log("quiting");
        Application.Quit();
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

}
