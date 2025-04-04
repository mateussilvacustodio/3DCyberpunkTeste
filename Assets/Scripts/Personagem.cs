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
    //[SerializeField] bool podeRodar;
    //[SerializeField] Quaternion rotacaoAlvo;
    //[SerializeField] bool irEmbora;
    [Header("Botões")]
    public Button botaoSim;
    public Button botaoNao;
    [SerializeField] GameObject botaoFimDoDia;
    [Header("Balão")]
    public Animator balaoAnim;
    [SerializeField] Balao balao;
    [Header("GameController")]
    [SerializeField] GameController gameController;
    [Header("Teste")]
    public bool teste;
    void Start()
    {
        botaoSim = GameObject.Find("BotaoSim").GetComponent<Button>();
        botaoNao = GameObject.Find("BotaoNao").GetComponent<Button>();
        
        balaoAnim = GameObject.Find("Balao").GetComponent<Animator>();
        balao = GameObject.Find("Balao").GetComponent<Balao>();

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventarioScript = Resources.FindObjectsOfTypeAll<Inventario>().FirstOrDefault(); //como o inventario começa desativado na cena, essa linha puxa ele mesmo desativado
        contentMercenario = Resources.FindObjectsOfTypeAll<Transform>().FirstOrDefault(t => t.gameObject.CompareTag("ContentMercenario"));
        mercenarioScript = Resources.FindObjectsOfTypeAll<Mercenarios>().FirstOrDefault();
        
        if(missaoMercenario != null){
            
            //missaoMercenario.GetComponent<MissoesMercenario>().recursosMercenarios = recursos;
            missaoMercenario.GetComponent<MissoesMercenario>().mudadoresMercenarios = mudadoresSim;

        }
        

        botaoFimDoDia = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(b => b.gameObject.name == "BotaoFimDoDia");
        botaoFimDoDia.SetActive(false);


    }
    void Update()
    {
        if(podeMover) {
            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * velocidadeMover;
        }

        // if(podeRodar) {

        //     transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoAlvo, velocidadeRodar * Time.deltaTime);

        //      if(Quaternion.Angle(transform.rotation, rotacaoAlvo) < 0.1f) {

        //          transform.rotation = rotacaoAlvo;
        //          podeRodar = false;
        //          if(irEmbora) {

        //              podeMover = true;
                    
        //          } else if (!irEmbora) {

        //              //botaoSim.interactable = true;
        //              //botaoNao.interactable = true;
        //              //print("setar trigger");
        //              balaoAnim.SetTrigger("Aparecer");

        //        }

        //      }

        // }
        
    }

    // void pararEVirar() {

    //     podeMover = false;
    //     rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
    //     podeRodar = true;

    // }

    public void concordo() {

        if(tipoPedido.ToString() == "inventario"){

            inventarioScript.GanharPerderItens(itemDoNPC);

        }

        if(tipoPedido.ToString() == "mercenario") {

            //print("Voce aceitou o pedido que requer um mercenario");
            GameObject novaMissao = Instantiate(missaoMercenario, contentMercenario);
            mercenarioScript.pedidosAceitos.Add(novaMissao);
            novaMissao.GetComponent<MissoesMercenario>().index = mercenarioScript.pedidosAceitos.Count - 1;

        }
        
        for (int i = 0; i < recursos.Length; i++)
        {
            if(recursos[i]) {

                gameController.gangues[i] += mudadoresSim[i];

            }
        }
        
        botaoSim.interactable = false;
        botaoNao.interactable = false;
        //irEmbora = true;
        //rotacaoAlvo = transform.rotation * Quaternion.Euler(0,90,0);
        //podeRodar = true;
        personagemAnim.SetTrigger("IrEmbora");
        balao.balaoTexto.text = "";
        balao.nomeTexto.text = "";
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        balao.botaoSimTexto.text = "";
        balao.botaoNaoTexto.text = "";

    }

    public void discordo() {

        for (int i = 0; i < recursos.Length; i++)
        {
            if(recursos[i]) {

                gameController.gangues[i] += mudadoresNao[i];

            }
        }
        
        botaoSim.interactable = false;
        botaoNao.interactable = false;
        //irEmbora = true;
        //rotacaoAlvo = transform.rotation * Quaternion.Euler(0,90,0);
        //podeRodar = true;
        personagemAnim.SetTrigger("IrEmbora");
        balao.balaoTexto.text = "";
        balao.nomeTexto.text = "";
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        balao.botaoSimTexto.text = "";
        balao.botaoNaoTexto.text = "";

    }

    public void parar() {

        podeMover = false;

    }

    public void SetarTriggerBalao() {

        balaoAnim.SetTrigger("Aparecer");

    }

    public void andar() {

        podeMover = true;

    }

    void OnTriggerEnter (Collider collider) {

        // if(collider.gameObject.name == "Trigger") {

        //     //pararEVirar();

        // } else 
        
        if(collider.gameObject.name == "Trigger2") {

            //print("Colidiu com trigger");
            gameController.quantidadeDePedidos++;
            
            if(gameController.quantidadeDePedidos < gameController.quantidadeDePedidosPorDia) {
                
                gameController.CriarPersonagem2();

            } else {

                gameController.quantidadeDePedidos = 0;
                //gameController.FimDoDia();
                botaoFimDoDia.SetActive(true);

            }

        }

    }

    
}
