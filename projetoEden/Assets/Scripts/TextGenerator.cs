using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    TMP_Text textBox;
    public bool executing {get; private set;} = false;

    ///<summary> Changes the text box </summary>
    ///<param name = "textBox"> A TMP_Text, the new text box </param>
    public void SetTextBox(TMP_Text textBox)
    {
        this.textBox = textBox; 
    }

    ///<summary> Shows a text in the text box after a time wait </summary>
    ///<param name = "text"> A string, the text to show </param>
    ///<param name = "timeWait"> A int, the time to wait </param>
    public void ShowText(string text, int timeWait)
    {
        if (textBox != null)
            StartCoroutine(BuildText(text, timeWait));
    }

    ///<summary> Shows a text in the text box immediately </summary>
    ///<param name = "text"> A string, the text to show </param>
    public void ShowText(string text)
    {
        if (textBox != null)
            StartCoroutine(BuildText(text, 0));
    }
    
    /* Shows the text one letter every 0.05 seconds */
    IEnumerator BuildText(string text, int timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        
        while (executing)
            yield return null;
        
        executing = true;
        textBox.SetText("");

        for (int i = 0; i < text.Length; i++){
            textBox.text = string.Concat(textBox.text, text[i]);
            yield return new WaitForSeconds(0.05f);
        }

        executing = false;
    }
}
