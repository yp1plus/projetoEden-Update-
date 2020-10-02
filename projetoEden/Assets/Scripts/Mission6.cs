using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission6 : MissionStructure
{
    int currentIndex = 0;
    GameObject flame;

    // Update is called once per frame
    void Update()
    {
        if (flame != null)
        {
            if (flame.GetComponent<Renderer>().material.color.a > 0.05f) //It's not transparent
            {
                WarriorController.StoneDeactivated = true;
            }
            else
            {
                WarriorController.StoneDeactivated = false;
            }
        }
    }

    public override bool StatementIsCorrect(int index)
    {
        currentIndex = index;
        return index >= 1;
    }

    public override void ExecuteCode()
    {
        if (currentIndex == 1)
        {
            flame = GameObject.FindGameObjectWithTag("Fire");
        }
    }
}
