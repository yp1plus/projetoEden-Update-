using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static Vector3 lastCheckPointPosition; //=new Vector3(32.82f, -5.9f, 0);
    public static int lastLevel = 3;
    public static bool tutorialExecuted = true;
    public static bool isSubPhase = false;
    public static bool debug = true;

    public static void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Reset()
    {
        if (lastLevel != WarriorController.level)
            lastLevel = Mathf.Clamp(WarriorController.level - 1, 0, 12);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        MainMenu.debug = false;
        isSubPhase = WarriorController.isSubPhase;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
