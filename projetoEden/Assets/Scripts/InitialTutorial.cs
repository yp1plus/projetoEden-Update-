using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialTutorial : TipsController
{
    public GameObject background;
    public GameObject backgroundInvisible;
    public Image click;

    ///<summary> Starts a tutorial from Mission 0 </summary>
    public void StartTutorial()
    {
        boxTip.SetActive(true);
        EnableScreen(true);
        textGenerator.ShowText("Esta é sua tela de codificação!");
        StartCoroutine(ShowBackground());

        if (Languages.indexLanguage != (int) Languages.TypesLanguages.Python)
        {
            textGenerator.ShowText("Marque como constante, já que seu apelido não mudará", 4);
            StartCoroutine(GoToType());
        }

        StartCoroutine(DisableScreen());
        StartCoroutine(GoToName());
        StartCoroutine(GoToValue());
        StartCoroutine(GoOut());
    }

    ///<summary> Hides the click image when go out of the tutorial </summary>
    public void HideClick()
    {
        click.gameObject.SetActive(false);
    }

    IEnumerator GoToType()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackIncorrect[(int) CodingScreen.InputTypes.type].IsActive());

        int currentIndex = background.transform.GetSiblingIndex();

        textGenerator.ShowText("O feedback é associado ao tipo de dado");
        textGenerator.ShowText("Apelido é um conjunto de caracteres", 4);
        textGenerator.ShowText("Então escolha o tipo " + Screen.typeFromMission0 , 8);

        yield return new WaitForSeconds(10);
        background.transform.SetSiblingIndex(currentIndex - 1);
    }

    IEnumerator GoToName()
    {
        bool isPython = Languages.indexLanguage == (int) Languages.TypesLanguages.Python;
        
        if (!isPython)
            yield return new WaitUntil(() => CodingScreen.instance.feedbackCorrect[(int) CodingScreen.InputTypes.type].IsActive());

        int currentIndex = background.transform.GetSiblingIndex();

        textGenerator.ShowText("Parabéns! Agora é hora de escolher o nome da constante", isPython ? 4 : 0);
        textGenerator.ShowText("O padrão para constantes é NOME_JOGADOR", isPython ? 8 : 4);

        yield return new WaitForSeconds(isPython ? 10 : 6);
        
        //if isPython desconsiders the toggle const and the type dropdown
        background.transform.SetSiblingIndex(isPython ? currentIndex - 3 : currentIndex - 1);   
    }

    IEnumerator GoToValue()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackCorrect[(int) CodingScreen.InputTypes.name].IsActive());

        int currentIndex = background.transform.GetSiblingIndex();

        textGenerator.ShowText("Parabéns! Por último, hora de escrever o seu apelido");
        textGenerator.ShowText("Lembre-se que ele só pode ter 7 caracteres", 4);
        textGenerator.ShowText("Para avaliar seu comando, aperte ENTER após escrever", 8);
        textGenerator.ShowText("Se precisar de ajuda, recorra às dicas", 12);

        yield return new WaitForSeconds(14);
        background.transform.SetSiblingIndex(currentIndex - 2);
        background.SetActive(false);
    }

    IEnumerator GoOut()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackCorrect[(int) CodingScreen.InputTypes.value].IsActive());

        textGenerator.ShowText("Ótimo! Clique no X para sair");

        click.gameObject.SetActive(true);
    }

    IEnumerator DisableScreen()
    {
        yield return new WaitForSeconds(7f);
        EnableScreen(false);
    }

    /* Avoids the interation with screen during the presentation */
    void EnableScreen(bool state)
    {
        backgroundInvisible.SetActive(state);
    }

    IEnumerator ShowBackground()
    {
        yield return new WaitForSeconds(4f);
        background.SetActive(true);
    }
}
