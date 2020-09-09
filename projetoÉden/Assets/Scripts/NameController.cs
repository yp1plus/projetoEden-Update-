using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameController : MonoBehaviour
{
    Vector3 positionReferenceWarrior;
    Vector3 positionReferenceName;
    public GameObject warrior;
    WarriorController warriorController;
    Vector3 transformPosition;

    // Start is called before the first frame update
    void Awake()
    {
        warriorController = warrior.GetComponent<WarriorController>();
        positionReferenceWarrior = warriorController.GetPosition();
        transformPosition = transform.position;
        positionReferenceName= transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transformPosition.x = (warriorController.GetPosition().x - positionReferenceWarrior.x) + positionReferenceName.x;
        transformPosition.y = (warriorController.GetPosition().y - positionReferenceWarrior.y) + positionReferenceName.y;

        transform.position = transformPosition; 
    }
}
