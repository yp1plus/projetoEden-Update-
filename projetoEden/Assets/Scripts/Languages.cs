using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Languages : MonoBehaviour
{
    static int index = 0;
    public static int indexLanguage {get {return index;}}
    public enum TypesLanguages {C, CSharp, Python, Java};
    public List<OptionData> inputTypes = new List<OptionData>();

    public static bool isPython()
    {
        return index == (int) TypesLanguages.Python;
    }

    public void ClickC()
    {
        index = (int) TypesLanguages.C;
    }

    public void ClickPython()
    {
        index = (int) TypesLanguages.Python;
    }

    public void ClickJava()
    {
        index  = (int) TypesLanguages.Java;
    }

    public void ClickCSharp()
    {
        index  = (int) TypesLanguages.CSharp;
    }
}
