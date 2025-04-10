using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] List<GameObject> personagensTutorial = new List<GameObject>();
    public GameObject personagemTutorialInstancia;
    [SerializeField] int indexTutorial;
    public int etapasTutorial;
    [SerializeField] Inventario inventarioScript;

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
    [SerializeField] GameObject botaoFimDoDia;

    void Start()
    {
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
            corrotinaDigitar = null;

        }

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
        nomeBalaoTutorialTexto.text = textoNomeTutorial[etapasTutorial];
        corrotinaDigitar = StartCoroutine(DigitarTutorial());


    }

    IEnumerator DigitarTutorial() {

        foreach (char letter in textosTutorial[etapasTutorial]) {

            pedidoBalaoTutorialTexto.text += letter;
            yield return new WaitForSeconds(typeSpeed);

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
        etapasTutorial++;
        balaoAnim.SetTrigger("Sumir");

        if(etapasTutorial < 6) {

            InstanciarPersonagemTutorial();

        } else if (etapasTutorial == 7){

            print("Ultimo tutorial");
            Tutoriall();

        } else if (etapasTutorial == 8) {

            botaoFimDoDia.SetActive(true);

        }

    }

    public void FimDoTutorial() {

        SceneManager.LoadScene(0);

    }

}
