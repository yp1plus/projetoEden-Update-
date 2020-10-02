using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3 : MissionVariable
{
    float height = 0;

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        GameObject warrior = GameObject.FindGameObjectWithTag("Player");
        warrior.GetComponent<WarriorController>().ChangeHeight(height/100f);
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        return  index == (int) Types.FLOAT && ConstIdentifierIsCorrect(isConstant);
    }

    /// </inheritdoc>
    public override bool NameIsCorrect(int index)
    {
        return  index == 5; //altura_jogador
    }

    /// </inheritdoc>
    public override bool AnswerIsCorrect(string answer)
    {
        /* 119,5 - 77,4 */
        int position = answer.IndexOf("-");
        bool canConvert;

        if (position < 0) //putted the value directly
        {
            canConvert = float.TryParse(answer, System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, out height); //allows dots and whitespaces
            
            if (!canConvert) return false;
        }
        else
        {
            string firstValue = answer.Substring(0, position);
            string secondValue = answer.Substring(position + 1);
            float first_value;
            float second_value;

            canConvert = float.TryParse(firstValue, System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, out first_value);

            if (!canConvert) return false;

            canConvert = float.TryParse(secondValue, System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, out second_value);

            if (!canConvert) return false;

            height = first_value - second_value;
        }

        return height == 42.1f;
    }
}
