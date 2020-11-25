using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a mission, with methods to verify answer (or value of variable) and to execute the result.
/// </summary>
public abstract class MissionVariable : Mission
{
    /// <summary>
    /// Verifies if the toggle which indicates "const" it should be checked or not.
    /// </summary>T
    /// <param name = "isOn"> A bool, if toggle it's checked. </param>
    /// <returns> A bool, the result. </returns>
    public virtual bool ConstIdentifierIsCorrect(bool isOn) 
    {
        return !isOn; //it's not constant
    }

    /// <summary>
    /// Verifies if the type of variable it's correct.
    /// </summary>
    /// <param name = "index"> The index of dropdown obtained dynamycally. </param>
     /// <param name = "isConstant"> A bool, if the player marked constant or not. </param>
    /// <returns> A bool, the result if the type it's correct. </returns>
    public abstract bool TypeIsCorrect(int index, bool isConstant);

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

    //protected abstract void LoadCode();

    /// <summary>
    /// Executes the modifications expected after the write of code.
    /// </summary>
    /*public override void ExecuteCode()
    {
        LoadCode();
    }*/
}