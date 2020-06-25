using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a mission, with methods to verify answer (or value of variable) and to execute the result.
/// </summary>
public abstract class Mission : MonoBehaviour
{
    /// <summary>
    /// Verifies if the answer (or value of variable) it's correct.
    /// </summary>
    /// <param name = "answer"> A string, the answer obtained of text box. </param>
    /// <returns> A bool, the result if the answer it's correct. </returns>
    public abstract bool AnswerCorrect(string answer);

    /// <summary>
    /// Executes the modifications expected after the write of code.
    /// </summary>
    public abstract void ExecuteCode();
}
