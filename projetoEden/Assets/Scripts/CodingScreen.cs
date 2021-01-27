using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Makes appear or dissapear the coding screen, and controls main operations related with dropdowns, textboxs, tips and feedbacks.
/// </summary>
///<remarks>
/// <para> This class it's responsible for control each mission. </para>
/// <para> This class also controls the inputs of user (type, name and value) and its feedbacks. </para>
///</remarks>
public class CodingScreen : Screen
{
    /// <value> Gets the value of static instance of the class </value>
    public static CodingScreen instance { get; private set; }
    public GameObject panel;
    public GameObject codingScreen;
    public TMP_InputField constIdentifier;
    public Image[] feedbackCorrect = new Image[5];
    public Image[] feedbackIncorrect = new Image[5];
    int currentIndex = 0;
    Mission[] missions = new Mission[14];
    public enum InputTypes {type, name, value, for1, for2};
    public enum TipsReferenceIndexes {consts = 0, types = 5, names = 10, generics = 22};

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        missions[0] = gameObject.AddComponent<Mission0>();
        missions[1] = gameObject.AddComponent<Mission1>();
        missions[2] = gameObject.AddComponent<Mission2>();
        missions[3] = gameObject.AddComponent<Mission3>();
        missions[4] = gameObject.AddComponent<Mission4>();
        missions[5] = gameObject.AddComponent<Mission5>(); 
        missions[6] = gameObject.AddComponent<Mission6>();
        missions[7] = gameObject.AddComponent<Mission7>();
        missions[8] = gameObject.AddComponent<Mission8>();
        missions[9] = gameObject.AddComponent<Mission9>();
        missions[10] = gameObject.AddComponent<Mission10>();
        missions[11] = gameObject.AddComponent<Mission11>();
        missions[12] = gameObject.AddComponent<Mission12>();
    }

    public void OpenPanel(bool state)
    {
        if (panel == null)
            return;

        if (state)
        {
            panel.gameObject.SetActive(state);
            fade.FadeIn();
        } 
        else
        {
            if (UIController.instance.InfoIsActive())
                StartCoroutine(ClosePanel());
            else
                StartCoroutine(ForcedClosePanel());
        }

        WarriorController.instance.DeactivateMovement(state);
    }

    public void ForceClosePanel()
    {
        panel.gameObject.SetActive(false);
        codingScreen.SetActive(false);
        UIController.instance.DeactivateInfo(true);
        WarriorController.instance.DeactivateMovement(false);
    }

    /// <summary>
    /// Enables the Canvas of the panel which contains the coding screen.
    /// </summary>
    /// <param name = "state"> A bool, if must open or close the panel. </param>
    public void OpenCode(bool state)
    {
        OpenPanel(state);
        
        if (state)
        {
            LoadData(WarriorController.level);
            UpdateScreen(WarriorController.level);
            Reset();
        } 
        
        codingScreen.SetActive(state);
    }

    IEnumerator ForcedClosePanel()
    {
        yield return new WaitForSeconds(0.3f);
        
        if (!UIController.instance.InfoIsActive() && !codingScreen.activeSelf)
        {
            fade.FadeOut();
            yield return new WaitForSeconds(0.3f);
            panel.gameObject.SetActive(false);
        }
    }

    IEnumerator ClosePanel()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(0.3f);
        panel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if the toggle it's checked, activating the option const.
    /// </summary>
    /// <param name = "isOn"> A bool, if toggle it's checked, obtained dynamycally. </param>
    public void CheckConstIdentifier(bool isOn)
    {
        //Implements later verification wheter type it's correct
        if (isOn)
        {
            constIdentifier.text = missionData.constIdentifier;
        }
        else
        {   
            constIdentifier.text = "";
        }

        CheckType(currentIndex);
    }

    /// <summary>
    /// Checks if input it's equal as the type expected.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckType(int index)
    {
        MissionVariable mission = (MissionVariable) missions[WarriorController.level];
        bool  isConst = constIdentifier.text == missionData.constIdentifier;

        currentIndex = index;

        if (mission.TypeIsCorrect(index, isConst))
            IsCorrect((int) InputTypes.type);
        else
            IsWrong((int) InputTypes.type);
        
        if (index != 0)
            mission.SetIndexTip(index);
        
        if(!mission.ConstIdentifierIsCorrect(isConst))
        {
            mission.SetIndexTip((int) TipsReferenceIndexes.consts);
        }
    }

    /// <summary>
    /// Checks if input it's equal as the names expected.
    /// </summary>
    /// <remarks> Might be more than one name correct on dropdown. </remarks>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckName(int index)
    {
        MissionVariable mission = (MissionVariable) missions[WarriorController.level];

        if (mission.NameIsCorrect(index))
            IsCorrect((int) InputTypes.name);
        else
            IsWrong((int) InputTypes.name);
        
        if (index != 0)
            mission.SetIndexTip(index + (int) TipsReferenceIndexes.types); //name comes after all types in list
    }

    /// <summary>
    /// Checks if input it's correct based on the individual methods of each mission.
    /// </summary>
    /// <remarks> Gets the answer when the button it's clicked. </remarks>
    /// <remarks> Treats the expression, verifying sintaxe and removing semicolon. </remarks>
    public void CheckValue()
    {
        Mission6 mission = (Mission6) missions[WarriorController.level];

        if (mission != null && mission.AnswerIsCorrect())
            IsCorrect((int) InputTypes.value);
        else
            IsWrong((int) InputTypes.value);
    }

    /// <summary>
    /// Checks if statement it's correct based on the individual methods of each mission.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckStatement(int index)
    {
        MissionStructure mission = (MissionStructure) missions[WarriorController.level];

        if (mission != null && mission.StatementIsCorrect(index))
        {
            if (WarriorController.level < (int) WarriorController.PHASES.FOURTH_WALL)
                IsCorrect((int) InputTypes.value);
            else
                IsCorrect((int) InputTypes.for1);
        }    
        else
        {
            if (WarriorController.level < (int) WarriorController.PHASES.FOURTH_WALL)
                IsWrong((int) InputTypes.value);
            else
                IsWrong((int) InputTypes.for1);
        }
    }

    /// <summary>
    /// Checks if a second statement it's correct especifically in mission 10.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckStatement2(int index)
    {
        Mission12 mission = WarriorController.level == 12 ? (Mission12) missions[WarriorController.level] : null;

        if (mission != null && mission.Statement2IsCorrect(index))
            IsCorrect((int) InputTypes.for2);
        else if (WarriorController.level == (int) WarriorController.PHASES.FOURTH_WALL)
            IsWrong((int) InputTypes.for2);
    }

    /// <summary>
    /// Checks if input it's correct based on the individual methods of each mission.
    /// </summary>
    /// <remarks> Gets the answer when key enter is pressed. </remarks>
    /// <remarks> Treats the expression, verifying sintaxe and removing semicolon. </remarks>
    /// <param name = "answer"> The string, a answer getted of textbox. </param>
    public void CheckValue(string answer)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            string value = answer;
            MissionVariable mission = (MissionVariable) missions[WarriorController.level];
            bool couldRemove = true;
        
            value = Mission.RemoveSemicolon(answer);

            if (value == null) 
            {
                mission.SetIndexTip((int) TipsReferenceIndexes.generics); //a huge index to the end of list
                couldRemove = false;
                value = answer;
            }

            if (mission.AnswerIsCorrect(value) && couldRemove)
                IsCorrect((int) InputTypes.value);
            else
                IsWrong((int) InputTypes.value);
        }
    }

    public int GetIndexTip()
    {
        return missions[WarriorController.level].GetIndexTip();
    }

    public void RemoveTip()
    {
        missions[WarriorController.level].RemoveTip();
    }

    /// <summary>
    /// Closes the coding screen and executes code of mission only if all of fields are correct.
    /// </summary>
    public void Exit()
    {
        if (WarriorController.level < (int) WarriorController.PHASES.LAST_OF_VARIABLES)
        {
            for (int i = Languages.isPython() ? 1 : 0; i < 3; i++)
            {
                if (!feedbackCorrect[i].IsActive())
                {
                    TipsController.instance.ShowMessageExitError();
                    return;
                }
            }
        }
        else if (WarriorController.level < (int) WarriorController.PHASES.LAST_OF_STRUCTURES)
        {
            if(!feedbackCorrect[(int) InputTypes.value].IsActive()) //just the third feedback needs to appear
            {
                TipsController.instance.ShowMessageExitError();
                return;
            } 
        } //CHANGE LATER
        else if (WarriorController.level == (int) WarriorController.PHASES.LAST_OF_STRUCTURES)
        {
            if(!feedbackCorrect[(int) InputTypes.for1].IsActive() || !feedbackCorrect[(int) InputTypes.for2].IsActive())
            {
                TipsController.instance.ShowMessageExitError();
                return;
            }       
        }

        OpenCode(false);

        if (WarriorController.level == (int) WarriorController.PHASES.FIRST_OF_VARIABLES
            || (WarriorController.level > (int) WarriorController.PHASES.CHICKENS
            && WarriorController.level != (int) WarriorController.PHASES.BARRIER
            && WarriorController.level <= (int) WarriorController.PHASES.LAST_OF_VARIABLES))
            UIController.instance.ShowNewInfo();
        
        if (WarriorController.level <= (int) WarriorController.PHASES.LAST_OF_STRUCTURES)
        {
            MainMenu.StartScene(WarriorController.level);
            missions[WarriorController.level].ExecuteCode();
        } 
    }

    public void StartTutorial()
    {
        OpenCode(true);
        
        if (MainMenu.tutorialExecuted)
            return;

        GetComponent<InitialTutorial>().StartTutorial();
        MainMenu.tutorialExecuted = true;
    }

    void Reset()
    {
        TipsController.instance.ResetTip();
        
        for (int i = 0; i <= (int) InputTypes.for2; i++)
        {
            feedbackCorrect[i].gameObject.SetActive(false);
            feedbackIncorrect[i].gameObject.SetActive(false);
        }
    }

    public bool FeedbackCorrectIsActive(int inputType)
    {
        return feedbackCorrect[inputType].gameObject.activeSelf;
    }

    // Actives the feedback correct and disables the feedback incorrect if necessary.
    void IsCorrect(int inputType)
    {
        feedbackCorrect[inputType].gameObject.SetActive(true);
        feedbackIncorrect[inputType].gameObject.SetActive(false);
    }

    // Actives the feedback incorrect and disables the feedback correct if necessary.
    void IsWrong(int inputType)
    {
        feedbackIncorrect[inputType].gameObject.SetActive(true);
        feedbackCorrect[inputType].gameObject.SetActive(false);
    }
}
