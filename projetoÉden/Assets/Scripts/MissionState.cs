using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class creates and reads a file JSON with the modifiable data of each mission (title, description and input names of dropdown).
/// </summary>
public class MissionState : MonoBehaviour
{
    [SerializeField] static MissionData missionData = new MissionData();

    const string path = @"C:\Users\Yure Pablo\Documents\Graduação BCC USP\IC\projetoEden-Update-\projetoÉden\Assets\Scripts\Resources";
    static string data;

    void Start()
    {
        //SaveIntoJson();
    }

    /* Function that creates a JSON with data updated on Unity Interface. It's executed only once. */
    void SaveIntoJson()
    {
        data = JsonUtility.ToJson(missionData);
        System.IO.File.WriteAllText(path + "/MissionData.json", data);
    }

    /// <summary>
    /// Loads data for the entire game from a JSON in the folder "Resources".
    /// </summary>
    /// <returns> An Object MissionData with the data </returns>
    public static MissionData LoadFromJson()
    {
        data = System.IO.File.ReadAllText(path + "/MissionData.json");
        missionData = JsonUtility.FromJson<MissionData>(data);

        return missionData;
    }

    /// <summary>
    /// Overload data from a JSON according specificies of each programming language.
    /// </summary>
    /// <param "mission"> An Object MissionData with the original data </param>
    /// <param "fileName"> A JSON with the new data </param>
    public static void OverloadFromJson(MissionData mission, string fileName)
    {
        data = System.IO.File.ReadAllText(path + "/" + fileName);
        JsonUtility.FromJsonOverwrite(data, mission);
    }
}

[System.Serializable]
public class MissionData
{
    public List<string> title = new List<string>();
    public List<string> description = new List<string>();
    /* Dropdown modificable of each mission */
    public List<OptionData> input = new List<OptionData>();
    /* Refers to level 9 */
    public List<string> options_for2 = new List<string>();
    /* Dropdown relative to data type input */
    public List<OptionData> inputTypes = new List<OptionData>();
    public List<string> resultsStructures1 = new List<string>();
    public List<string> resultsStructures2 = new List<string>();
}

[System.Serializable]
public class OptionData
{
    public List<string> options = new List<string>();
}
