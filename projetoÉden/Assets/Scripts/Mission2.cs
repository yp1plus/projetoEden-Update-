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
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index, bool isConstant)
    {
        return  index == (int) Types.BOOL && ConstIdentifierIsCorrect(isConstant);
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
        if (answer == "true" && indexName == 3) //chamaApagada = true
            return true;
        if (answer == "false" && indexName == 1) //chamaAcesa = false
            return true;
        
        return false;
    }
}
