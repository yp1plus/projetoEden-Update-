using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission6 : MissionStructure
{
    int currentIndex = 0;
    GameObject flame;
    bool flag = false;

    // Update is called once per frame
    void Update()
    {
        if (flame != null && WarriorController.level == 6)
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

        if (flag && WarriorController.level == 6)
        {
            if (WarriorController.instance.quantChickens >= 5)
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

        if (index >= 1)
        {
            SetIndexTip(index + 10);
            return true;
        }
        
        return false;
    }

    public override void ExecuteCode()
    {
        if (currentIndex == 1)
        {
            flame = GameObject.FindGameObjectWithTag("Fire");
            flag = false;
        }
        else if (currentIndex == 2)
        {
            flag = true;
            flame = null;
        }
    }
}
