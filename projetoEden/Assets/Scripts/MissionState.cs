using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class creates and reads a file JSON with the modifiable data of each mission (title, description and input names of dropdown).
/// </summary>
public class MissionState : MonoBehaviour
{
    [SerializeField] static MissionData missionData = new MissionData();
    [SerializeField] static UIInfo infoUI = new UIInfo();
    const string path = @"C:\Users\Yure Pablo\Documents\projetoEden-Update-\projetoEden\Assets\Resources";
    static TextAsset jsonTextFile;
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
        jsonTextFile = Resources.Load<TextAsset>("MissionData");
        data = jsonTextFile.ToString();
        missionData = JsonUtility.FromJson<MissionData>(data);

        return missionData;
    }

    public static UIInfo LoadUIFromJson()
    {
        jsonTextFile = Resources.Load<TextAsset>("UIInfo");
        data = jsonTextFile.ToString();
        infoUI = JsonUtility.FromJson<UIInfo>(data);
        
        return infoUI;
    }

    /// <summary>
    /// Overload data from a JSON according specificies of each programming language.
    /// </summary>
    /// <param "mission"> An Object MissionData with the original data </param>
    /// <param "fileName"> A JSON with the new data </param>
    public static void OverloadFromJson(MissionData mission, string fileName)
    {
        jsonTextFile = Resources.Load<TextAsset>(fileName);
        data = jsonTextFile.ToString();
        JsonUtility.FromJsonOverwrite(data, mission);
    }
}


[System.Serializable]
public class UIInfo
{
    public List<string> title = new List<string>();
    public List<string> description = new List<string>();

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
    public List<Tip> tips = new List<Tip>();
    public List<string> genericTips = new List<string>();
    public List<string> titleInfo = new List<string>();
    public List<string> descriptionInfo = new List<string>();
}

[System.Serializable]
public class Tip
{
    public List<string> tipsForTypes = new List<string>();
    public List<string> tipsForNames = new List<string>();
    public List<string> tipsForValue = new List<string>();
}

[System.Serializable]
public class OptionData
{
    public List<string> options = new List<string>();
}
