using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission8 : MissionStructure
{
    int currentIndex = 0;
    GameObject flame;
    bool flag = false;

    // Update is called once per frame
    void Update()
    {
        
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
      
    }
}
