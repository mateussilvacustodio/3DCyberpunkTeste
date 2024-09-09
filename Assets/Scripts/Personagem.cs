using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Personagem : MonoBehaviour
{
    [Header("Textos")]
    public string pedido;
    public string opcao1;
    public string opcao2;
    //
    [Header("Recursos")]
    [SerializeField] bool ponteiroGangue1;
    [SerializeField] float mudadorPonteiro1S;
    [SerializeField] float mudadorPonteiro1N;
    [SerializeField] bool ponteiroGangue2;
    [SerializeField] float mudadorPonteiro2S;
    [SerializeField] float mudadorPonteiro2N;
    [SerializeField] bool ponteiroGangue3;
    [SerializeField] float mudadorPonteiro3S;
    [SerializeField] float mudadorPonteiro3N;
    [SerializeField] bool dinheiro;
    public float mudadorDinheiroS;
    public float mudadorDinheiroN;
    [Header("Movimento")]
    [SerializeField] float velocidadeMover;
    [SerializeField] bool podeMover;
    [SerializeField] float velocidadeRodar;
    [SerializeField] bool podeRodar;
    [SerializeField] Quaternion rotacaoAlvo;
    [SerializeField] bool irEmbora;
    [Header("Botões")]
    public Button botaoSim;
    public Button botaoNao;
    [Header("Balão")]
    [SerializeField] Animator balaoAnim;
    [SerializeField] Balao balao;
    [Header("GameController")]
    [SerializeField] GameController gameController;
    void Start()
    {
        botaoSim = GameObject.Find("BotaoSim").GetComponent<Button>();
        botaoNao = GameObject.Find("BotaoNao").GetComponent<Button>();
        
        balaoAnim = GameObject.Find("Balao").GetComponent<Animator>();
        balao = GameObject.Find("Balao").GetComponent<Balao>();

        gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }
    void Update()
    {
        if(podeMover) {
            transform.position += new Vector3(-10, 0, 0) * Time.deltaTime * velocidadeMover;
        }

        if(podeRodar) {

            transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoAlvo, velocidadeRodar * Time.deltaTime);

            if(Quaternion.Angle(transform.rotation, rotacaoAlvo) < 0.1f) {

                transform.rotation = rotacaoAlvo;
                podeRodar = false;
                if(irEmbora) {

                    podeMover = true;
                    
                } else if (!irEmbora) {

                    //botaoSim.interactable = true;
                    //botaoNao.interactable = true;
                    print("setar trigger");
                    balaoAnim.SetTrigger("Aparecer");

                }

            }

        }
        
    }

    void pararEVirar() {

        podeMover = false;
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,90,0);
        podeRodar = true;

    }

    public void concordo() {

        if(ponteiroGangue1) {
            gameController.Gangue1 += mudadorPonteiro1S;
            gameController.comecarRodar1(-mudadorPonteiro1S);
        }

        if(ponteiroGangue2) {
            gameController.Gangue2 += mudadorPonteiro2S;
            gameController.comecarRodar2(-mudadorPonteiro2S);
        }

        if(ponteiroGangue3) {
            gameController.Gangue3 += mudadorPonteiro3S;
            gameController.comecarRodar3(-mudadorPonteiro3S);
        }

        if(dinheiro) {
            gameController.dinheiroValor += mudadorDinheiroS;
        }

        botaoSim.interactable = false;
        botaoNao.interactable = false;
        irEmbora = true;
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
        podeRodar = true;
        balao.balaoTexto.text = "";
        balao.nomeTexto.text = "";
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        balao.botaoSimTexto.text = "";
        balao.botaoNaoTexto.text = "";

    }

    public void discordo() {

        if(ponteiroGangue1) {
            gameController.Gangue1 += mudadorPonteiro1N;
            gameController.comecarRodar1(-mudadorPonteiro1N);
        }

        if(ponteiroGangue2) {
            gameController.Gangue2 += mudadorPonteiro2N;
            gameController.comecarRodar2(-mudadorPonteiro2N);
        }

        if(ponteiroGangue3) {
            gameController.Gangue3 += mudadorPonteiro3N;
            gameController.comecarRodar3(-mudadorPonteiro3N);
        }

        if(dinheiro) {
            gameController.dinheiroValor += mudadorDinheiroN;
        }

        botaoSim.interactable = false;
        botaoNao.interactable = false;
        irEmbora = true;
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
        podeRodar = true;
        balao.balaoTexto.text = "";
        balao.nomeTexto.text = "";
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        balao.botaoSimTexto.text = "";
        balao.botaoNaoTexto.text = "";

    }

    void OnTriggerEnter (Collider collider) {

        if(collider.gameObject.name == "Trigger") {

            pararEVirar();

        } else if(collider.gameObject.name == "Trigger2") {

            gameController.criarPersonagem();
            Destroy(this.gameObject);

        }

    }

    
}
