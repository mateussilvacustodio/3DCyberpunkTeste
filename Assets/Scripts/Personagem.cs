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
    [SerializeField] GameObject ponteiro;
    [SerializeField] GameObject dinheiro;
    //[SerializeField] GameController gameController;
    public float mudadorDinheiro;
    public float mudadorImplantes;
    [Header("Movimento")]
    [SerializeField] float velocidadeMover;
    [SerializeField] bool podeMover;
    [SerializeField] float velocidadeRodar;
    [SerializeField] bool podeRodar;
    [SerializeField] Quaternion rotacaoAlvo;
    [SerializeField] bool irEmbora;
    [SerializeField] float tempoPraParar;
    [Header("Botões")]
    public Button botaoSim;
    public Button botaoNao;
    [Header("Balão")]
    [SerializeField] Animator balaoAnim;
    [SerializeField] Balao balao;
    // Start is called before the first frame update
    void Start()
    {
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
    }

    // Update is called once per frame
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
                    balaoAnim.SetTrigger("Aparecer");

                }

            }

        }

        //tempoPraParar -= Time.deltaTime;

        // if(tempoPraParar <= 0) {

        //     pararEVirar();

        // }


        if(Input.GetKeyDown(KeyCode.A)) {

            podeMover = false;
            rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
            podeRodar = true;

        }

        if(Input.GetKeyDown(KeyCode.B)) {

            rotacaoAlvo = transform.rotation * Quaternion.Euler(0,90,0);
            podeRodar = true;
            irEmbora = true;

        }
        
    }

    void pararEVirar() {

        podeMover = false;
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,-90,0);
        podeRodar = true;

    }

    public void VaiTeEmbora() {

        botaoSim.interactable = false;
        botaoNao.interactable = false;
        irEmbora = true;
        rotacaoAlvo = transform.rotation * Quaternion.Euler(0,90,0);
        podeRodar = true;
        balao.balaoTexto.text = "";
        balao.corrotinaDigitar = null;
        balaoAnim.SetTrigger("Sumir");
        
    }

    void OnTriggerEnter (Collider collider) {

        if(collider.gameObject.name == "Trigger") {

            pararEVirar();

        }

    }

    
}
