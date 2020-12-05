/* Dado o fluxo de comandos, 
 * descubra a senha e digite seu valor
 * entre aspas duplas na caixa de texto.
 * Assim, você destruirá a Inteligência Artificial,
 * de uma vez por todas!
 */
void main(){
    int quantGalinhas = 0;
    int chamaAcesa = 0;
    const char codigo = '!';
    float alturaJogador = 42.1;
    char[200] senha;

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
        chamaAcesa = 0;
    }

    senha = senha + "0.123456789123456789123456789!";
}
