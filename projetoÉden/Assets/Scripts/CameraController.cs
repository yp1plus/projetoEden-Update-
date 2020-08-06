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
            if (originalName == GetName())
                animator.SetTrigger("Detected");
            else
            {
                GameObject obj = GameObject.Find(originalName);
                if (obj != null)
                {
                    CameraController camera = obj.GetComponent<CameraController>();
                    camera.ForceDetection();
                }
            }

            isActivated = false;
        }
    }

    bool SawSomeone()
    {
        double horizontalPosition = WarriorController.instance.GetPosition().x;

        //It's at least two positions apart
        return horizontalPosition <= transform.position.x + 2 && horizontalPosition >= transform.position.x - 2;
    }

    public string GetName() 
    { 
        return inputCamera.text; 
    }

    public void SetName(string name)
    {
        inputCamera.text = name;
        gameObject.name = name;
    }

    public void ForceDetection()
    {
        animator.SetTrigger("Detected");
    }
}
