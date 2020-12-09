using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2 : MissionVariable
{
    int indexName = 0; //which name was chosed

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        GameObject flame = GameObject.FindGameObjectWithTag("Fire");
        flame.GetComponent<FlameController>().PutOut();
        WarriorController.instance.LoadFlame(flame);
        FlameController.canBeBurnt = false;
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        switch(Languages.indexLanguage)
        {
            case (int) Languages.TypesLanguages.C:
                return index == (int) Types.INT && ConstIdentifierIsCorrect(isConstant);
            default:
                return  index == (int) Types.BOOL && ConstIdentifierIsCorrect(isConstant);
        }
    }

    /// </inheritdoc>
    public override bool NameIsCorrect(int index)
    {
        indexName = index;
        return  index == 1 || index == 3; //chamaAcesa or chamaApagada
    }

    /// </inheritdoc>
    public override bool AnswerIsCorrect(string answer)
    {
        switch(Languages.indexLanguage)
        {
            case (int) Languages.TypesLanguages.C:
                int value;
                bool canConvert = int.TryParse(answer, out value);
                
                if (!canConvert)
                {
                    SetIndexTip(11);
                    return false;
                }

                if (value == 1 && indexName == 3) //chamaApagada = true
                    return true;
                
                if (value == 0 && indexName == 1) //chamaAcesa = false
                    return true;
                
                SetIndexTip(12);
                break;
            case (int) Languages.TypesLanguages.Python:
                if (answer != "True" && answer != "False")
                {
                    SetIndexTip(13);
                    return false;
                }

                if (answer == "True" && indexName == 3) //chamaApagada = true
                    return true;
                if (answer == "False" && indexName == 1) //chamaAcesa = false
                    return true;
                
                SetIndexTip(15);
                break;
            default:
                if (answer != "true" && answer != "false")
                {
                    SetIndexTip(14);
                    return false;
                }

                if (answer == "true" && indexName == 3) //chamaApagada = true
                    return true;
                if (answer == "false" && indexName == 1) //chamaAcesa = false
                    return true;
                
                SetIndexTip(15);
                break;
        }

        return false;
    }
}
