using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator startGameContentPanel;
    public Animator settingsButton;

    // function for starting the actual game
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // function for sliding startGameContentPanel
    public void showGameModes()
    {
        bool is_StartGameContentPanelDown = startGameContentPanel.GetBool("contentPanelDown");
        startGameContentPanel.SetBool("contentPanelDown", !is_StartGameContentPanelDown);
        settingsButton.SetBool("slideDown", !is_StartGameContentPanelDown);

        if (GameObject.Find("StartButton").GetComponentInChildren<Text>().text == "Start Game")
            GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "Choose map - number of players";
        else
            GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "Start Game";
    }
    
}
