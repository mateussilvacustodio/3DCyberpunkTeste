using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] List<GameObject> personagensTutorial = new List<GameObject>();
    [SerializeField] GameObject personagemTutorialInstancia;
    [SerializeField] int indexTutorial;
    [SerializeField] int etapasTutorial;

    [Header("Balao")]
    
    [SerializeField] GameObject nomeBalaoTutorial;
    [SerializeField] GameObject pedidoBalaoTutorial;
    public Text nomeBalaoTutorialTexto;
    public TMP_Text pedidoBalaoTutorialTexto;
    [SerializeField] float typeSpeed;
    Coroutine corrotinaDigitar;
    [SerializeField] Animator balaoAnim;
    [SerializeField] string[] textosTutorial;
    [SerializeField] string[] textosOpcao1Tutorial;
    [SerializeField] string[] textosOpcao2Tutorial;
    [SerializeField] string[] textoNomeTutorial;

    [Header("Botoes")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    public Text botaoSimTexto;
    public Text botaoNaoTexto;

    [Header("Recursos")]
        [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
            public float[] gangues;
            [SerializeField] Image[] barrasGangues;
            //[SerializeField] Image[] barrasGangues2;
            [SerializeField] Text[] barrasGanguesPCT;
            //[SerializeField] Text[] barrasGanguesPCT2;
            [SerializeField] Text dinheiroText;
    void Start()
    {
        AtualizarBarras();
        Tutoriall();
        
    }

    void Update()
    {
        
        if(Input.GetMouseButtonUp(0) && corrotinaDigitar != null) {

            StopCoroutine(corrotinaDigitar);
            pedidoBalaoTutorialTexto.text = textosTutorial[etapasTutorial];
            corrotinaDigitar = null;

        }
        
        if(pedidoBalaoTutorialTexto.text == textosTutorial[etapasTutorial] && !botaoSim.interactable && !botaoNao.interactable) {

            botaoSimTexto.text = textosOpcao1Tutorial[etapasTutorial];
            botaoNaoTexto.text = textosOpcao2Tutorial[etapasTutorial];
            
            botaoSim.interactable = true;
            botaoNao.interactable = true;

            print("Deu certo");

        }

        dinheiroText.text = gangues[6].ToString("F0");
    }

    public void InstanciarPersonagemTutorial() {

        if(personagemTutorialInstancia != null) {
            
            Destroy(personagemTutorialInstancia);

        }
        
        personagemTutorialInstancia = Instantiate(personagensTutorial[indexTutorial]);
        indexTutorial++;

        botaoSim.onClick.RemoveAllListeners();
        botaoNao.onClick.RemoveAllListeners();
        botaoSim.onClick.AddListener(personagemTutorialInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemTutorialInstancia.GetComponent<Personagem>().discordo);

    }

    public void AparecerTextoTutorial() { //esse metódo será chamado no ultimo frame da animação do balão abrindo

        pedidoBalaoTutorial.SetActive(true);
        nomeBalaoTutorial.SetActive(true);

        //nomeBalaoTutorialTexto.text = personagemTutorialInstancia.GetComponent<Personagem>().nome;
        //nomeBalaoTutorialTexto.color = personagemTutorialInstancia.GetComponent<Personagem>().corGangue;
        //corrotinaDigitar = StartCoroutine(DigitarPersonagemTutorial());
        nomeBalaoTutorialTexto.text = textoNomeTutorial[etapasTutorial];
        corrotinaDigitar = StartCoroutine(DigitarTutorial());


    }

    IEnumerator DigitarPersonagemTutorial() {

        foreach (char letter in personagemTutorialInstancia.GetComponent<Personagem>().pedido.ToCharArray()) {

            pedidoBalaoTutorialTexto.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }

    }

    IEnumerator DigitarTutorial() {

        foreach (char letter in textosTutorial[etapasTutorial]) {

            pedidoBalaoTutorialTexto.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }

    }

    public void AtualizarBarras() {

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            float R = (100 - gangues[i]) / 50;
            float G = gangues[i] /  50;
            Color corTeste = new Color(R, G, 0f, 1f);
            barrasGangues[i].color = corTeste;    
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT[i].text = gangues[i].ToString() + "%";
        }

    }

    public void Tutoriall() {

        balaoAnim.SetTrigger("Aparecer");
        botaoSim.onClick.RemoveAllListeners();
        botaoNao.onClick.RemoveAllListeners();
        botaoSim.onClick.AddListener(Entendi);
        botaoNao.onClick.AddListener(Entendi);


    }
    
    public void Entendi() {

        
        botaoSim.interactable = false;
        botaoNao.interactable = false;
        nomeBalaoTutorialTexto.text = "";
        pedidoBalaoTutorialTexto.text = "";
        botaoSimTexto.text = "";
        botaoNaoTexto.text = "";
        balaoAnim.SetTrigger("Sumir");
        etapasTutorial++;
        InstanciarPersonagemTutorial();

    }
}
