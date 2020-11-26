using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject[] barrier;
    public bool barrierActivated {get; private set;}
    bool phaseIsFinished = false;

    public static BarrierController instance {get; private set;}
    
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController warrior = other.gameObject.GetComponent<WarriorController>();
        
        if (warrior != null && !phaseIsFinished)
        {
            barrierActivated = true;
            for (int i = 0; i < barrier.Length; i++)
            {
                barrier[i].SetActive(true);
            }
        }
    }

    public void ResetAttributes()
    {
        barrierActivated = false;
        phaseIsFinished = true;
        for (int i = 0; i < barrier.Length; i++)
        {
            barrier[i].SetActive(false);
        }
    }
}
