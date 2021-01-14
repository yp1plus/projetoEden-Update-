using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mission11 : MissionStructure
{
    int currentIndex = 0;
    int quant = 0;

    public override bool StatementIsCorrect(int index)
    {
        currentIndex = index;

        if (index >= 1)
        {
            SetIndexTip(index + 10);
        }

        if (index == 5)
            return false;
        
        return true;
    }

    public override void ExecuteCode()
    {
        switch(currentIndex)
        {
            case 1:
            case 3:
                quant = 8;
                break;
            case 2:
                quant = 7;
                break;
            case 4:
                quant = 4;
                break;
        }
     
        StartCoroutine(ExecuteAnimation());
    }

    IEnumerator ExecuteAnimation()
    {
        GameObject[] bladeBarriers = GameObject.FindGameObjectsWithTag("BladeBarrier");
        
        for (int i = 0; i < quant && i < bladeBarriers.Length; i++)
        {
            Destroy(bladeBarriers[i]);
                
            yield return new WaitForSeconds(0.3f);
        }
    }
}
