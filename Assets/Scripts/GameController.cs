using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Recursos")]
    [SerializeField] float dinheiroValor;
    [SerializeField] float Gangue1;
    [SerializeField] float Gangue2;
    [SerializeField] float Gangue3;
    [SerializeField] Text dinheiroText;
    [Header("Personagens")]
    public GameObject[] personagens;
    public int personagemIndex;
    public GameObject personagemInstancia;
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    // Start is called before the first frame update
    void Start()
    {
        
        personagemInstancia = Instantiate(personagens[personagemIndex]);
        
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().VaiTeEmbora);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().VaiTeEmbora);
    }

    // Update is called once per frame
    void Update()
    {
        //dinheiroText.text = dinheiroValor.ToString("F0");
    }

    public void criarPersonagem() {

        int aleatoria = Random.Range(0,personagens.Length);

        while(aleatoria == personagemIndex) {

            aleatoria = Random.Range(0,personagens.Length);

        }

        personagemInstancia = Instantiate(personagens[aleatoria]);
        personagemIndex = aleatoria;
        botaoSim.onClick.RemoveAllListeners();
        botaoNao.onClick.RemoveAllListeners();
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().VaiTeEmbora);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().VaiTeEmbora);

    }
}
