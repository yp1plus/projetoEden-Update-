﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission4 : MissionVariable
{
    float height = 0;

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        GameObject warrior = GameObject.FindGameObjectWithTag("Player");
        warrior.GetComponent<WarriorController>().ChangeHeight(1/(Mathf.Pow(1.2f, 5)));
        warrior.GetComponent<WarriorController>().speed = 15;
        warrior.GetComponent<WarriorController>().jumpForce = 17;
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
        /* 119,5 - 79,3 */
        int position = 0;
        bool canConvert;

        if (answer != null)
            position = answer.IndexOf(".");
            
        if (position < 0 || answer == null) //didn't put the dot
        {
            SetIndexTip(11);
            return false;
        }

        position = answer.IndexOf("-");

        if (position < 0) //putted the value directly
        {
            canConvert = float.TryParse(answer, System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, out height); //allows dots and whitespaces
            
            if (!canConvert)
            {
                SetIndexTip(13);
                return false;
            }
                
        }
        else
        {
            string firstValue = answer.Substring(0, position);
            string secondValue = answer.Substring(position + 1);
            float first_value;
            float second_value;

            canConvert = float.TryParse(firstValue, System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, out first_value);

            if (!canConvert)
            {
                SetIndexTip(13);
                return false;
            }

            Debug.Log(first_value);

            canConvert = float.TryParse(secondValue, System.Globalization.NumberStyles.Float, 
                    System.Globalization.NumberFormatInfo.InvariantInfo, out second_value);

            if (!canConvert)
            {
                SetIndexTip(13);
                return false;
            }

            Debug.Log(second_value);

            height = first_value - second_value;

            Debug.Log(height);
        }

        if (Mathf.Approximately(height, 40.2f))
            return true;
        
        SetIndexTip(12);
        return false;
    }
}
