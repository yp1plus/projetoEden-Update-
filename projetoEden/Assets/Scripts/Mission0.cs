using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission0 : MissionVariable
{
    string playerName;

    protected override bool ConstIdentifierIsCorrect(bool isOn)
    {
        return isOn;
    }

    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        return index == (int) Types.STRING && ConstIdentifierIsCorrect(isConstant);
    }
    public override bool NameIsCorrect(int index)
    {
        return index == 3; //NOME_JOGADOR
    }

    public override bool AnswerIsCorrect(string answer)
    {
        answer = answer.Trim(); //removes whitespaces on the right and on the left
        if (!answer.EndsWith("\"") || !answer.StartsWith("\""))
                return false;
        answer = answer.Trim('\"'); //removes the single quotes

        if (answer.Length > 30)
            return false;

        playerName = answer;

        return true;
    }
    public override void ExecuteCode()
    {
        TMP_Text name = GameObject.FindGameObjectWithTag("Name").GetComponent<TMP_Text>();
        name.text = playerName;

        /* click image just appears in the initial tutorial */
        GetComponent<InitialTutorial>().HideClick();
    }
}
