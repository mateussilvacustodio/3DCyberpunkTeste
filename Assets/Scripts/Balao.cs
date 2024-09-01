using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balao : MonoBehaviour
{
    [SerializeField] GameObject balaoFala;
    public Text balaoTexto;
    public string texto;
    [SerializeField] float typeSpeed;
    public Coroutine corrotinaDigitar;
    [SerializeField] Personagem personagem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {

            StopCoroutine(corrotinaDigitar);
            balaoTexto.text = personagem.pedido;

        }

        if(balaoTexto.text == personagem.pedido && !personagem.botaoSim.interactable && !personagem.botaoNao.interactable) {

            print("Habilitar botoes");
            personagem.botaoSim.interactable = true;
            personagem.botaoNao.interactable = true;

        }
    }

    void AparecerTexto() {

        balaoFala.SetActive(true);
        corrotinaDigitar = StartCoroutine(Digitar());

    }

    IEnumerator Digitar() {

        foreach (char letter in personagem.pedido.ToCharArray()) {

            balaoTexto.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }

    }
}
