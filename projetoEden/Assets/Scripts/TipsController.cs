﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    public static TipsController instance {get; private set;}
    public GameObject boxTip;
    
    public TMP_Text dialogBox;
    public TMP_Text txtValueTip;
    protected Fade fade;
    protected TextGenerator textGenerator;
    int valueTip = 1;
    string currentTip = null;
    bool executing = false;

    void Awake()
    {
        fade = gameObject.AddComponent<Fade>();
        textGenerator = gameObject.AddComponent<TextGenerator>();
        instance = this;
        textGenerator.SetTextBox(dialogBox);
        if (txtValueTip != null)
            txtValueTip.text = valueTip.ToString();
    }

    ///<summary> Shows a error message when the player clicks on the button exit with wrong fields yet </summary>
    public void ShowMessageExitError()
    {
        boxTip.SetActive(true);
        StartCoroutine(ShowMessage("Você só pode sair quando tudo estiver correto"));
    }
    

    ///<summary> Shows a tip when press the button </summary>
    public void ShowTip()
    {
        if ((executing || textGenerator.executing) && boxTip.activeSelf)
            return;
            
        if (!boxTip.activeSelf)
        {
            executing = false;
            textGenerator.executing = false;
            boxTip.SetActive(true);
            fade.FadeIn();
        }

        int indexTip = CodingScreen.instance.GetIndexTip();

        if (indexTip == -1)
        {
            StartCoroutine(ShowMessage("Você não fez nenhuma tentativa ainda."));
            return;
        }

        if (WarriorController.level != 0 && !WarriorController.instance.RemoveCoins(valueTip))
        {
            StartCoroutine(ShowMessage("Você não tem moedas suficientes ):"));
            return;
        }

        string tip = null;

        if (indexTip >= 0 && indexTip <= (int) CodingScreen.TipsReferenceIndexes.types) //type
        {
            //Doesn't need this tip anymore
            if (CodingScreen.instance.FeedbackCorrectIsActive((int) CodingScreen.InputTypes.type))
            {
                CodingScreen.instance.RemoveTip();
                ShowTip();
                return;
            }

            if (Screen.tip.tipsForTypes.Count != 0)
                tip = Screen.tip.tipsForTypes[indexTip];
        }
        else if (indexTip <= (int) CodingScreen.TipsReferenceIndexes.names) //name
        {
            //Doesn't need this tip anymore
            if (CodingScreen.instance.FeedbackCorrectIsActive((int) CodingScreen.InputTypes.name))
            {
                CodingScreen.instance.RemoveTip();
                ShowTip();
                return;
            }

            int typesFinalIndex = (int) CodingScreen.TipsReferenceIndexes.types;
            if (Screen.tip.tipsForNames.Count != 0)
                tip = Screen.tip.tipsForNames[indexTip - (typesFinalIndex + 1)];
        }
        else //value
        {
            //Doesn't need this tip anymore
            if (CodingScreen.instance.FeedbackCorrectIsActive((int) CodingScreen.InputTypes.value))
            {
                CodingScreen.instance.RemoveTip();
                ShowTip();
                return;
            }

            int namesFinalIndex = (int) CodingScreen.TipsReferenceIndexes.names;
            if (indexTip == (int) CodingScreen.TipsReferenceIndexes.generics) //generic tip
            {
                tip = Screen.genericTips[0];
            }
            else if (Screen.tip.tipsForValue.Count > (indexTip - (namesFinalIndex + 1)))
                    tip = Screen.tip.tipsForValue[indexTip - (namesFinalIndex + 1)];
        }

        if (tip != null && tip != "")
        {
            CodingScreen.instance.RemoveTip();
            currentTip = tip;
        }
        else
        {
            //Ignores empty tip
            if (tip != null)
            {
                CodingScreen.instance.RemoveTip();
                ShowTip();
                return;
            }
        }
        
        textGenerator.ShowText(currentTip);
        valueTip += 2;
        if (txtValueTip != null)
            txtValueTip.text = valueTip.ToString();
    }

    ///<summary> Reset the vector purchased of tips to false </summary>
    public void ResetTip()
    {
        valueTip = 1;
        SkipBoxTip();
        if (txtValueTip != null)
            txtValueTip.text = valueTip.ToString();
    }

    IEnumerator ShowMessage(string message)
    {
        while ((executing || textGenerator.executing))
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
    private void SkipBoxTip()
    {
        boxTip.SetActive(false);
    }
}
