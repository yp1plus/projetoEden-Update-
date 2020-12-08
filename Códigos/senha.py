''' 
    Dado o fluxo de comandos, 
    descubra a senha e digite seu valor
    entre aspas duplas na caixa de texto.
    Assim, você destruirá a Inteligência Artificial,
    de uma vez por todas!
'''
def criar_senha():
    quantGalinhas = 0
    chamaAcesa = False
    codigo = '!'
    alturaJogador = 42.1
    senha = None

    if (codigo == '!' or alturaJogador > 50):
        senha = "!987654321432120000;"
    elif (chamaAcesa):
        senha = "8654321432120000"

    for i in range(5):
        if (quantGalinhas > 0):
            senha = senha + "false?"
            quantGalinhas = quantGalinhas - 1
        else:
            senha = senha + "true?"
            quantGalinhas = quantGalinhas + 1

    while (chamaAcesa):
        senha = senha + "a"
        chamaAcesa = False

    senha = senha + "0.123456789123456789123456789!"