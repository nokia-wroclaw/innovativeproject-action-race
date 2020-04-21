using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD_manager : MonoBehaviour
{
    public GameObject Infopanel;
    public Text timerLabel;
    public Text pointsLabel;
    public float Timer = 60;

    void Update()
    {
        //Timer -= Time.deltaTime;
        ////show timer on UI
        //if (Timer <= 0)
        //{
        //    //endgame
        //    Timer = 60;
        //}

        ////update the label value
        //timerLabel.text = "" + (int)Timer + " s";
    }

    public void UpdatePoints(int redTeamScore, int blueTeamScore)
    {
        pointsLabel.text = "" + redTeamScore + " : " + blueTeamScore;
    }

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
