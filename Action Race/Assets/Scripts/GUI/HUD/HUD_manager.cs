using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_manager : MonoBehaviour
{
    public GameObject Infopanel;

    public void exitToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene",LoadSceneMode.Single);    
    }

    public void openInfoPanel()
    {
        if (Infopanel != null)
        {
            Infopanel.SetActive(true);
        }
        else
        {
            System.Console.WriteLine("panel to open is null");
        }
    }

    public void closeInfoPanel()
    {
        if (Infopanel != null)
        {
            Infopanel.SetActive(false);
        }
        else
        {
            System.Console.WriteLine("panel to close is null");
        }
    }
}
