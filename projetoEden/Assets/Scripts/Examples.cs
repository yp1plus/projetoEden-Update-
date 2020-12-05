using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senha
{
    int quantGalinhas = 0;
    bool chamaAcesa = false;
    const char codigo = '!';
    float alturaJogador = 42.1f;
    string senha;

    void CriarSenha()
    {
        if (codigo == '!' || alturaJogador > 50)
        {
            senha = "!987654321432120000;";
        }
        else if (chamaAcesa == false)
        {
            senha = "8654321432120000";
        }

        for (int i = 0; i < 5; i++)
        {
            if (quantGalinhas > 0)
            {
                senha = senha + "false?";
                quantGalinhas = quantGalinhas - 1;
            }
            else
            {
                senha = senha + "true?";
                quantGalinhas = quantGalinhas + 1;
            }
        }

        while (chamaAcesa){
            senha = senha + "a";
            chamaAcesa = false;
        }

        senha = senha + "0.123456789123456789123456789!";
    }
}
