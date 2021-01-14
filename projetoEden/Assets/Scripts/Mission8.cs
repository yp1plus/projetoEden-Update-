using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission8 : MissionStructure
{
    static int currentIndex = 0;

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

    public static bool Test()
    {
        if (WarriorController.level < 8)
            return false;
        
        switch (currentIndex)
        {
            case 1:
                GameObject flame = GameObject.FindGameObjectWithTag("Fire");
                return flame.GetComponent<FlameController>().isBurning 
                    || WarriorController.quantChickens > 0;
            case 2:
                return WarriorController.height < 50.0f;
            case 3:
                return WarriorController.name.Length == 7;
            case 4:
                return true;
            case 5:
                return false;
            default:
                return false;
        }
    }

    public override void ExecuteCode()
    {
        ;
    }
}
