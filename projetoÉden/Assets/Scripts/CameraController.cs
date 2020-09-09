using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraController : MonoBehaviour
{
    Animator animator;
    bool isActivated;

    string originalName;

    TMP_Text inputCamera;

    public GameObject identifier; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputCamera = identifier.GetComponent<TMP_Text>();
        isActivated = true;
        originalName = GetName();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && SawSomeone())
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Camera");

            for(int i = 0; i < 3; i++) //O(3)
            {
                 CameraController camera = obj[i].GetComponent<CameraController>();

                if (camera.GetName() == originalName)
                {
                    camera.AnimateDetection();
                }
            } 
            
            isActivated = false;  //only executes once  
        }
    }

    //Considers a field of vision of two horizontal positions 
    bool SawSomeone()
    {
        double horizontalPosition = WarriorController.instance.GetPosition().x;

        //It's at least two positions apart
        return horizontalPosition <= transform.position.x + 2 && horizontalPosition >= transform.position.x - 2;
    }

    /// <summary>
    /// Gets the camera current name.
    /// </summary>
    /// <returns> A string, the camera name. </returns>
    public string GetName() 
    { 
        return inputCamera.text; 
    }

    /// <summary>
    /// Sets the camera name.
    /// </summary>
    /// <param name = "name"> The new name. </param>
    public void SetName(string name)
    {
        inputCamera.text = name;
    }

    /// <summary>
    /// Sets camera animation to enemy detected.
    /// </summary>
    public void AnimateDetection()
    {
        animator.SetTrigger("Detected");
    }
}
