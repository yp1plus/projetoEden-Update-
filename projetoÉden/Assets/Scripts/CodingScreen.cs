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
    public GameObject panel;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Dropdown inputName;
    public Image[] feedbackCorrect = new Image[3];
    public Image[] feedbackIncorrect = new Image[3];
    Mission[] missions = new Mission[10];
    MissionData missionData = new MissionData(); 
    enum InputTypes {type, name, value}; 
    enum Types : ushort {STRING = 1, INT, FLOAT, BOOL, CHAR};

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
        missionData = MissionState.LoadFromJson();
        missions[0] = new Mission1();
        //missions[1] = new Mission2();
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

        title.text = missionData.title[WarriorController.level - 1];
        //description.text = missionData.description[WarriorController.level - 1];
        inputName.ClearOptions();
        List<string> optionsName = missionData.inputName[WarriorController.level - 1].optionsName;
        optionsName.Insert(0, "nomeDaVariável");
        inputName.AddOptions(optionsName);
    }

    void CompareType(int index, int type){
        if (index == type) 
        {
            IsCorrect((int) InputTypes.type);
        }
        else
        {
            IsWrong((int) InputTypes.type);
        }
    }

    /// <summary>
    /// Checks if input it's equal as the type expected.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckType(int index)
    {
        switch(WarriorController.level){
            case 1:
                CompareType(index, (int) Types.INT);
                break;
                    
        }
    }

    /// <summary>
    /// Checks if input it's equal as the names expected.
    /// </summary>
    /// <remarks> Might be more than one name correct on dropdown. </remarks>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    public void CheckName(int index)
    {
        switch(WarriorController.level){
            case 1:
                if (index == 3 || index == 4) 
                {
                    IsCorrect((int) InputTypes.name);
                }
                else
                {
                    IsWrong((int) InputTypes.name);
                }
                break;
        }
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
            int position = answer.IndexOf(";"); //selects before the ';'

            if (position < 0) 
            {
                IsWrong((int) InputTypes.value);
                return;
            }

            value = answer.Substring(0, position);

            switch(WarriorController.level){
                case 1:
                    if (missions[0].AnswerCorrect(value)) IsCorrect((int) InputTypes.value);
                    else IsWrong((int) InputTypes.value);

                    break;
            }
        }
    }

    /// <summary>
    /// Closes the coding screen and executes code of mission only if all of fields are correct.
    /// </summary>
    public void Exit()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!feedbackCorrect[i].IsActive())
                return;
        }

        OpenPanel(false);

        switch(WarriorController.level)
        {
            case 1:
                missions[0].ExecuteCode();
                break;
        }
    }

    /// <summary>
    /// Actives the feedback correct and disables the feedback incorrect if necessary.
    /// </summary>
    private void IsCorrect(int inputType)
    {
        feedbackCorrect[inputType].gameObject.SetActive(true);
        feedbackIncorrect[inputType].gameObject.SetActive(false);
    }

    /// <summary>
    /// Actives the feedback incorrect and disables the feedback correct if necessary.
    /// </summary>
    private void IsWrong(int inputType)
    {
        feedbackIncorrect[inputType].gameObject.SetActive(true);
        feedbackCorrect[inputType].gameObject.SetActive(false);
    }
}
