/* Dado o fluxo de comandos, 
 * descubra a senha e digite seu valor
 * entre aspas duplas na caixa de texto.
 * Não esquece do ;
 * Assim, você destruirá a Inteligência Artificial,
 * de uma vez por todas!
 */
void CriarSenha()
{
    int quantGalinhas = 0;
    bool chamaAcesa = false;
    const char codigo = '!';
    double alturaJogador = 42.1;
    string senha;

    if (codigo == '!' || alturaJogador > 50)
    {
        senha = "!987654321432120000;";
    }
    else if (chamaAcesa)
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