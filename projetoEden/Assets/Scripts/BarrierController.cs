using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject[] barrier;
    public bool barrierActivated {get; private set;}
    bool phaseIsFinished = false;
    bool trigger = false;
    public AudioClip battleSong;

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
        
        if (warrior != null && !phaseIsFinished && !trigger)
        {
            UIController.instance.ShowNewInfo(5);
            WarriorController.instance.audioController.PlayMusic(battleSong);
            trigger = true;
            barrierActivated = true;
            for (int i = 0; i < barrier.Length; i++)
            {
                if (barrier!= null && barrier[i] != null)
                    barrier[i].SetActive(true);
            }

            if (WarriorController.level != (int) WarriorController.PHASES.BATTLE)
            {
                SmallBoss.instance.DecreaseHit();
                GameObject[] blueEnemies = GameObject.FindGameObjectsWithTag("BlueEnemy");
                for (int i = 0; i < blueEnemies.Length; i++)
                {
                    blueEnemies[i].GetComponent<BlueEnemy>().DecreaseHit();
                }
            }
        }
    }

    public void ResetAttributes()
    {
        barrierActivated = false;
        phaseIsFinished = true;
        for (int i = 0; i < barrier.Length; i++)
        {
            if (barrier!= null && barrier[i] != null)
                barrier[i].SetActive(false);
        }
    }
}
