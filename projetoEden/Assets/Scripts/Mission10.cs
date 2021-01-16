using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission10 : MissionStructure
{
    const int tileHorizontalSize = 6;
    const int quantBlocks = 17;
    int currentIndex = 0;
    GameObject enemies;

    void Awake()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemies");
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
            StartCoroutine(InstatiateBlocks(6));
        else if (currentIndex == 4)
            StartCoroutine(InstatiateBlocks(quantBlocks));
        else
        {
            StartCoroutine(InstatiateBlocks(quantBlocks));
            StartCoroutine(ShowGameOver());
        }
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3f);
        CodingScreen.instance.ShowGameOver(true);
        GameObject infiniteLoop = GameObject.FindGameObjectWithTag("SupportingText");
        TMP_Text txt = infiniteLoop.GetComponent<TMP_Text>();
        txt.enabled = true;
        txt.text = "O jogo travou por loop infinito";
    }

    IEnumerator InstatiateBlocks(int quant)
    {
        GameObject block = GameObject.FindGameObjectWithTag("Block");
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        MainMenu.SelectActiveScene((int) WarriorController.PHASES.EMPTY_PATH + 1);
        //GameObject gridParent = GameObject.FindGameObjectWithTag("Grid");    

        float x = 0;

        for (int i = 0; i < quant; i++)
        {
            x += tileHorizontalSize;
            //Instantiate(block, new Vector3(x, 0, 0), Quaternion.identity, gridParent.transform);
            if (block != null)
            {
                Instantiate(block, new Vector3(x, 0, 0), Quaternion.identity);
            } else {
               yield break; //see later
            }
        
            yield return new WaitForSeconds(0.2f);
        }

        if (enemies != null)
            enemies.transform.GetChild(0).gameObject.SetActive(true);

        MainMenu.SelectActiveScene(1);
    }
}
