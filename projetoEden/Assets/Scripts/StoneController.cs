using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        
        if (player != null && (WarriorController.StoneDeactivated || flag))
        {
            flag = true;
        }
        else if (player != null)
        {
            StartCoroutine(ChangeRigidBodyType());
        }
    }

    //Makes stone fall
    IEnumerator ChangeRigidBodyType()
    {
        yield return new WaitForSeconds(1f);
        if (!WarriorController.StoneDeactivated)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}
