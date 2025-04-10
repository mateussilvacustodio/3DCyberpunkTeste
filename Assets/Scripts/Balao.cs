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
    [SerializeField] bool tutorial;
    [SerializeField] Tutorial tutorialScript;
    [Header("Botões")]
    public Text botaoSimTexto;
    public Text botaoNaoTexto;
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && corrotinaDigitar != null) {

            StopCoroutine(corrotinaDigitar);
            balaoTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().pedido;
            corrotinaDigitar = null;

        }

        if(gameController.personagemInstancia != null) {

            if(balaoTexto.text == gameController.personagemInstancia.GetComponent<Personagem>().pedido && !gameController.personagemInstancia.GetComponent<Personagem>().botaoSim.interactable && !gameController.personagemInstancia.GetComponent<Personagem>().botaoNao.interactable) {

            botaoSimTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().opcao1;
            botaoNaoTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().opcao2;

            gameController.personagemInstancia.GetComponent<Personagem>().botaoSim.interactable = true;
            gameController.personagemInstancia.GetComponent<Personagem>().botaoNao.interactable = true;

            }

        }
        
    }

    void AparecerTexto() {

        if(!tutorial) {

            balaoFala.SetActive(true);
            nome.SetActive(true);
            nomeTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().nome;
            nomeTexto.color = gameController.personagemInstancia.GetComponent<Personagem>().corGangue;
            corrotinaDigitar = StartCoroutine(Digitar());

        } else {

            tutorialScript.AparecerTextoTutorial();

        }

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
