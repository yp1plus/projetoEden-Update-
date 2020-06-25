using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionState : MonoBehaviour
{
    [SerializeField] MissionData missionData = new MissionData();

    static string path = @"C:\Users\Yure Pablo\Documents\Graduação BCC USP\IC\projetoEden-Update-\projetoÉden\Assets\Scripts\Resources";
    static string data;

    void Start()
    {
        SaveIntoJson();
    }

    public void SaveIntoJson()
    {
        data = JsonUtility.ToJson(missionData);
        System.IO.File.WriteAllText(path + "/MissionData.json", data);
    }

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
