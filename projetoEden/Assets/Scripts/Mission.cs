using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a mission, with methods to verify answer (or value of variable) and to execute the result.
/// </summary>
public abstract class Mission : MonoBehaviour
{
    public enum Types : ushort {STRING = 1, INT, FLOAT, CHAR, BOOL};
    protected Stack<int> tips =  new Stack<int>();
    protected HashSet<int> indexTipsAdded = new HashSet<int>();

    /// <summary>
    /// Executes the modifications expected after the write of code.
    /// </summary>
    public abstract void ExecuteCode();

    public void SetIndexTip(int i)
    {
        if (!indexTipsAdded.Contains(i))
        {
            tips.Push(i);
            indexTipsAdded.Add(i);
        }
    }

    public int GetIndexTip()
    {
        if (tips.Count != 0)
            return tips.Peek();
        else
            return -1;
    }

    public void RemoveTip()
    {
        if (tips.Count != 0)
            tips.Pop();
    }

    /// <summary>
    /// Removes string semicolon.
    /// </summary>
    /// <param name = "value"> The string, a value with semicolon. </param>
    /// <returns> A string without semicolon or null if can't remove. </returns>
    public static string RemoveSemicolon(string value)
    {
        value = value.Trim(); //removes whitespaces
        
        if (!value.EndsWith(";"))
        {
            return null;
        }
        
        int position = value.IndexOf(";");
        
        return value.Substring(0, position);
    }
}
