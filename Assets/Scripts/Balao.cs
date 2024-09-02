using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balao : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] GameObject balaoFala;
    [SerializeField] GameObject nome;
    public Text balaoTexto;
    public Text nomeTexto;
    public string texto;
    [SerializeField] float typeSpeed;
    public Coroutine corrotinaDigitar;
    [Header("GameController")]
    [SerializeField] GameController gameController;
    //[SerializeField] Personagem personagem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && corrotinaDigitar != null) {

            StopCoroutine(corrotinaDigitar);
            balaoTexto.text = gameController.personagemInstancia.GetComponent<Personagem>().pedido;
            corrotinaDigitar = null;

        }

        if(balaoTexto.text == gameController.personagemInstancia.GetComponent<Personagem>().pedido && !gameController.personagemInstancia.GetComponent<Personagem>().botaoSim.interactable && !gameController.personagemInstancia.GetComponent<Personagem>().botaoNao.interactable) {

            print("Habilitar botoes");
            gameController.personagemInstancia.GetComponent<Personagem>().botaoSim.interactable = true;
            gameController.personagemInstancia.GetComponent<Personagem>().botaoNao.interactable = true;

        }
    }

    void AparecerTexto() {

        balaoFala.SetActive(true);
        nome.SetActive(true);
        nomeTexto.text = gameController.personagemInstancia.name;
        corrotinaDigitar = StartCoroutine(Digitar());

    }

    IEnumerator Digitar() {

        foreach (char letter in gameController.personagemInstancia.GetComponent<Personagem>().pedido.ToCharArray()) {

            balaoTexto.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }

    }
}
