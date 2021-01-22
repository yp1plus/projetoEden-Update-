using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject [] powers = new GameObject [4];
    public GameObject intro;

    [System.Serializable]
    public struct Info{
        public GameObject parent;
        public TMP_Text title;
        public GameObject description;
        public GameObject congratulations;
        public Button confirmation;
    }

    public Info info = new Info();
    UIInfo txtInfoUI;
    int index;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!MainMenu.debug)
        {
            CodingScreen.instance.OpenPanel(true);
            intro.SetActive(true);
        }
        txtInfoUI = MissionState.LoadUIFromJson();
        index = 5;
    }

    public void GoToTutorial()
    {
        intro.SetActive(false);
        CodingScreen.instance.StartTutorial();   
    }

    public void ResetPower()
    {
        if (WarriorController.level >= 0 
            && WarriorController.level < (int) WarriorController.PHASES.BATTLE - 1 
            || WarriorController.level == (int) WarriorController.PHASES.CLOUDS - 1)
        {
            powers[Mathf.Clamp(WarriorController.level, 0, 3)].SetActive(false);
            index = Mathf.Clamp(WarriorController.level + 1, 0, 4);
        }
    }

    public void ShowNewInfo()
    {
        if (index <= 5)
        {
            if (index != WarriorController.level && WarriorController.level <= (int) WarriorController.PHASES.CLOUDS)
                index = Mathf.Clamp(WarriorController.level, 0, 4);
            
            if (index == 5)
            {
                info.congratulations.SetActive(false);
                //Increases description height maintaining the same reference position
                RectTransform rt = info.description.GetComponent<RectTransform>();
                Vector2 sizeDelta = rt.sizeDelta;
                sizeDelta.y = 206.98f;
                rt.sizeDelta = sizeDelta;
                rt.anchoredPosition = new Vector3(0, -71.887f, 0);
                TMP_Text txtButton = info.confirmation.GetComponentInChildren<TMP_Text>(); 
                if (txtButton != null)
                    txtButton.text = "Vamos nessa!";
            }
                        
            HideInfo(false);
            info.title.text = txtInfoUI.title[index];
            TMP_Text description = info.description.GetComponent<TMP_Text>();
            if (description != null)
                description.text = txtInfoUI.description[index];
            if (index > 0 && index <= 4)
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
