using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class to shoot a flying chicken and control collision with it.
/// </summary>

public class ChickenShoot : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Renderer renderer;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnBecameInvisible()
    {
        if (rigidbody2D.position.y < -20)
            return;
        
        if (gameObject == null || gameObject.activeSelf)
        {
            WarriorController.instance.SubtractChicken();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Launch a chicken in a direction and with a specific force.
    /// </summary>
    /// <param name = "direction"> A Vector2 direction. </param>
    /// <param name = "force"> A float number which represents a force of shoot. </param>
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);

        /* Flip the Chicken when it's necessary */
        Vector3 transformScale = transform.localScale;
        transformScale.x *= direction.x;
        transform.localScale = transformScale;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider (2D physics only).
    /// </summary>
    /// <remarks> Decreases life of enemies in fifty units. </remarks>
    /// <param name="other">The Collision2D data associated with this collision (only enemies).</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();

        if (e != null)
        {
            e.ChangeHealth(-e.hitChicken);
        }

        Destroy(gameObject);
        WarriorController.instance.SubtractChicken();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
        WarriorController.instance.SubtractChicken();
    }
}
