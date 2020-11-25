using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject [] powers = new GameObject [4];
    public GameObject intro;

    [System.Serializable]
    public struct Info{
        public GameObject parent;
        public TMP_Text title;
        public TMP_Text description;
    }

    public Info info = new Info();
    UIInfo txtInfoUI;
    int index = 7;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //CodingScreen.instance.OpenPanel(true);
        //intro.SetActive(true);
        txtInfoUI = MissionState.LoadUIFromJson();
    }

    public void GoToTutorial()
    {
        intro.SetActive(false);
        CodingScreen.instance.StartTutorial();   
    }

    public void ResetPower()
    {
        if (WarriorController.level >= 0 && WarriorController.level <= 3)
        {
            powers[WarriorController.level].SetActive(false);
            index = WarriorController.level + 1;
        }
    }

    public void ShowNewInfo()
    {
        if (index < 5)
        {
            HideInfo(false);
            info.title.text = txtInfoUI.title[index];
            info.description.text = txtInfoUI.description[index];
            if (index > 0)
                powers[index - 1].SetActive(true);
            index++;
        }
    }

    public void HideInfo(bool state)
    {
        CodingScreen.instance.OpenPanel(!state);
        info.parent.SetActive(!state);
    }

    public bool InfoIsActive()
    {
        return info.parent.activeSelf;
    }

    public void CheckSkipTutorialToggle(bool isOn)
    {
        MainMenu.tutorialExecuted = isOn;
    }
}
