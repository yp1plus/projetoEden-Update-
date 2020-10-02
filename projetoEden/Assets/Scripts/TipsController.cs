using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    public static TipsController instance {get; private set;}
    public GameObject boxTip;
    
    public TMP_Text dialogBox;
    protected Fade fade;
    protected TextGenerator textGenerator;
    int[] valueTips = {1, 3, 5};
    bool[] purchased = {false, false, false};
    bool executing = false;

    void Awake()
    {
        fade = gameObject.AddComponent<Fade>();
        textGenerator = gameObject.AddComponent<TextGenerator>();
        instance = this;
        textGenerator.SetTextBox(dialogBox);
    }

    ///<summary> Shows a error message when the player clicks on the button exit with wrong fields yet </summary>
    public void ShowMessageExitError()
    {
        boxTip.SetActive(true);
        StartCoroutine(ShowMessage("Você só pode sair quando tudo estiver correto"));
    }

    ///<summary> Shows a tip when press the button </summary>
    ///<param name = "i"> A int, the 0 <= index <= 2 </param>
    public void ShowTip(int i)
    {
        if (i >= 3 || textGenerator.executing)
            return;
            
        if (!boxTip.activeSelf)
        {
            boxTip.SetActive(true);
            fade.FadeIn();
        }    
        
        if (WarriorController.level != 0 && !purchased[i] && !WarriorController.instance.RemoveCoins(valueTips[i]))
        {
            StartCoroutine(ShowMessage("Você não tem moedas suficientes ):"));
            return;
        }
          
        textGenerator.ShowText(CodingScreen.tips[i]);
        purchased[i] = true;
    }

    ///<summary> Reset the vector purchased of tips to false </summary>
    public void ResetTips()
    {
        for (int i = 0;  i < 3; i++)
            purchased[i] = false;
    }

    IEnumerator ShowMessage(string message)
    {
        while (executing)
            yield return null;
        
        executing = true;
        fade.FadeIn();
        textGenerator.ShowText(message);
        yield return new WaitForSeconds(3f);
        fade.FadeOut();
        yield return new WaitForSeconds(0.3f);
        SkipBoxTip();
        executing = false;
    }

    ///<summary> Hides the box tip </summary>
    public void SkipBoxTip()
    {
        boxTip.SetActive(false);
    }
}
