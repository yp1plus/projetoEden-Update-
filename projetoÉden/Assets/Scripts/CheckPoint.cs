using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    bool triggered;


    void Awake()
    {
        triggered = false;
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null && !triggered)
        {
            CodingScreen.instance.OpenPanel(true);
            triggered = true;
            controller.GoToNextLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
