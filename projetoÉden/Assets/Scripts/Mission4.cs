using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission4 : MissionVariable
{
    /// </inheritdoc>
    public override void ExecuteCode()
    {
        GameObject warrior = GameObject.FindGameObjectWithTag("Player");
        //warrior.GetComponent<WarriorController>().ChangeHeight(height/100f);
    }

    /// </inheritdoc>
    protected override bool ConstIdentifierIsCorrect(bool isOn)
    {
        return isOn; //It's constant
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        return  index == (int) Types.CHAR && ConstIdentifierIsCorrect(isConstant);
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
        answer = answer.Trim('\''); //removes the single quotes
        return answer == "!";
    }
}
