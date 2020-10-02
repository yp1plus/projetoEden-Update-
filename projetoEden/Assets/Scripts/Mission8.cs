using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission8 : MissionStructure
{
    const int tileHorizontalSize = 6;
    const int quantBlocks = 17;

    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool StatementIsCorrect(int index)
    {
        currentIndex = index;
        return index != 0;
    }
    public override void ExecuteCode()
    {
        if (currentIndex == 1)
            StartCoroutine(InstatiateBlocks(6));
        else if (currentIndex == 4)
            StartCoroutine(InstatiateBlocks(quantBlocks));
        else
        {
            StartCoroutine(InstatiateBlocks(quantBlocks));
            Debug.Log("Overflow");
        }
    }

    IEnumerator InstatiateBlocks(int quant)
    {
        GameObject block = GameObject.FindGameObjectWithTag("Block");
        GameObject gridParent = GameObject.FindGameObjectWithTag("Grid");    

        float x = 0;

        for (int i = 0; i < quant; i++)
        {
            x += tileHorizontalSize;
            Instantiate(block, new Vector3(x, 0, 0), Quaternion.identity, gridParent.transform);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
