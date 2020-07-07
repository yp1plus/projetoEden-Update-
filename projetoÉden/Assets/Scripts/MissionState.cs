using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class creates and reads a file JSON with the modifiable data of each mission (title, description and input names of dropdown).
/// </summary>
public class MissionState : MonoBehaviour
{
    [SerializeField] MissionData missionData = new MissionData();

    static string path = @"C:\Users\Yure Pablo\Documents\Graduação BCC USP\IC\projetoEden-Update-\projetoÉden\Assets\Scripts\Resources";
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
        return JsonUtility.FromJson<MissionData>(data);
    }
}

[System.Serializable]
public class MissionData
{
    public List<string> title = new List<string>();
    public List<string> description = new List<string>();
    public List<OptionData> inputName = new List<OptionData>();
}

[System.Serializable]
public class OptionData
{
    public List<string> optionsName = new List<string>();
}
