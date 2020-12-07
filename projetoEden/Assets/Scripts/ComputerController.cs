using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    Animator animator;
    AudioController audioController;
    public AudioClip explosion;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioController = gameObject.AddComponent<AudioController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();

        if (player != null && !flag)
        {
            flag = true;
            EndMission.instance.ShowPanel();
        }
    }

    public void Explode()
    {
        animator.SetTrigger("Explode");
        audioController.PlaySound(explosion);
    }

    public bool IsExploding()
    {
        return audioController.audioIsPlaying;
    }
}
