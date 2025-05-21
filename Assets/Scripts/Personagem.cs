using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum tipoPedido {inventario, mercenario, nenhum}

public class Personagem : MonoBehaviour
{
    [Header("Textos")]
    public string nome;
    public string pedido;
    public tipoPedido tipoPedido;
    public Item itemDoNPC;
    public GameObject missaoMercenario;
    public Transform contentMercenario;
    [SerializeField] bool ramifica;
    [SerializeField] GameObject personagemRamificacao1;
    [SerializeField] GameObject personagemRamificacao2;
    public string opcao1;
    public string opcao2;
    public Color corGangue;
    //
    [Header("Recursos")]
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public bool[] recursos;
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public float[] mudadoresSim;
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public float[] mudadoresNao;
    [SerializeField] Inventario inventarioScript;
    [SerializeField] Mercenarios mercenarioScript;
    [Header("Movimento")]
    [SerializeField] float velocidadeMover;
    [SerializeField] bool podeMover;
    [SerializeField] Animator personagemAnim;
    //[SerializeField] AnimacaoDosPersonagens animacaoDosPersonagensScript;
    //[SerializeField] float velocidadeRodar;
    [SerializeField] bool podeRodar;
    Quaternion rotacaoAlvo = Quaternion.Euler(0, 180, 0);
    [SerializeField] bool irEmbora;
    [Header("Botões")]
    public Button botaoSim;
    public Button botaoNao;
    [SerializeField] GameObject botaoFimDoDia;
    [Header("Balão")]
    public Animator balaoAnim;
    [SerializeField] Balao balao;
    [Header("GameController")]
    [SerializeField] GameController gameController;
    [SerializeField] bool tutorial;
    [SerializeField] Tutorial tutorialScript;
    [Header("Teste")]
    public bool teste;
    void Start()
    {
        botaoSim = GameObject.Find("BotaoSim").GetComponent<Button>();
        botaoNao = GameObject.Find("BotaoNao").GetComponent<Button>();

        balaoAnim = GameObject.Find("Balao").GetComponent<Animator>();
        balao = GameObject.Find("Balao").GetComponent<Balao>();
        tutorialScript = GameObject.Find("Balao").GetComponent<Tutorial>();

        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        inventarioScript = Resources.FindObjectsOfTypeAll<Inventario>().FirstOrDefault(); //como o inventario começa desativado na cena, essa linha puxa ele mesmo desativado
        contentMercenario = Resources.FindObjectsOfTypeAll<Transform>().FirstOrDefault(t => t.gameObject.CompareTag("ContentMercenario"));
        mercenarioScript = Resources.FindObjectsOfTypeAll<Mercenarios>().FirstOrDefault();

        if (missaoMercenario != null)
        {

            //missaoMercenario.GetComponent<MissoesMercenario>().recursosMercenarios = recursos;
            missaoMercenario.GetComponent<MissoesMercenario>().mudadoresMercenarios = mudadoresSim;

        }

        botaoFimDoDia = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(b => b.gameObject.name == "BotaoFimDoDia");
        botaoFimDoDia.SetActive(false);


    }
    void Update()
    {
        if (podeMover)
        {
            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * velocidadeMover;
        }

        if (podeRodar)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoAlvo, 10f * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, rotacaoAlvo) < 0.1f)
            {

                transform.rotation = rotacaoAlvo;
                podeRodar = false;
                if (!irEmbora)
                {
                    balaoAnim.SetTrigger("Aparecer");
                }

            }

        }

    }

    public void concordo()
    {

        if (ramifica)
        {

            gameController.personagensDiaSeguinte.Add(personagemRamificacao1);

        }

        if (tipoPedido.ToString() == "inventario")
        {

            inventarioScript.GanharPerderItens(itemDoNPC);

        }

        if (tipoPedido.ToString() == "mercenario")
        {

            //print("Voce aceitou o pedido que requer um mercenario");
            GameObject novaMissao = Instantiate(missaoMercenario, contentMercenario);
            mercenarioScript.pedidosAceitos.Add(novaMissao);
            novaMissao.GetComponent<MissoesMercenario>().index = mercenarioScript.pedidosAceitos.Count - 1;
            gameController.numNotificacao++;

        }

        for (int i = 0; i < recursos.Length; i++)
        {
            if (recursos[i])
            {

                gameController.gangues[i] += mudadoresSim[i];

            }
        }

        botaoSim.interactable = false;
        botaoNao.gameObject.SetActive(true); //para o tutorial
        botaoNao.interactable = false;
        IrEmbora();
        balao.balaoTexto.text = ""; //--
        balao.nomeTexto.text = ""; //--
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        balao.botaoSimTexto.text = ""; //--
        balao.botaoNaoTexto.text = ""; //--

        if (tutorial)
        {

            tutorialScript.nomeBalaoTutorialTexto.text = "";
            tutorialScript.pedidoBalaoTutorialTexto.text = "";
            tutorialScript.botaoSimTexto.text = "";
            tutorialScript.botaoNaoTexto.text = "";

        }

    }

    public void discordo()
    {

        if (ramifica)
        {

            gameController.personagensDiaSeguinte.Add(personagemRamificacao2);

        }

        for (int i = 0; i < recursos.Length; i++)
        {
            if (recursos[i])
            {

                gameController.gangues[i] += mudadoresNao[i];

            }
        }

        botaoSim.interactable = false;
        botaoNao.interactable = false;
        IrEmbora();
        balao.balaoTexto.text = "";
        balao.nomeTexto.text = "";
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        balao.botaoSimTexto.text = "";
        balao.botaoNaoTexto.text = "";

        if (tutorial)
        {

            tutorialScript.nomeBalaoTutorialTexto.text = "";
            tutorialScript.pedidoBalaoTutorialTexto.text = "";
            tutorialScript.botaoSimTexto.text = "";
            tutorialScript.botaoNaoTexto.text = "";

        }

    }

    public void Parar()
    {

        if (!irEmbora)
        {
            podeRodar = true;
            podeMover = false;
            personagemAnim.SetBool("Andar", false);
        }

    }

    public void IrEmbora()
    {

        irEmbora = true;
        personagemAnim.SetBool("Andar", true);
        personagemAnim.SetTrigger("IrEmbora");
        podeMover = true;
        rotacaoAlvo = Quaternion.Euler(0, 270, 0);
        podeRodar = true;

    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Trigger2")
        {

            if (!tutorial)
            {

                gameController.quantidadeDePedidos++;

                if (gameController.quantidadeDePedidos < gameController.quantidadeDePedidosPorDia)
                {

                    Invoke("ChamarCriarPersonagem2", 0.5f);

                }
                else
                {

                    gameController.quantidadeDePedidos = 0;
                    //gameController.FimDoDia();
                    botaoFimDoDia.SetActive(true);

                }

            }
            else
            {

                //tutorialScript.InstanciarPersonagemTutorial();
                tutorialScript.etapasTutorial++;
                tutorialScript.Tutoriall();

            }

        }

    }

    void ChamarCriarPersonagem2()
    {
        
        gameController.CriarPersonagem2();

    }
    
}
