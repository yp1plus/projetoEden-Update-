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
        public TMP_Text[] camerasValue;
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
        missionData = MissionState.LoadFromJson();

        switch(Languages.indexLanguage)
        {
            case (int) Languages.TypesLanguages.CSharp:
                UpdateForCSharp();
                break;
            case (int) Languages.TypesLanguages.Java:
                UpdateForJava();
                break;
            case (int) Languages.TypesLanguages.Python:
                UpdateForPython();
                break;
        }

        typeFromMission0 = missionData.inputTypes[Languages.indexLanguage].options[(int) Mission.Types.STRING];
        genericTips = missionData.genericTips;
        
        fade = gameObject.AddComponent<Fade>();
    }

    public void LoadData(int level)
    {
        components.title.text = missionData.title[level];
        components.description.text = missionData.description[level];
        List<string> options = missionData.input[level].options;
        tip = missionData.tips[level];

        if (level <= 4)
        {
            variablesPhaseParent.SetActive(true); //change later
            ResetVariablesPhase();

            UpdateDropDown(components.variablesPhase.inputName, options);

            if (level == 0) //inputType order don't change
            {
                options = missionData.inputTypes[Languages.indexLanguage].options;
                UpdateDropDown(components.variablesPhase.inputType, options);
            }
        }
        else if (level >= 6)
        {
            UpdateDropDown(components.structurePhase.condition1, options);

            if (level == 9)
            {
                UpdateDropDown(components.structurePhase.condition2, missionData.options_for2);
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
        if (level == 5) 
        {
            variablesPhaseParent.SetActive(false);
            camerasPhaseParent.SetActive(true);
        } 
        else if (level >= 6) //change later
        {
            camerasPhaseParent.SetActive(false);
            structuresPhaseParent.SetActive(true);
        } 
        if (level >= 7 && level <= 9) //change later
        {
            bool mustActivate = level == 7 || level == 9;
            components.structurePhase.result1.transform.GetChild(0).GetComponent<TMP_Text>().text = missionData.resultsStructures1[level - 7];
            components.structurePhase.result2.transform.GetChild(0).GetComponent<TMP_Text>().text = missionData.resultsStructures2[level - 7];

            ActivateStructure2(mustActivate);
            components.structurePhase.statement1.SetActive(mustActivate);

            if (level == 9)
                UpdateForPhase9();
        }
    }

    //Activates the condition 2 (nested for) and updates your position
    void UpdateForPhase9()
    {
        components.structurePhase.statement1.transform.GetChild(0).GetComponent<TMP_Text>().text = "for";
        components.structurePhase.statement2.transform.GetChild(0).GetComponent<TMP_Text>().text = "for";
        components.structurePhase.condition2.GetComponent<TMP_Dropdown>().interactable = true;
        components.structurePhase.condition2.transform.GetChild(1).gameObject.SetActive(true); //Shows Arrow
        components.structurePhase.result1.SetActive(false); //only needs one result, it's not if else 
        components.structurePhase.statement2.gameObject.transform.localPosition = new Vector3(-148.46f, -126.1f, 0);
        components.structurePhase.condition2.transform.localPosition = new Vector3(36.1f, -126.1f, 0);
        components.structurePhase.result2.transform.localPosition = new Vector3(-69.9f, -167.6f, 0);
    }

    void ResetVariablesPhase()
    {  
        components.variablesPhase.constIdentifier.GetComponentInChildren<Toggle>().isOn = false;
        components.variablesPhase.inputType.GetComponent<TMP_Dropdown>().value = 0;
        components.variablesPhase.inputValue.GetComponent<TMP_InputField>().text = "";
    }

    void ActivateStructure2(bool state)
    {
        components.structurePhase.statement2.SetActive(state); 
        components.structurePhase.condition2.SetActive(state);
        components.structurePhase.result2.SetActive(state);
    }

    void UpdateForPython()
    {
        MissionState.OverloadFromJson(missionData, "InfoPython");

        /* Python doesn't require data type */
        components.variablesPhase.constIdentifier.SetActive(false);
        components.variablesPhase.inputType.SetActive(false);

        components.variablesPhase.inputName.transform.localPosition = new Vector3(-110.229f, -117.4f, 0);
        components.variablesPhase.identifier.transform.localPosition = new Vector3(-25.7f, -117.4f, 0);
        components.variablesPhase.inputValue.transform.localPosition = new Vector3(45.9f, -117.4f, 0);
        components.variablesPhase.feedbackName.transform.localPosition = new Vector3(-116.229f, 0, 0);
        components.variablesPhase.feedbackValue.transform.localPosition = new Vector3(-105.1f, 0, 0);

        components.structurePhase.statement2.transform.GetChild(0).GetComponent<TMP_Text>().text = "elif";
        components.camerasPhase.camerasTypes[0].text = "CAMERA_A99";
        components.camerasPhase.camerasTypes[1].text = "CAMERA_A98";
        components.camerasPhase.camerasTypes[2].text = "CAMERA_A97";
        components.camerasPhase.camerasValue[0].text = "\"A99\"";
        components.camerasPhase.camerasValue[1].text = "\"A98\"";
        components.camerasPhase.camerasValue[2].text = "\"A97\"";
    }

    void UpdateForCSharp()
    {
        MissionState.OverloadFromJson(missionData, "InfoCSharp");
        components.camerasPhase.camerasTypes[0].text = "string CAMERA_A99";
        components.camerasPhase.camerasTypes[1].text = "string CAMERA_A98";
        components.camerasPhase.camerasTypes[2].text = "string CAMERA_A97";
    }

    void UpdateForJava()
    {
        MissionState.OverloadFromJson(missionData, "InfoJava");
        components.variablesPhase.constIdentifier.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "final";
        components.camerasPhase.camerasTypes[0].text = "String CAMERA_A99";
        components.camerasPhase.camerasTypes[1].text = "String CAMERA_A98";
        components.camerasPhase.camerasTypes[2].text = "String CAMERA_A97";
    }

    public void ShowGameOver(bool state)
    {
        if (panelGameOver != null && panelGameOver.activeSelf != state)
        {
            panelGameOver.SetActive(state);
            if (state)
                WarriorController.instance.audioController.PlaySound(gameOverClip);
            if (!state)
                infiniteLoopTxt.GetComponent<TMP_Text>().enabled = state;
        }
    }
}


