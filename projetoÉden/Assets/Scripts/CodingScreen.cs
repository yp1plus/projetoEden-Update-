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
public class CodingScreen : MonoBehaviour
{
    /// <value> Gets the value of static instance of the class </value>
    public static CodingScreen instance { get; private set; }
    public Screen screen;
    public WarriorController warrior;
    public GameObject panel;
    public TMP_InputField constIdentifier;
    public Image[] feedbackCorrect = new Image[5];
    public Image[] feedbackIncorrect = new Image[5];
    int currentIndex = 0;
    Mission[] missions = new Mission[10];
    public enum InputTypes {type, name, value, for1, for2}; 

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
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
        screen = gameObject.GetComponent<Screen>();
        OpenPanel(true);
    }

    /// <summary>
    /// Enables the Canvas of the panel which contains the coding screen.
    /// </summary>
    public void OpenPanel(bool state)
    {
        if (panel != null)
        {
           panel.gameObject.SetActive(state);
        }

        if (state)
        {
            screen.LoadData(WarriorController.level);
            screen.UpdateScreen(WarriorController.level);
        }

        warrior.DeactivateMovement(state);
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
            constIdentifier.text = "const";
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
        string _const = constIdentifier.text;

        currentIndex = index;

        if (mission.TypeIsCorrect(index, _const == "const"))
            IsCorrect((int) InputTypes.type);
        else
            IsWrong((int) InputTypes.type);
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
    }

    /// <summary>
    /// Checks if input it's correct based on the individual methods of each mission.
    /// </summary>
    /// <remarks> Gets the answer when the button it's clicked. </remarks>
    /// <remarks> Treats the expression, verifying sintaxe and removing semicolon. </remarks>
    public void CheckValue()
    {
        Mission5 mission = (Mission5) missions[WarriorController.level];

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
        MissionStructure mission =  (MissionStructure) missions[WarriorController.level];

        if (mission != null && mission.StatementIsCorrect(index))
        {
            if (WarriorController.level < 9)
                IsCorrect((int) InputTypes.value);
            else
                IsCorrect((int) InputTypes.for1);
        }    
        else
        {
            if (WarriorController.level < 9)
                IsWrong((int) InputTypes.value);
            else
                IsWrong((int) InputTypes.for1);
        }
    }

    /// <summary>
    /// Checks if a second statement it's correct especifically in mission 9.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckStatement2(int index)
    {
        Mission9 mission =  (Mission9) missions[WarriorController.level];

        if (mission != null && mission.Statement2IsCorrect(index))
            IsCorrect((int) InputTypes.for2);
        else
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
            string value;
            MissionVariable mission = (MissionVariable) missions[WarriorController.level];
            
            value = Mission.RemoveSemicolon(answer);

            if (value == null) 
            {
                IsWrong((int) InputTypes.value);
                return;
            }

            if (mission.AnswerIsCorrect(value))
                IsCorrect((int) InputTypes.value);
            else
                IsWrong((int) InputTypes.value);
        }
    }

    /// <summary>
    /// Closes the coding screen and executes code of mission only if all of fields are correct.
    /// </summary>
    public void Exit()
    {
        if (WarriorController.level < 5)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!feedbackCorrect[i].IsActive())
                    return;
            }
        }
        else if (WarriorController.level < 9)
        {
            if(!feedbackCorrect[(int) InputTypes.value].IsActive()) //just the third feedback needs to appear
                return;
        }
        else if (WarriorController.level == 9)
        {
            if(!feedbackCorrect[(int) InputTypes.for1].IsActive() || !feedbackCorrect[(int) InputTypes.for2].IsActive())
                return;
        }

        OpenPanel(false);

        missions[WarriorController.level].ExecuteCode();
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
