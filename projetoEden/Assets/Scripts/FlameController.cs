using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    Fade fade;
    bool debug = true; 

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        fade = gameObject.AddComponent<Fade>();
    }

    void Update() //change later
    {
        if (WarriorController.level > 2 && debug)
        {
            PutOut(); 
            debug = false;
        }
    }

    /// <summary>
    /// Puts out the flame, executing a fade out and later destrying the flame.
    /// </summary>
    public void PutOut() 
    {
        fade.FadeOut();
        StartCoroutine(HideFlame());
    }

    IEnumerator HideFlame()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(transform.position.x, -27, transform.position.z);
    }

    public void Ignite()
    {
        Vector3 playerPosition = WarriorController.instance.GetPosition();
        transform.position = new Vector3(playerPosition.x + 8, playerPosition.y, playerPosition.z); 

        fade.FadeIn();
        Invoke("PutOut", 2);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();
        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();

        if (controller != null && WarriorController.level <=2)
        {
            controller.ChangeHealth(-100);
        } 
        else if (enemy != null) 
        {
            enemy.ChangeHealth(-100);
        } 
    }
}
