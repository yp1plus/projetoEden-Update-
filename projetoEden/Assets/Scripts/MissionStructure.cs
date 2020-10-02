using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a mission, with methods to verify answer (or value of variable) and to execute the result.
/// </summary>
public abstract class MissionStructure : Mission
{
    public abstract bool StatementIsCorrect(int index);
}