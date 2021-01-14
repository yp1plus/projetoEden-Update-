using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    public GameObject variablesPhaseParent;
    public GameObject camerasPhaseParent;
    public GameObject structuresPhaseParent;
    public static MissionData missionData {get; private set;}
    public static string typeFromMission0 {get; private set;}
    public static Tip tip {get; private set;}

    public static List<string> genericTips {get; private set;}

    public GameObject panelGameOver;
    public GameObject infiniteLoopTxt;

    [System.Serializable]
    public struct VariablesPhase
    {
        public GameObject constIdentifier;
        public GameObject inputType;
        public GameObject inputName;
        public GameObject identifier;
        public GameObject inputValue;
        public GameObject feedbackName;
        public GameObject feedbackValue;
    }
    
    [System.Serializable]
    public struct StructurePhase 
    {
        public GameObject statement1;
        public GameObject condition1;
        public GameObject result1;
        public GameObject statement2;
        public GameObject condition2;
        public GameObject result2;
    }

    [System.Serializable]
    public struct CamerasPhase 
    {
        public TMP_Text [] camerasTypes;
        public TMP_InputField[] camerasValue;
    }

    [System.Serializable]
    public struct Components
    {
        public TMP_Text title;
        public TMP_Text description;
        public VariablesPhase variablesPhase;
        public StructurePhase structurePhase;
        public CamerasPhase camerasPhase;
    }

    public Components components = new Components();

    protected Fade fade;
    public AudioClip gameOverClip; 

    public virtual void Awake()
    {
        Initialize();

        typeFromMission0 = missionData.inputTypes[Languages.indexLanguage].options[(int) Mission.Types.STRING];
        genericTips = missionData.genericTips;
        
        fade = gameObject.AddComponent<Fade>();
    }

    /* Charges data and sets up screen according the programming language */
    void Initialize()
    {
        missionData = MissionState.LoadFromJson();

        switch(Languages.indexLanguage)
        {
            case (int) Languages.TypesLanguages.CSharp:
                MissionState.OverloadFromJson(missionData, "InfoCSharp");
                break;
            case (int) Languages.TypesLanguages.Java:
                MissionState.OverloadFromJson(missionData, "InfoJava");
                break;
            case (int) Languages.TypesLanguages.Python:
                MissionState.OverloadFromJson(missionData, "InfoPython");
                SetUpScreenForPython();
                break;
        }

        components.variablesPhase.constIdentifier.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = missionData.constIdentifier;

        for(int i = 0; i < 3; i++)
        {
            components.camerasPhase.camerasTypes[i].text = missionData.namesCameras[i];
        }
    }

    public void LoadData(int level)
    {
        components.title.text = missionData.title[level];
        components.description.text = missionData.description[level];
        List<string> options = missionData.input[level].options;
        tip = missionData.tips[level];

        if (level <= (int) WarriorController.PHASES.LAST_OF_VARIABLES)
        {
            variablesPhaseParent.SetActive(true); //change later

            if (level != (int) WarriorController.PHASES.CLOUDS)
            {
                ResetVariablesPhase();
                UpdateDropDown(components.variablesPhase.inputName, options);
            }
            else
            {
                BlockTypeAndNameInput();
            }

            if (level == 0) //inputType order don't change
            {
                options = missionData.inputTypes[Languages.indexLanguage].options;
                UpdateDropDown(components.variablesPhase.inputType, options);
            }
        }
        else if (level >= (int) WarriorController.PHASES.FIRST_OF_STRUCTURES)
        {
            UpdateDropDown(components.structurePhase.condition1, options);

            if (level >= (int) WarriorController.PHASES.BUG && level <= (int) WarriorController.PHASES.FOURTH_WALL)
            {
                UpdateDropDown(components.structurePhase.condition2, missionData.optionsCondition2[level - (int) WarriorController.PHASES.BUG].options);
            }
        }
    }

    void UpdateDropDown(GameObject input, List<string> options)
    {
        TMP_Dropdown dropdown = input.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void UpdateScreen(int level)
    {
        if (level == (int) WarriorController.PHASES.CAMERAS) 
        {
            variablesPhaseParent.SetActive(false);
            camerasPhaseParent.SetActive(true);
            if (Languages.isPython())
                components.variablesPhase.feedbackValue.transform.localPosition = new Vector3(0, 0, 0);
        } 
        else if (level >= (int) WarriorController.PHASES.FIRST_OF_STRUCTURES)
        {
            variablesPhaseParent.SetActive(false);
            camerasPhaseParent.SetActive(false);
            structuresPhaseParent.SetActive(true);

            bool mustActivate = level == (int) WarriorController.PHASES.BUG
                || level == (int) WarriorController.PHASES.FOURTH_WALL
                || level == (int) WarriorController.PHASES.FLOATING_PLATFORM;
            bool isPhaseBladesBarrier = level == (int) WarriorController.PHASES.BLADES_BARRIER;
            components.structurePhase.result1.transform.GetChild(0).GetComponent<TMP_Text>().text 
                = missionData.resultsStructures1[level - (int) WarriorController.PHASES.FIRST_OF_STRUCTURES];
            components.structurePhase.result2.transform.GetChild(0).GetComponent<TMP_Text>().text 
                = missionData.resultsStructures2[level - (int) WarriorController.PHASES.FIRST_OF_STRUCTURES];

            components.structurePhase.statement1.SetActive(mustActivate || isPhaseBladesBarrier);
            components.structurePhase.statement1.transform.GetChild(0).GetComponent<TMP_Text>().text = 
                missionData.statementsStructures1[level - (int) WarriorController.PHASES.FIRST_OF_STRUCTURES];
            ActivateStructure2(mustActivate);
            components.structurePhase.statement2.transform.GetChild(0).GetComponent<TMP_Text>().text = 
                missionData.statementsStructures2[level - (int) WarriorController.PHASES.FIRST_OF_STRUCTURES];

            if (level == (int) WarriorController.PHASES.FLOATING_PLATFORM)
                components.structurePhase.condition2.SetActive(false);
      
            else if (level == (int) WarriorController.PHASES.FOURTH_WALL)
                SetUpScreenForFourthWallPhase();
        } 
    }

    //Activates the condition 2 (nested for) and updates your position
    void SetUpScreenForFourthWallPhase()
    {
        components.structurePhase.condition2.GetComponent<TMP_Dropdown>().interactable = true;
        components.structurePhase.condition2.transform.GetChild(1).gameObject.SetActive(true); //Shows Arrow
        components.structurePhase.result1.SetActive(false); //only needs one result, it's not if else 
        components.structurePhase.statement2.gameObject.transform.localPosition = new Vector3(-148.46f, -126.1f, 0);
        components.structurePhase.condition2.transform.localPosition = new Vector3(36.1f, -126.1f, 0);
        components.structurePhase.result2.transform.localPosition = new Vector3(-69.9f, -167.6f, 0);
    }

    void BlockTypeAndNameInput()
    {
        Toggle toggle = components.variablesPhase.constIdentifier.GetComponentInChildren<Toggle>();
        toggle.interactable = false;
        TMP_Dropdown dropdown = components.variablesPhase.inputType.GetComponent<TMP_Dropdown>();
        dropdown.interactable= false;
        dropdown = components.variablesPhase.inputName.GetComponent<TMP_Dropdown>();
        dropdown.interactable = false;
        components.variablesPhase.inputValue.GetComponent<TMP_InputField>().text = "";
    }

    void ResetVariablesPhase()
    {   
        Toggle toggle = components.variablesPhase.constIdentifier.GetComponentInChildren<Toggle>();
        toggle.interactable = true;
        toggle.isOn = false;
        TMP_Dropdown dropdown = components.variablesPhase.inputType.GetComponent<TMP_Dropdown>();
        dropdown.interactable = true;
        dropdown.value = 0;
        dropdown = components.variablesPhase.inputName.GetComponent<TMP_Dropdown>();
        dropdown.interactable = true;
        components.variablesPhase.inputValue.GetComponent<TMP_InputField>().text = "";
    }

    void ActivateStructure2(bool state)
    {
        components.structurePhase.statement2.SetActive(state); 
        components.structurePhase.condition2.SetActive(state);
        components.structurePhase.result2.SetActive(state);
    }

    void SetUpScreenForPython()
    {
        /* Python doesn't require data type */
        components.variablesPhase.constIdentifier.SetActive(false);
        components.variablesPhase.inputType.SetActive(false);

        components.variablesPhase.inputName.transform.localPosition = new Vector3(-110.229f, -117.4f, 0);
        components.variablesPhase.identifier.transform.localPosition = new Vector3(-25.7f, -117.4f, 0);
        components.variablesPhase.inputValue.transform.localPosition = new Vector3(45.9f, -117.4f, 0);
        components.variablesPhase.feedbackName.transform.localPosition = new Vector3(-116.229f, 0, 0);
        components.variablesPhase.feedbackValue.transform.localPosition = new Vector3(-105.1f, 0, 0);

        components.structurePhase.statement2.transform.GetChild(0).GetComponent<TMP_Text>().text = "elif";
        
        components.camerasPhase.camerasValue[0].text = "\"A99\"";
        components.camerasPhase.camerasValue[1].text = "\"A98\"";
        components.camerasPhase.camerasValue[2].text = "\"A97\"";
    }

    public void ShowGameOver(bool state)
    {
        if (panelGameOver != null && panelGameOver.activeSelf != state)
        {
            WarriorController.instance.DeactivateMovement(state);
            panelGameOver.SetActive(state);
            if (state)
                WarriorController.instance.audioController.PlaySound(gameOverClip);
            if (!state)
                infiniteLoopTxt.GetComponent<TMP_Text>().enabled = state;
        }
    }
}


