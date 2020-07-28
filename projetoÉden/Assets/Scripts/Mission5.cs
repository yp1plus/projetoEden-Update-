using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission5 : Mission
{
    GameObject[] inputCameras; 
    int quant_phases = 1;
    int i = 0;

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        switch(quant_phases)
        {
            case 1:
                if (!IsSolution())
                    Debug.Log("Erro! A câmera A99 te viu!");
                break;
            case 2:
                if (!IsSolution())
                    Debug.Log("Erro! Alguma câmera te viu!");
                break;
        }
    }
    
    /* Verifies if the answer it's compilable and if it's one of options */
    bool IsValid(string answer)
    {
        answer = Mission.RemoveSemicolon(answer);
        answer = answer.Trim(); //removes whitespaces
        return answer == @"""A99""" || answer == @"""A98""" || answer == @"""A97""";
    }

    /// </inheritdoc>
    public bool AnswerIsCorrect()
    {
        if (inputCameras == null)
            inputCameras = GameObject.FindGameObjectsWithTag("InputCamera"); //3 cameras

        for (int i = 0; i < 3; i++)
        {
            TMP_InputField inputCamera = inputCameras[i].GetComponent<TMP_InputField>();
    
            if (inputCamera != null)
                if (!IsValid(inputCamera.text))
                    return false;
        }

        return true;
    }

    /* Verifies if the answer solves the problem */
    bool IsSolution()
    {
        switch(quant_phases) 
        {
            case 1:
                quant_phases++;
                for (int j = 0; j < 3; j++)
                {
                    TMP_InputField inputCamera = inputCameras[i].GetComponent<TMP_InputField>();

                    if (inputCamera != null)
                        if (inputCamera.text != @"""A99""" && (inputCamera.text == @"""A98""" 
                            || inputCamera.text == @"""A97"""))
                            continue;
                    else
                        return false;
                }
                break;
            case 2:
                for (int j = 0; j < 3; j++)
                {
                    TMP_InputField inputCamera = inputCameras[i].GetComponent<TMP_InputField>();

                    if (inputCamera.text == @"""A99""")
                        continue;
                    else
                        return false;
                }
                break;
        }

        return false;
    }
}
