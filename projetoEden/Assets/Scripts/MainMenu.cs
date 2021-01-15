using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static Vector3 lastCheckPointPosition;
    public static int lastLevel;
    public static bool tutorialExecuted;
    public static bool isSubPhase;
    public static bool debug;

    void Start()
    {
        lastCheckPointPosition = new Vector3(-30.6f, 0.9f, 0);
        lastLevel = 0;
        tutorialExecuted = false;
        isSubPhase = false;
        debug = false;
    }

    public static void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2,  LoadSceneMode.Additive);
    }

    public static void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Reset()
    {
        if (lastLevel != WarriorController.level)
        {
            lastLevel = Mathf.Clamp(WarriorController.level - 1, 0, 14);
            if (WarriorController.level == (int) WarriorController.PHASES.CAMERAS)
            {
                isSubPhase = false;
                WarriorController.isSubPhase = isSubPhase;
            }
        }
        
        SceneManager.UnloadSceneAsync(lastLevel + 2);
        SceneManager.LoadScene(lastLevel + 2, LoadSceneMode.Additive);
        MainMenu.debug = false;
        WarriorController.instance.ResetWarriorAttributes();
        UIController.instance.ResetPower();
        CodingScreen.instance.ShowGameOver(false);
        isSubPhase = WarriorController.isSubPhase;
    }

    public static void StartScene(int previousLevel)
    {
        if (previousLevel == (int) WarriorController.PHASES.FIRST_OF_VARIABLES)
            return;
        
        if (previousLevel >= (int) WarriorController.PHASES.LAST_OF_STRUCTURES)
            return;
        
        /* Sum 2 because there is the MainScene's index */
        if (SceneManager.sceneCount < previousLevel + 2)
        {                       
            SceneManager.LoadScene(previousLevel + 2, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync(previousLevel + 2);
            SceneManager.LoadScene(previousLevel + 2, LoadSceneMode.Additive);
        }
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void GoToFinal()
    {
        SceneManager.LoadScene((int) WarriorController.PHASES.END_MISSION + 1);
    }

    public static void SelectActiveScene(int i)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(i));
    }
}
