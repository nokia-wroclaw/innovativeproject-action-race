using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public Animator startGameContentPanel;
    //public Animator settingsButton;

    // function for starting the actual game

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //// function for sliding startGameContentPanel
    //public void showGameModes()
    //{
    //    bool is_StartGameContentPanelDown = startGameContentPanel.GetBool("contentPanelDown");
    //    startGameContentPanel.SetBool("contentPanelDown", !is_StartGameContentPanelDown);
    //    settingsButton.SetBool("slideDown", !is_StartGameContentPanelDown);

    //    if (GameObject.Find("StartButton").GetComponentInChildren<Text>().text == "Start Game")
    //        GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "Choose map - number of players";
    //    else
    //        GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "Start Game";
    //}

    public GameObject mainPanel, quickPlayPanel, createRoomPanel, joinRoomPanel, optionsPanel, creditsPanel;

    // function for switching panels (menus)

    void switchPanels(GameObject panelToClose, GameObject panelToOpen)
    {
        if(panelToOpen != null)
        {
            panelToClose.SetActive(false);
            panelToOpen.SetActive(true);
        }
        else
        {
            System.Console.WriteLine("panel to open is null");
        }
    }

    //  QuickPlayPanel
    
    public void swithFrom_MainPanel_to_QuickPlayPanel()
    {
        switchPanels(mainPanel, quickPlayPanel);
    }

    public void switchFrom_QuickPlayPanel_to_MainPanel()
    {
        switchPanels(quickPlayPanel, mainPanel);
    }

    //  CreateRoomPanel

    public void switchFrom_MainPanel_to_CreateRoomPanel()
    {
        switchPanels(mainPanel, createRoomPanel);
    }

    public void switchFrom_CreateRoomPanel_to_MainPanel()
    {
        switchPanels(createRoomPanel, mainPanel);
    }

    // JoinRoomPanel

    public void switchFrom_JoinRoomPanel_to_MainPanel()
    {
        switchPanels(joinRoomPanel, mainPanel);
    }

    public void switchFrom_MainPanel_to_JoinRoomPanel()
    {
        switchPanels(mainPanel, joinRoomPanel);
    }
    
    // OptionsPanel

    public void switchFrom_MainPanel_to_OptionsPanel()
    {
        switchPanels(mainPanel,optionsPanel);
    }

    public void switchFrom_OptionsPanel_to_MainPAnel()
    {
        switchPanels(optionsPanel, mainPanel);
    }

    // CreditsPanel

    public void switchFrom_MainPanel_to_CreditsPanel()
    {
        switchPanels(mainPanel, creditsPanel);
    }

    public void switchFrom_CreditsPanel_to_MainPanel()
    {
        switchPanels(creditsPanel, mainPanel);
    }

}
