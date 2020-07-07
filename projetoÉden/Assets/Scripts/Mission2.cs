using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2 : Mission
{
    FlameController flame;
    int indexName = 0; //which name was chosed

    void Start()
    {
        flame = gameObject.AddComponent<FlameController>();
    }

    /// </inheritdoc>
    public override void ExecuteCode()
    {
        flame.PutOut();
    }

    /// </inheritdoc>
    public override bool TypeIsCorrect(int index)
    {
        return  index == (int) Types.BOOL;
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
