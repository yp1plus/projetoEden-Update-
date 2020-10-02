using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mission9 : MissionStructure
{
    Tilemap tilemap;
    ParticleController particle;

    int currentIndexI = 0;

    int currentIndexJ = 0;
    const int heightWall = 14;

    const int widthWall = 13;
    const int sizeTile = 3;

    const int firstPositionX = 702; 

    const int firstPositionY = 19;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //ExecuteCode();
    }
    public override bool StatementIsCorrect(int index)
    {
        currentIndexI = index;

        if (index != 0 && index != 4)
            return true;
        
        return false;
    }

    public bool Statement2IsCorrect(int index)
    {
        currentIndexJ = index;

        if (index == 2 || index == 3)
            return true;
        
        return false;
    }

    public override void ExecuteCode()
    {
        int height = 0, width = 0;

        switch(currentIndexI)
        {
            case 1:
                height = heightWall;
                break;
            case 2:
                height = heightWall - 1;
                break;
            case 3:
                height = 4;
                break;
            case 5:
                height = -4;
                break;
        }

        switch(currentIndexJ)
        {
            case 2:
                width = -4;
                break;
            case 3:
                width = widthWall;
                break;
        }
     
        StartCoroutine(ExecuteAnimation(height, width));
    }

    IEnumerator ExecuteAnimation(int height, int width)
    {
        tilemap = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        particle = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleController>();
        
        int constSumI = 3, constSumJ = 3; //Equivalent to size tile

        if (height < 0)
        {
            constSumI = -constSumI;
        }
            
        if (width < 0)
        {
            constSumJ = -constSumJ;
        }

        //O(14*13*9)
        //Removes every tiles row, that is, the wall
        for (int i = height < 0 ? (heightWall-1) * sizeTile : 0; height > 0 ? (i < height * sizeTile) : (i >= (heightWall - 4) * sizeTile); i += constSumI)
        {
            //Removes a tiles row
            for (int j = width < 0 ? (widthWall-1) * sizeTile : 0; width > 0 ? (j < width * sizeTile) : (j >= (widthWall - 4) * sizeTile); j += constSumJ)
            {
                particle.Move(new Vector3(j + ParticleController.firstPositionX, ParticleController.firstPositionY - i, 0));
                particle.AnimateParticle();
                yield return new WaitForSeconds(0.5f);
                
                //Removes a tile 3x3 from wall 
                for (int k = 0; k < sizeTile; k++)
                {
                    for (int l = 0; l < sizeTile; l++)
                    {
                        tilemap.SetTile(new Vector3Int(l + j + firstPositionX, firstPositionY - k - i, 0), null);
                    }
                }
            }   
        }
    }
}
