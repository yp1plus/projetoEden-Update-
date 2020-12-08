using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMission : MonoBehaviour
{
    public static EndMission instance;
    public GameObject panel;
    public GameObject panelGameWin;
    public Image feedbackCorrect;
    public Image feedbackIncorrect;
    public GameObject computer;
    ComputerController computerController;
    AudioController audioController;
    public AudioClip winSong;
    public Sprite[] codes = new Sprite[4];
    public Image content;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        computerController = computer.GetComponent<ComputerController>();
        audioController = gameObject.AddComponent<AudioController>();
        content.sprite = codes[Languages.indexLanguage];
    }

    public void CheckValue(string answer)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log(answer);
            answer = answer.Trim(); //removes whitespaces on the right and on the left
            if (Languages.indexLanguage != (int) Languages.TypesLanguages.Python)
                answer = Mission.RemoveSemicolon(answer);
            answer = answer.Trim('\"'); //removes the single quotes
            Debug.Log(answer);
            if (answer == "!987654321432120000;true?false?true?false?true?0.123456789123456789123456789!")
            {
                IsCorrect();
                StartCoroutine(HidePanel());
                ExecuteCode();
            }
            else
                IsWrong();
        }   
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        WarriorController.instance.DeactivateMovement(true);
    }

    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(0.1f);
        panel.SetActive(false);
        WarriorController.instance.DeactivateMovement(false);
    }

    IEnumerator ShowGameWin()
    {
        yield return new WaitForSeconds(0.7f);
        yield return new WaitUntil(() => !computerController.IsExploding());

        panelGameWin.SetActive(true);
        WarriorController.instance.DeactivateMovement(true);
        audioController.PlaySound(winSong);

        yield return new WaitUntil(() => !audioController.audioIsPlaying);
        MainMenu.Restart();
    }

    void ExecuteCode()
    {
        computerController.Explode();
        StartCoroutine(ShowGameWin());
    }

     // Actives the feedback correct and disables the feedback incorrect if necessary.
    void IsCorrect()
    {
        feedbackCorrect.gameObject.SetActive(true);
        feedbackIncorrect.gameObject.SetActive(false);
    }

    // Actives the feedback incorrect and disables the feedback correct if necessary.
    void IsWrong()
    {
        feedbackIncorrect.gameObject.SetActive(true);
        feedbackCorrect.gameObject.SetActive(false);
    }


}