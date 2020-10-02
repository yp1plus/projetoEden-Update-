using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission7 : MissionStructure
{
    int currentIndex = 0;

    public override bool StatementIsCorrect(int index)
    {
        GameObject[] conditions = GameObject.FindGameObjectsWithTag("Condition");
        conditions[1].GetComponent<TMP_Dropdown>().value = index;

        currentIndex = index;

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
