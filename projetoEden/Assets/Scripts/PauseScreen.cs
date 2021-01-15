﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        backgroundInvisible.SetActive(state);
        isPaused = state;
        flag = false;
    }

    public void ShowCommands()
    {
        mainMenu.SetActive(false);
        infoPanel.SetActive(false);
        commandsMenu.SetActive(true);
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
}
