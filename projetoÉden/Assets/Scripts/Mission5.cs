using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission5 : Mission
{
    GameObject[] inputCameras; 
    CameraController[] cameras = new CameraController[3]; 
    int quant_phases = 1;
    int i = 0;

    void Start()
    {
        GameObject[] cameraObject = GameObject.FindGameObjectsWithTag("Camera");
        for (int i = 0; i < 3; i++)
        {
            cameras[i] = cameraObject[i].GetComponent<CameraController>();
        }
        
    }
    /// </inheritdoc>
    public override void ExecuteCode()
    {
        for (int j = 0; j < 3; j++)
        {
            TMP_InputField inputCamera = inputCameras[i].GetComponent<TMP_InputField>();

            cameras[i].SetName(GetIdentifier(inputCamera.text));
        }
    }

    string GetIdentifier(string answer)
    {
        answer = Mission.RemoveSemicolon(answer);
        answer = answer.Trim(); //removes whitespaces
        answer = answer.Trim('\"'); //removes the double quotes

        return answer;
    }
    
    /* Verifies if the answer it's compilable and if it's one of options */
    bool IsValid(string answer)
    {
        answer = GetIdentifier(answer);
        return answer == "A99" || answer == "A98" || answer == "A97";
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
