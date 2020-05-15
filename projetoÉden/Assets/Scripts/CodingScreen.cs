using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodingScreen : MonoBehaviour
{
    public static CodingScreen instance { get; private set; }
    public GameObject panel;
    public Image[] feedbackCorrect = new Image[3];
    public Image[] feedbackIncorrect = new Image[3];

    private enum inputTypes {type, name, value};
    //public TMP_InputField inputType;
    // public TMP_InputField inputName;
    // public TMP_InputField inputValue;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        /*for (int i = 0; i < 3; i++)
        {
            feedbackCorrect[i] = GetComponent<Image>();
            feedbackIncorrect[i] = GetComponent<Image>();
        }*/
        //inputType = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sair(){
        for (int i = 0; i < 3; i++)
        {
            if (!feedbackCorrect[i].IsActive())
                return;
        }

        OpenPanel(false);
    }

    public void CompareAnswer(string answer, int inputType){
        switch(inputType){
            case (int) inputTypes.type: 
                if (answer.Equals("int"))
                    IsCorrect((int) inputTypes.type);
                else 
                    IsWrong((int) inputTypes.type);
                break;

            case (int) inputTypes.name:
                if (answer.Equals("quant_galinhas"))
                    IsCorrect((int) inputTypes.name);
                else
                    IsWrong((int) inputTypes.name);
                break;

            case (int) inputTypes.value:
                if (answer.Equals("galinhas/2;"))
                    IsCorrect((int) inputTypes.value);
                else
                    IsWrong((int) inputTypes.value);
                break;
        }
    }

    private void IsCorrect(int inputType)
    {
        feedbackCorrect[inputType].gameObject.SetActive(true);
        feedbackIncorrect[inputType].gameObject.SetActive(false);
    }

    private void IsWrong(int inputType)
    {
        feedbackIncorrect[inputType].gameObject.SetActive(true);
        feedbackCorrect[inputType].gameObject.SetActive(false);
    }

    public void SubmitAnswer(TMP_InputField input){
        if (Input.GetKey(KeyCode.Return))
        {
            switch(input.name){
                case "InputType":
                    CompareAnswer(input.text, (int) inputTypes.type);
                    break;
                case "InputName":
                    CompareAnswer(input.text, (int) inputTypes.name);
                    break;
                case "InputValue":
                    CompareAnswer(input.text, (int) inputTypes.value);
                    break;
            }
            
            //Debug.Log(input.text);
        }
    }

    public void OpenPanel(bool state)
    {
        if (panel != null)
        {
           panel.gameObject.SetActive(state);
        }
    }
}
