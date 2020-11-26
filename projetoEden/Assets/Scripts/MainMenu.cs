using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static Vector3 lastCheckPointPosition = new Vector3(-30.6f, 0.9f, 0);
    public static int lastLevel = 3;
    public static bool tutorialExecuted = false;
    public static bool isSubPhase = false;
    public static bool debug = false;

    public static void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2,  LoadSceneMode.Additive);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void Reset()
    {
        if (lastLevel != WarriorController.level)
        {
            //if (WarriorController.level == 4) lastLevel = 4;
            lastLevel = Mathf.Clamp(WarriorController.level - 1, 0, 12);
            if (WarriorController.level == 5)
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
        if (previousLevel == 0)
            return;
        
        if (previousLevel == 9)
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

    public static void SelectActiveScene(int i)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(i));
    }
}
