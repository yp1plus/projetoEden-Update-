using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformController : MonoBehaviour
{
    public static bool canPin = false;

    private void OnCollisionEnter2D(Collision2D other) {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();

        canPin = !Mission8.Test();

        if (player != null && canPin)
        {
            player.DeactivateMovement(true);
            StartCoroutine(ShowGameOver());
        }
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3f);
        CodingScreen.instance.ShowGameOver(true);
        GameObject pinForever = GameObject.FindGameObjectWithTag("SupportingText");
        TMP_Text txt = pinForever.GetComponent<TMP_Text>();
        txt.enabled = true;
        txt.text = "Você ficou preso para sempre";
    }
}
