#include <stdio.h>
#include <string.h>

void igualar(char senha[200], char* str){
    strcpy(senha, str);
}

void concatenar(char senha[200], char* str){
    strcat(senha, str);
}

/* Dado o fluxo de comandos, 
 * descubra a senha e digite seu valor
 * entre aspas duplas na caixa de texto.
 * Não esquece do ;
 * Assim, você destruirá a Inteligência Artificial,
 * de uma vez por todas!
 */
void criarSenha(){
    int quantGalinhas = 0;
    int chamaAcesa = 0;
    const char codigo = '!';
    float alturaJogador = 42.1;
    char senha[200];
    

    if (codigo == '!' || alturaJogador > 50)
    {
        igualar(senha, "!987654321432120000;");
    }
    else if (chamaAcesa == 0)
    {
        igualar(senha, "8654321432120000");
    }

    for (int i = 0; i < 5; i++)
    {
        if (quantGalinhas > 0)
        {
            concatenar(senha, "false?");
            quantGalinhas = quantGalinhas - 1;
        }
        else
        {
            concatenar(senha, "true?");
            quantGalinhas = quantGalinhas + 1;
        }
    }

    while (chamaAcesa){
        concatenar(senha, "a");
        chamaAcesa = 0;
    }

    concatenar(senha, "0.123456789123456789123456789!");
}
