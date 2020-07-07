using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a mission, with methods to verify answer (or value of variable) and to execute the result.
/// </summary>
public abstract class Mission : MonoBehaviour
{
    protected enum Types : ushort {STRING = 1, INT, FLOAT, BOOL, CHAR};
    
    /// <summary>
    /// Verifies if the type of variable it's correct.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    /// <returns> A bool, the result if the type it's correct. </returns>
    public abstract bool TypeIsCorrect(int index);

    /// <summary>
    /// Verifies if the name of variable it's correct.
    /// </summary>
    /// <remarks> Might be more than one name correct on dropdown. </remarks>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
    /// <returns> A bool, the result if the name it's correct. </returns>
    public abstract bool NameIsCorrect(int index);

    /// <summary>
    /// Verifies if the answer (or value of variable) it's correct.
    /// </summary>
    /// <param name = "answer"> A string, the answer obtained of text box. </param>
    /// <returns> A bool, the result if the answer it's correct. </returns>
    public abstract bool AnswerIsCorrect(string answer);

    /// <summary>
    /// Executes the modifications expected after the write of code.
    /// </summary>
    public abstract void ExecuteCode();
}
