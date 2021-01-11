using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission8 : MissionStructure
{
    int currentIndex = 0;

    public override bool StatementIsCorrect(int index)
    {
        GameObject[] conditions = GameObject.FindGameObjectsWithTag("Condition");
        conditions[1].GetComponent<TMP_Dropdown>().value = index;

        currentIndex = index;
        
        SetIndexTip(16);
        SetIndexTip(15);

        if (index >= 1)
            SetIndexTip(index + 10);

        return true;
    }

    public override void ExecuteCode()
    {
        BugController bug = GameObject.FindGameObjectWithTag("Bug").GetComponent<BugController>();

        bug.MakeVisible();

        if (currentIndex == 3) //correct option
        {
            bug.GetComponent<BugController>().DecreaseSpeed();
        }
    }
}
