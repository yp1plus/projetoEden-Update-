﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a mission, with methods to verify answer (or value of variable) and to execute the result.
/// </summary>
public abstract class Mission : MonoBehaviour
{
    protected enum Types : ushort {STRING = 1, INT, FLOAT, BOOL, CHAR};

    /// <summary>
    /// Executes the modifications expected after the write of code.
    /// </summary>
    public abstract void ExecuteCode();

    /// <summary>
    /// Removes string semicolon.
    /// </summary>
    /// <param name = "value"> The string, a value with semicolon. </param>
    /// <returns> A string without semicolon or null if can't remove. </returns>
    public static string RemoveSemicolon(string value)
    {
        int position = value.IndexOf(";");

        if (position < 0)
            return null;
        
        return value.Substring(0, position);
    }
}
