using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;

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
    [SerializeField] bool mercador;
    //
    [Header("Recursos")]
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public bool[] recursos;
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public float[] mudadoresSim;
    [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
    public float[] mudadoresNao;
    [SerializeField] GameObject textoDinheiroGanhoGasto;
    [SerializeField] Inventario inventarioScript;
    //[SerializeField] GameObject textoItemRecebido;
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
    [Header("Canvas")]
    [SerializeField] GameObject canvas;
    public Button botaoSim;
    public Button botaoNao;
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
        canvas = GameObject.Find("Canvas");

        inventarioScript = Resources.FindObjectsOfTypeAll<Inventario>().FirstOrDefault(); //como o inventario começa desativado na cena, essa linha puxa ele mesmo desativado
        contentMercenario = Resources.FindObjectsOfTypeAll<Transform>().FirstOrDefault(t => t.gameObject.CompareTag("ContentMercenario"));
        mercenarioScript = Resources.FindObjectsOfTypeAll<Mercenarios>().FirstOrDefault();

        gameController.SFXPassos.Play();

        for (int i = 0; i < gameController.gangueOlho1.Length; i++)
        {
            if (mudadoresSim[i] != 0)
            {
                gameController.gangueOlho1[i].SetActive(true);
                if (mudadoresSim[i] > 0)
                {
                    gameController.gangueOlho1[i].transform.Find("SetaCima")?.gameObject.SetActive(true);
                    gameController.gangueOlho1[i].transform.Find("SetaBaixo")?.gameObject.SetActive(false);
                }
                else
                {
                    gameController.gangueOlho1[i].transform.Find("SetaCima")?.gameObject.SetActive(false);
                    gameController.gangueOlho1[i].transform.Find("SetaBaixo")?.gameObject.SetActive(true);
                }
    
            }
            else
            {
                gameController.gangueOlho1[i].SetActive(false);
            }
        }
        if (mudadoresSim[6] != 0)
        {
            gameController.gangueOlho1Dinheiro.GameObject().SetActive(true);
            gameController.gangueOlho1Dinheiro.text = mudadoresSim[6].ToString();
        }
        else
        {
            gameController.gangueOlho1Dinheiro.GameObject().SetActive(false);
        }


        for (int i = 0; i < gameController.gangueOlho2.Length; i++)
        {
            if (mudadoresNao[i] != 0)
            {
                gameController.gangueOlho2[i].SetActive(true);
                if (mudadoresNao[i] > 0)
                {
                    gameController.gangueOlho2[i].transform.Find("SetaCima")?.gameObject.SetActive(true);
                    gameController.gangueOlho2[i].transform.Find("SetaBaixo")?.gameObject.SetActive(false);
                }
                else
                {
                    gameController.gangueOlho2[i].transform.Find("SetaCima")?.gameObject.SetActive(false);
                    gameController.gangueOlho2[i].transform.Find("SetaBaixo")?.gameObject.SetActive(true);
                }
            }
            else
            {
                gameController.gangueOlho2[i].SetActive(false);
            }
        }
        if (mudadoresNao[6] != 0)
        {
            gameController.gangueOlho2Dinheiro.GameObject().SetActive(true);
            gameController.gangueOlho2Dinheiro.text = mudadoresNao[6].ToString();
        }
        else
        {
            gameController.gangueOlho2Dinheiro.GameObject().SetActive(false);
        }

        if (missaoMercenario != null)
        {

            //missaoMercenario.GetComponent<MissoesMercenario>().recursosMercenarios = recursos;
            missaoMercenario.GetComponent<MissoesMercenario>().mudadoresMercenarios = mudadoresSim;

        }

        //botaoFimDoDia = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(b => b.gameObject.name == "BotaoFimDoDia");
        //botaoFimDoDia.SetActive(false);


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

        gameController.SFXBotao.Play();

        if (ramifica)
        {

            gameController.personagensDiaSeguinte.Add(personagemRamificacao1);
            if (nome == "David")
            {

                gameController.cheatDavid = true;

            }

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

        if (mudadoresSim[6] != 0)
        {

            string textoDinheiroExibir = mudadoresSim[6].ToString("+0;-0;0");
            textoDinheiroGanhoGasto.GetComponent<TMP_Text>().text = textoDinheiroExibir;
            GameObject instanciaDinheiro = Instantiate(textoDinheiroGanhoGasto, canvas.transform);
            Destroy(instanciaDinheiro, 0.75f);

        }

        if (!mercador)
        {
            //instanciar
            botaoSim.interactable = false;
            botaoNao.gameObject.SetActive(true); //para o tutorial
            botaoNao.interactable = false;
            gameController.botaoCiberOlho.interactable = false;
            if (gameController.ciberOlhoUsadoNessePedido)
            {
                gameController.quantidadeCiberOlho -= 1;
            }
            IrEmbora();
            balao.balaoTexto.text = ""; //--
            balao.nomeTexto.text = ""; //--
            balao.corrotinaDigitar = null;
            balaoAnim.SetTrigger("Sumir");
            balao.botaoSimTexto.text = ""; //--
            balao.botaoNaoTexto.text = ""; //--

        }
        else
        {

            print("Mercador");
            this.gameObject.GetComponent<PersonagemMercador>().AbrirMercador();

        }
        
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

        gameController.SFXBotao.Play();

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
        gameController.botaoCiberOlho.interactable = false;
        if (gameController.ciberOlhoUsadoNessePedido)
        {
            gameController.quantidadeCiberOlho -= 1;
        }
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

        gameController.SFXPassos.Stop();
        if (!irEmbora)
        {
            podeRodar = true;
            podeMover = false;
            personagemAnim.SetBool("Andar", false);
        }

    }

    public void IrEmbora()
    {

        gameController.SFXPassos.Play();
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
                    gameController.tVEffectScript.tvEffectAnim.SetTrigger("Fechar");
                    //gameController.FimDoDia();
                    //botaoFimDoDia.SetActive(true);

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
