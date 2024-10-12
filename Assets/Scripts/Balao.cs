using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Balao : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] GameObject balaoFala;
    [SerializeField] GameObject nome;
    public TextMeshProUGUI balaoTexto;
    public Text nomeTexto;
    public string texto;
    [SerializeField] float typeSpeed;
    public Coroutine corrotinaDigitar;
    bool dentroDaTag;
    [Header("GameController")]
    [SerializeField] GameController gameController;
    [Header("Botões")]
    public Text botaoSimTexto;
    public Text botaoNaoTexto;
    //[SerializeField] Personagem personagem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && corrotinaDigitar != null) {

            StopCoroutine(corrotinaDigitar);
            balaoTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().pedido;
            corrotinaDigitar = null;

        }

        if(balaoTexto.text == gameController.personagemInstancia.GetComponent<Personagem>().pedido && !gameController.personagemInstancia.GetComponent<Personagem>().botaoSim.interactable && !gameController.personagemInstancia.GetComponent<Personagem>().botaoNao.interactable) {

            botaoSimTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().opcao1;
            botaoNaoTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().opcao2;
            
            print("Habilitar botoes");
            gameController.personagemInstancia.GetComponent<Personagem>().botaoSim.interactable = true;
            gameController.personagemInstancia.GetComponent<Personagem>().botaoNao.interactable = true;

        }
    }

    void AparecerTexto() {

        balaoFala.SetActive(true);
        nome.SetActive(true);
        nomeTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().nome;
        nomeTexto.color = gameController.personagemInstancia.GetComponent<Personagem>().corGangue;
        corrotinaDigitar = StartCoroutine(Digitar());

    }

    IEnumerator Digitar() {

        

        foreach (char letter in gameController.personagemInstancia.GetComponent<Personagem>().pedido.ToCharArray()) {

            if(letter == '<') {

                dentroDaTag = true;

            }
            
            balaoTexto.text += letter;

            if(letter == '>') {

                dentroDaTag = false;

            }

            if(!dentroDaTag) {

                yield return new WaitForSeconds(typeSpeed);

            }
            

        }

    }
}
