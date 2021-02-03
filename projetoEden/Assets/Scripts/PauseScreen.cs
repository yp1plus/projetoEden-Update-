using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject mainMenu;
    public GameObject commandsMenu;
    public GameObject infoPanel;
    public GameObject btReturn;
    public GameObject backgroundInvisible;
    bool isPaused = false;
    bool flag = false;
    bool showingControls = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (CodingScreen.instance.components.variablesPhase.inputValue.GetComponent<TMP_InputField>().isFocused)
                    return;
            }
            
            isPaused = !isPaused;
            flag = !flag;
        }

        if (isPaused && flag)
        {
            ActivateMenu(true);
        }
        else if (flag)
        {
            ActivateMenu(false);
        }
    }

    public void ActivateMenu(bool state)
    {
        Time.timeScale = state ? 0 : 1;
        mainPanel.SetActive(state);
        mainMenu.SetActive(state);
        infoPanel.SetActive(false);
        commandsMenu.SetActive(false);
        btReturn.SetActive(false);
        backgroundInvisible.SetActive(state);
        isPaused = state;
        flag = false;
    }

    public void ShowCommands()
    {
        mainMenu.SetActive(false);
        infoPanel.SetActive(false);
        commandsMenu.SetActive(true);
        if (WarriorController.level >= (int) WarriorController.PHASES.FIRST_OF_STRUCTURES)
            commandsMenu.transform.GetChild(1).GetComponent<Button>().interactable = true;
        if (WarriorController.level >= (int) WarriorController.PHASES.FIRST_OF_ITERATIVE)
            commandsMenu.transform.GetChild(2).GetComponent<Button>().interactable = true;
        btReturn.SetActive(true);
    }

    public void ShowInfo(int index)
    {
        if (index < 0 || index > 3)
            return;
        
        if (index < 3)
            showingControls = false;
        else
            showingControls = true;

        mainMenu.SetActive(false);
        commandsMenu.SetActive(false);
        infoPanel.SetActive(true);
        btReturn.SetActive(true);
        TMP_Text[] texts = infoPanel.GetComponentsInChildren<TMP_Text>();
        texts[0].text = Screen.missionData.titleInfo[index];
        texts[1].text = Screen.missionData.descriptionInfo[index];
    }   

    public void Return()
    {
        if (commandsMenu.activeSelf || showingControls)
        {
            commandsMenu.SetActive(false);
            infoPanel.SetActive(false);
            mainMenu.SetActive(true);
            btReturn.SetActive(false);
        }
        else
        {
            ShowCommands();
        }
    }

    public void RestartFromLastCheckpoint()
    {
        MainMenu.Reset();
        ActivateMenu(false);
    }
}
