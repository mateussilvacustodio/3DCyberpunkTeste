using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    [SerializeField] Button botaoMenuReputacao;
    [SerializeField] Button botaoMenuInventario;
    [SerializeField] Button botaoMenuMercenario;
    [SerializeField] Button botaoMercanriosFimDoDia;
    [SerializeField] Button botaoFimTutorial;

[Header("Setas")]
    [SerializeField] GameObject[] setas;
    [SerializeField] GameObject seta8;
    [SerializeField] GameObject seta9;
    [SerializeField] GameObject seta10;

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

            switch (etapasTutorial)
            {
                case 0:
                    setas[0].SetActive(true);
                    break;

                case 2:
                    setas[1].SetActive(true);
                    setas[2].SetActive(true);
                    botaoMenuReputacao.interactable = true;
                    break;
                
                case 4:
                    setas[1].SetActive(true);
                    setas[2].SetActive(false);
                    setas[3].SetActive(true);
                    setas[4].SetActive(true);
                    botaoMenuInventario.interactable = true;
                    break;

                case 5:
                    botaoNao.gameObject.SetActive(false);
                    break;

                case 6:
                    setas[1].SetActive(true);
                    setas[3].SetActive(false);
                    setas[5].SetActive(true);
                    setas[6].SetActive(true);
                    setas[7].SetActive(true);
                    botaoMenuMercenario.interactable = true;
                    break;

                default:
                    break;
            }

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

            Tutoriall();

        } else if (etapasTutorial == 8) {

            botaoFimDoDia.SetActive(true);

        }

        foreach (GameObject seta in setas) //para cada variavel do tipo GameObject chamada 'seta' no array 'setas'
        {
            seta.SetActive(false);
        }

        botaoMenuReputacao.interactable = false;
        botaoMenuInventario.interactable = false;
        botaoMenuMercenario.interactable = false;

    }

    public void FimDoTutorial() {

        SceneManager.LoadScene(0);

    }

    public void SumirSetasFimDoDia(int parametro) {

        switch (parametro)
        {
            
            case 1:
                
                if(seta8.activeSelf) {

                    botaoMercanriosFimDoDia.interactable = true;
                    seta8.SetActive(false);
                    seta9.SetActive(true);

                }
                break;
            case 2:
                if(seta9.activeSelf) {

                    botaoFimTutorial.interactable = true;
                    seta9.SetActive(false);
                    seta10.SetActive(true);

                }
                break;
        }

    }

}
