using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission5 : MissionVariable
{
    float height = 0;
    enum Returns {FALSE, TRUE, UNDEFINED};

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        WarriorController warrior = GameObject.FindGameObjectWithTag("Player").GetComponent<WarriorController>();

        warrior.ChangeHeight(true, 11);
        //warrior.speed = 4;
        //warrior.jumpForce = 1;
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        return  index == (int) Types.FLOAT && ConstIdentifierIsCorrect(isConstant);
    }

    /// </inheritdoc>
    public override bool NameIsCorrect(int index)
    {
        return  index == 5 || index == 3; //altura_jogador || _alt_jog_
    }

    byte ConvertToFloat(string str, float expectedValue)
    {
        float value;

        value = ConvertToFloat(str);

        if (Mathf.Approximately(value, expectedValue))
            return (byte) Returns.TRUE;
        
        return (byte) Returns.UNDEFINED;
    }

    float ConvertToFloat(string str)
    {
        bool canConvert;
        float value;

        canConvert = float.TryParse(str, System.Globalization.NumberStyles.Float, 
            System.Globalization.NumberFormatInfo.InvariantInfo, out value);
                
        if (!canConvert)
        {
            SetIndexTip(13);
            return (byte) Returns.FALSE;
        }

        return value;
    }

    /// </inheritdoc>
    public override bool AnswerIsCorrect(string answer)
    {
        /* 7.43 * 40.2 */
        int position = 0;
        byte result;

        if (answer != null)
        {
            position = answer.IndexOf(".");
        }
            
        if (position < 0 || answer == null) //didn't put the dot
        {
            SetIndexTip(11);
            return false;
        }

        position = answer.IndexOf("*");

        if (position < 0) //putted the value directly
        {
            result = ConvertToFloat(answer, 298.686f);
            
            if (result != (byte) Returns.UNDEFINED)
            {
                return System.Convert.ToBoolean(result);
            }
        }
        else
        {
            string firstValue = answer.Substring(0, position).Trim();
            string secondValue = answer.Substring(position + 1).Trim();

            if (firstValue == Mission4.nameVariable)
            {
                result = ConvertToFloat(secondValue, 7.43f);
            
                if (result != (byte) Returns.UNDEFINED)
                {
                    return System.Convert.ToBoolean(result);
                }
            }
            else if (secondValue == Mission4.nameVariable)
            {
                result = ConvertToFloat(firstValue, 7.43f);
            
                if (result != (byte) Returns.UNDEFINED)
                {
                    return System.Convert.ToBoolean(result);
                }
            }
            else
            {
                float first_value = ConvertToFloat(firstValue);
                float second_value = ConvertToFloat(secondValue);

                if (Mathf.Approximately(first_value * second_value, 298.686f))
                    return true;
            }
        }
        
        SetIndexTip(12);
        return false;
    }
}
