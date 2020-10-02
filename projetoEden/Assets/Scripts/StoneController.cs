using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();
        
        if (player != null && !WarriorController.StoneDeactivated)
        {
            StartCoroutine(ChangeRigidBodyType());
            //WarriorController.StoneDeactivated = true; //only executes once
        } 
    }

    //Makes stone fall
    IEnumerator ChangeRigidBodyType()
    {
        yield return new WaitForSeconds(1f);
        if (!WarriorController.StoneDeactivated)
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
