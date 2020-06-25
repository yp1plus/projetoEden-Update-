using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In this mission, chickens can be destroyed or created.
/// </summary>
public class Mission1 : Mission
{
    int value = 0;

    /// <summary>
    /// Creates n chickens, besides the chickens pre-existing.
    /// </summary>
    /// <param name = "n"> A value int, number of chickens. </param>
    void CreateChickens(int n)
    {
        GameObject chicken = GameObject.FindGameObjectWithTag("Chicken");
        float x = 11.9F, y = -8F; //arbitrary values based on the scene

        for (int i = 0; i < n; i++)
        {
             if (i > 500) //avoids overflow
             {
                break;
             }
             Instantiate(chicken, new Vector3(x, y, 0), Quaternion.identity);
             if (y < 3.8) y += 2.9F;
             else 
             {
                 x += 3.7F; 
                 y = -8F;
             }
        }
    }

    /// <summary>
    /// Destroys n chickens, between the 30 existing.
    /// </summary>
    /// <param name = "n"> A value int, number of chickens. </param>
    void DestroyChickens(int n)
    {
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");

        for(int i = 29; i >= n; i--)
        {
            GameObject.Destroy(chickens[i]);
        }
    }

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        if (value <= 30)
            DestroyChickens(value);
        else
            CreateChickens(value - 30);
    }

    /// </inheritdoc>
    public override bool AnswerCorrect(string answer)
    {
        bool canConvert = int.TryParse(answer, out value);
        if (canConvert)
        {
            if (value >= 0) return true;
        }
        
        return false;
    }
}
