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

    public WarriorController warriorController;
    private enum inputTypes {type, name, value};
    private enum types : ushort {STRING = 1, INT, FLOAT, BOOL, CHAR};

    /* Valores digitados pelo Usuário */
    int value_int;
    float value_float;
    string value_string;
    char value_char;
    bool value_bool;

    //public TMP_InputField inputType;
    // public TMP_InputField inputName;
    // public TMP_InputField inputValue;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CompareType(int index, int type){
        if (index == type) 
        {
            IsCorrect((int) inputTypes.type);
        }
        else
        {
            IsWrong((int) inputTypes.type);
        }
    }

    public void CheckType(int index)
    {
        Debug.Log(index);
        switch(warriorController.level){
            case 1:
                CompareType(index, (int) types.INT);
                break;
                    
        }
    }

    public void CheckName(int index)
    {
        Debug.Log(index);
        switch(warriorController.level){
            case 1:
                if (index == 3 || index == 4) 
                {
                    IsCorrect((int) inputTypes.name);
                }
                else
                {
                    IsWrong((int) inputTypes.name);
                }
                break;
        }
    }

    public void CheckValue(TMP_InputField input)
    {
        if (Input.GetKey(KeyCode.Return))
        {
        string value, answer = input.text;
        bool canConvert;
        int position = answer.IndexOf(";"); //seleciona antes do ;
        Debug.Log(position);

        if (position < 0) IsWrong((int) inputTypes.value);

        value = answer.Substring(0, position);

        switch(warriorController.level){
            case 1:
                value_int = 0;
                canConvert = int.TryParse(value, out value_int);
                if (canConvert)
                {
                    if (value_int >= 0) IsCorrect((int) inputTypes.value);
                }
                else IsWrong((int) inputTypes.value);

                break;
        }
        }
 
    }

    public void Exit()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!feedbackCorrect[i].IsActive())
                return;
        }

        OpenPanel(false);

        switch(warriorController.level)
        {
            case 1:
                if (value_int <= 30)
                    DestroyChickens(value_int);
                else
                    CreateChickens(value_int - 30);
                break;
        }
    }

    void DestroyChickens(int n)
    {
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");

        for(int i = 30; i > n; i--)
        {
            GameObject.Destroy(chickens[i]);
        }
    }

    void CreateChickens(int n)
    {
        GameObject chicken = GameObject.FindGameObjectWithTag("Chicken");
        float x = 3.661544F, y = -0.269F;

        for (int i = 0; i < n; i++)
        {
             Instantiate(chicken, new Vector3(x, y, 0), Quaternion.identity);
             if (y < 0.286) y += 0.3F;
             else 
             {
                 x += 0.3F; 
                 y = -0.269F;
             }
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

    public void OpenPanel(bool state)
    {
        if (panel != null)
        {
           panel.gameObject.SetActive(state);
        }
    }
}
