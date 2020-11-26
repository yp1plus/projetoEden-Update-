using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission4 : MissionVariable
{
    /// </inheritdoc>
    public override void ExecuteCode()
    {
      
    }

    /// </inheritdoc>
    public override bool ConstIdentifierIsCorrect(bool isOn)
    {
        return isOn;
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        return index == (int) Types.CHAR && ConstIdentifierIsCorrect(isConstant);
    }

    /// </inheritdoc>
    public override bool NameIsCorrect(int index)
    {
        return  index == 1; //CODIGO_SECRETO
    }

    /// </inheritdoc>
    public override bool AnswerIsCorrect(string answer)
    {
        /* '!' */
        answer = answer.Trim(); //removes whitespaces on the right and on the left
        if (!answer.StartsWith("'") || !answer.EndsWith("'"))
        {
            SetIndexTip(11);
            return false;
        }
        answer = answer.Trim('\''); //removes the single quotes

        if (answer.Length != 1)
        {
            SetIndexTip(12);
            return false;
        }
        
        if (answer == "!")
            return true;

        SetIndexTip(14);
        SetIndexTip(13);
        return false;
    }
}
