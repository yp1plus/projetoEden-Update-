using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    public GameObject codingScreen;
    public GameObject variablesPhasesComponents;
    public GameObject camerasPhaseComponents;
    public GameObject structuresPhasesComponents;
    MissionData missionData; 

    [System.Serializable]
    public struct VariablesPhase
    {
        public GameObject constIdentifier;
        public GameObject inputType;
        public GameObject inputName;
        public GameObject identifier;
        public GameObject inputValue;
        public GameObject feedbackName;
        public GameObject feedbackValue;
    }
    
    [System.Serializable]
    public struct StructurePhase 
    {
        public GameObject statement1;
        public GameObject condition1;
        public GameObject result1;
        public GameObject statement2;
        public GameObject condition2;
        public GameObject result2;
    }

    [System.Serializable]
    public struct MissionStructure
    {
        public TMP_Text title;
        public TMP_Text description;
    }

    [System.Serializable]
    public struct Dialogue
    {
        public GameObject boxTip;
        public GameObject background;
        public GameObject backgroundInvisible;
        public TMP_Text dialogBox;
        public Image click;
    }

    public StructurePhase structurePhase = new StructurePhase();

    public MissionStructure missionStructure = new MissionStructure();

    public VariablesPhase variablesPhase = new VariablesPhase();

    public Dialogue dialogue = new Dialogue();

    void Awake()
    {
        missionData = MissionState.LoadFromJson();

        switch(Languages.indexLanguage)
        {
            case (int) Languages.TypesLanguages.CSharp:
                UpdateForCSharp();
                break;
            case (int) Languages.TypesLanguages.Java:
                UpdateForJava();
                break;
            case (int) Languages.TypesLanguages.Python:
                UpdateForPython();
                break;
        }
    }

    public void LoadData(int level)
    {
        missionStructure.title.text = missionData.title[level];
        missionStructure.description.text = missionData.description[level];
        List<string> options = missionData.input[level].options;

        if (level <= 4)
        {
            UpdateDropDown(variablesPhase.inputName, options);
        }
        else if (level >= 6)
        {
            UpdateDropDown(structurePhase.condition1, options);

            if (level == 9)
            {
                UpdateDropDown(structurePhase.condition2, missionData.options_for2);
            }
        }

        options = missionData.inputTypes[Languages.indexLanguage].options;
        UpdateDropDown(variablesPhase.inputType, options);
    }

    void UpdateDropDown(GameObject input, List<string> options)
    {
        TMP_Dropdown dropdown = input.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void UpdateScreen(int level)
    {
        if (level == 5) 
        {
            variablesPhasesComponents.SetActive(false);
            camerasPhaseComponents.SetActive(true);
        } 
        else if (level == 6) //change later
        {
            camerasPhaseComponents.SetActive(false);
            structuresPhasesComponents.SetActive(true);
        } 
        else if (level >= 7 && level <= 9)
        {
            bool mustActivate = level == 7 || level == 9;
            structurePhase.result1.transform.GetChild(0).GetComponent<TMP_Text>().text = missionData.resultsStructures1[level - 7];
            structurePhase.result2.transform.GetChild(0).GetComponent<TMP_Text>().text = missionData.resultsStructures2[level - 7];

            ActivateStructure2(mustActivate);
            structurePhase.statement1.SetActive(mustActivate);

            if (level == 9)
                UpdateForPhase9();
        }
    }

    //Activates the condition 2 (nested for) and updates your position
    void UpdateForPhase9()
    {
        structurePhase.statement1.transform.GetChild(0).GetComponent<TMP_Text>().text = "for";
        structurePhase.statement2.transform.GetChild(0).GetComponent<TMP_Text>().text = "for";

        structurePhase.condition2.GetComponent<TMP_Dropdown>().interactable = true;
        structurePhase.condition2.transform.GetChild(1).gameObject.SetActive(true); //Shows Arrow

        structurePhase.result1.SetActive(false); //only needs one result, it's not if else 

        structurePhase.statement2.gameObject.transform.localPosition = new Vector3(-148.46f, -126.1f, 0);
        structurePhase.condition2.transform.localPosition = new Vector3(36.1f, -126.1f, 0);
        structurePhase.result2.transform.localPosition = new Vector3(-69.9f, -167.6f, 0);
    }

    void ActivateStructure2(bool state)
    {
        structurePhase.statement2.SetActive(state); 
        structurePhase.condition2.SetActive(state);
        structurePhase.result2.SetActive(state);
    }

    public void UpdateForPython()
    {
        MissionState.OverloadFromJson(missionData, "InfoPython.json");

        /* Python doesn't require data type */
        variablesPhase.constIdentifier.SetActive(false);
        variablesPhase.inputType.SetActive(false);

        variablesPhase.inputName.transform.localPosition = new Vector3(-110.229f, -117.4f, 0);
        variablesPhase.identifier.transform.localPosition = new Vector3(-25.7f, -117.4f, 0);
        variablesPhase.inputValue.transform.localPosition = new Vector3(45.9f, -117.4f, 0);
        variablesPhase.feedbackName.transform.localPosition = new Vector3(-116.229f, 0, 0);
        variablesPhase.feedbackValue.transform.localPosition = new Vector3(-105.1f, 0, 0);
    }

    public void UpdateForCSharp()
    {
        MissionState.OverloadFromJson(missionData, "InfoCSharp.json");
    }

    public void UpdateForJava()
    {
        variablesPhase.constIdentifier.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "final";
    }

    public void ShowDialog()
    {
        dialogue.boxTip.SetActive(true);
        StartCoroutine(BuildText("Esta é sua tela de codificação!"));
        StartCoroutine(ShowBackground());
        StartCoroutine(ShowNewText("Marque como constante, já que seu nickname não mudará", 4));
        Invoke("EnableScreen", 7);
        StartCoroutine(GoToType());
        StartCoroutine(GoToName());
        StartCoroutine(GoToValue());
        StartCoroutine(GoOut());
    }

    /* Avoids the interation with screen during the presentation */
    void EnableScreen()
    {
        dialogue.backgroundInvisible.SetActive(false);
    }

    IEnumerator GoOut()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackCorrect[(int) CodingScreen.InputTypes.value].IsActive());

        StartCoroutine(ShowNewText("Ótimo! Clique no X para sair", 0));

        dialogue.click.gameObject.SetActive(true);
    }

    IEnumerator GoToValue()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackCorrect[(int) CodingScreen.InputTypes.name].IsActive());

        int currentIndex = dialogue.background.transform.GetSiblingIndex();

        StartCoroutine(ShowNewText("Parabéns! Por último, hora de escrever o seu nickname", 0));
        StartCoroutine(ShowNewText("Lembre-se que ele só pode ter 16 caracteres", 4));
        StartCoroutine(ShowNewText("Se precisar de ajuda, recorra às dicas", 8));

        yield return new WaitForSeconds(10);
        dialogue.background.transform.SetSiblingIndex(currentIndex - 2);
        dialogue.background.SetActive(false);
    }

    IEnumerator GoToName()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackCorrect[(int) CodingScreen.InputTypes.type].IsActive());

        int currentIndex = dialogue.background.transform.GetSiblingIndex();

        StartCoroutine(ShowNewText("Parabéns! Agora é hora de escolher o nome da constante", 0));
        StartCoroutine(ShowNewText("O padrão para constantes é NOME_JOGADOR", 4));

        yield return new WaitForSeconds(6);
        dialogue.background.transform.SetSiblingIndex(currentIndex - 1);
    }

    IEnumerator GoToType()
    {
        yield return new WaitUntil(() => CodingScreen.instance.feedbackIncorrect[(int) CodingScreen.InputTypes.type].IsActive());

        int currentIndex = dialogue.background.transform.GetSiblingIndex();

        StartCoroutine(ShowNewText("O feedback é associado ao tipo de dado", 0));
        StartCoroutine(ShowNewText("Nickname é um conjunto de caracteres", 4));
        string dataType = missionData.inputTypes[Languages.indexLanguage].options[(int) Mission.Types.STRING];
        StartCoroutine(ShowNewText("Então escolha o tipo " + dataType , 8));

        yield return new WaitForSeconds(10);
        dialogue.background.transform.SetSiblingIndex(currentIndex - 1);
    }

    IEnumerator ShowNewText(string text, int timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        StartCoroutine(BuildText(text));
    }

    IEnumerator ShowBackground()
    {
        yield return new WaitForSeconds(4f);
        dialogue.background.SetActive(true);
    }

    private IEnumerator BuildText(string text)
    {
        dialogue.dialogBox.SetText("");

        for (int i = 0; i < text.Length; i++){
            dialogue.dialogBox.text = string.Concat(dialogue.dialogBox.text, text[i]);
            yield return new WaitForSeconds(0.05f);
        }
    }
}


