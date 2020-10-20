using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    Animator animator;
    Renderer renderer;

    bool isEnabled;

    public const float firstPositionX = 703.47f;
    public const float firstPositionY = 18.47f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PlayerController.AnimatorIsPlaying(animator))
        {
            if (!renderer.enabled)
                renderer.enabled = true;
        }
        else
        {
            if (renderer.enabled)
                renderer.enabled = false;
        }*/
    }

    /// <summary>
    /// Restarts the particle animation and shows the particle on screen.
    /// </summary>
    public void AnimateParticle()
    {
        animator.SetTrigger("Activated");
        animator.Play("Particle", -1, 0f); //reset animation
        renderer.enabled = true;
    }

    /// <summary>
    /// Changes the particle position.
    /// </summary>
    /// <param name = "position"> A Vector3, the new position. </param>
    public void Move(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Disable()
    {
         renderer.enabled = false;
    }
}
