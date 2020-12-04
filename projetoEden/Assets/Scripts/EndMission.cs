using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        WarriorController player = other.gameObject.GetComponent<WarriorController>();

        if (player != null)
        {
            //MainMenu.GoToFinal();
        }
    }


}

/*public class Example : MonoBehaviour
{
    int quantGalinhas = 0;
    bool chamaAcesa = false;
    const char codigo = '!';
    float alturaJogador = 42.1f;
    string camera_A99 = "A99";
    string camera_A98 = "A98";
    string camera_A97 = "A97";

    if (chamaAcesa == false)
    {
        //PararBloco();
    }

    //se ficar || correr

    while (houverVacuo()){
        //CriarBloco();
    }

    for (int i = 0; i < 7; i++){
        for (int j = 0;  j< 12; j++){
            //DestruirBloco(i,j);
        }
    }

    //dar o resultado de quanto vai dar a senha depois de passar pelo código

}*/