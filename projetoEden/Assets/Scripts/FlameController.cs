using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour
{
    Fade fade;
    bool debug = true; 

    public static bool canBeBurnt;
    public bool isBurning {get; private set;} = true; 

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        fade = gameObject.AddComponent<Fade>();
    }

    void Start()
    {
        canBeBurnt = true;
    }

    void Update() //change later
    {
        if (WarriorController.level > 2 && debug)
        {
            Debug.Log("Flame");
            GameObject flame = GameObject.FindGameObjectWithTag("Fire");
            WarriorController.instance.LoadFlame(flame);
            canBeBurnt = false;
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
        isBurning = false;
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
        if (WarriorController.instance.transform.localScale.x < 0)
            transform.position = new Vector3(playerPosition.x - 8, playerPosition.y, playerPosition.z);
        else
            transform.position = new Vector3(playerPosition.x + 8, playerPosition.y, playerPosition.z);

        fade.FadeIn();
        isBurning = true;
        Invoke("PutOut", 2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController controller = other.GetComponent<WarriorController>();

        if (controller != null && canBeBurnt)
        {
            controller.ChangeHealth(-100);
        } 
    }
}
