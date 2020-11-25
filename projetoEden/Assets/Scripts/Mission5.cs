using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission5 : Mission
{
    GameObject[] inputCameras; 
    CameraController[] cameras = new CameraController[3]; 
    int i = 0;

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        if (cameras[0] == null)
        {   
            GameObject[] cameraObject = GameObject.FindGameObjectsWithTag("Camera");

            for (i = 0; i < 3; i++)
            {
                if (cameraObject != null)
                    cameras[i] = cameraObject[i].GetComponent<CameraController>();
            }
        }
        
        for (i = 0; i < 3; i++)
        {
            TMP_InputField inputCamera = inputCameras[i].GetComponent<TMP_InputField>();

            cameras[i].SetName(GetIdentifier(inputCamera.text));
        }
    }

    string GetIdentifier(string answer)
    {
        if (Languages.indexLanguage != (int) Languages.TypesLanguages.Python)
            answer = Mission.RemoveSemicolon(answer);
        
        if (answer != null)
        {
            answer = answer.Trim(); //removes whitespaces

            if (!answer.EndsWith("\"") || !answer.StartsWith("\""))
            {
                SetIndexTip(12);
                return null;
            }
            
            answer = answer.Trim('\"'); //removes the double quotes
        }
        else
        {
            SetIndexTip(22); //generic tip
        }

        return answer;
    }
    
    /* Verifies if the answer it's compilable and if it's one of options */
    bool IsValid(string answer)
    {
        answer = GetIdentifier(answer);

        if (answer == null)
            return false;
        
        if (answer == "A99" || answer == "A98" || answer == "A97")
            return true;
        
        SetIndexTip(11);
        return false;
    }

    /// </inheritdoc>
    public bool AnswerIsCorrect()
    {
        if (inputCameras == null)
            inputCameras = GameObject.FindGameObjectsWithTag("InputCamera"); //3 cameras

        for (i = 0; i < 3; i++)
        {
            TMP_InputField inputCamera = inputCameras[i].GetComponent<TMP_InputField>();

            if (inputCamera != null)
                if (!IsValid(inputCamera.text))
                    return false;
        }

        SetIndexTip(15);
        SetIndexTip(14);
        SetIndexTip(13);

        return true;
    }
}
