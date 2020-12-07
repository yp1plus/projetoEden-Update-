using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGround : MonoBehaviour
{
    const int maxRange = 10;

    Rigidbody2D rigidbody2D;

    public float speed = 3.0f;

    public int direction = -1;

    bool changedDirection = false;
    Vector3 offset;
    float offsetGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);

        if ((rigidbody2D.position.x <= -maxRange || rigidbody2D.position.x >= maxRange) && !changedDirection)
        {
            direction = -direction;
            changedDirection = true;

            StartCoroutine(ResetChangedDirection());
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();

        if (player != null)
        {
            offset = player.GetPosition();
            offsetGround = transform.position.x;
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();

        /*if (player != null)
        {
            if (player.move == 0)
            {
                if (direction > 0)
                {
                    player.SetPosition(offset.x + Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(offsetGround)));
                }
                else
                {
                    player.SetPosition(offset.x - Mathf.Abs(Mathf.Abs(Mathf.Abs(transform.position.x) - 10) - Mathf.Abs(offsetGround)));
                }
            }
            else
            {
                offset = player.GetPosition();
                offsetGround = transform.position.x;
            }
        }*/
    }

    //Avoids the case when the position it's larger than maximum range and when changes the direction remains larger
    IEnumerator ResetChangedDirection()
    {
        yield return new WaitForSeconds(0.2f);
        changedDirection = false;
    }
}
