using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Recursos")]
        [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
        public float[] gangues;
        [SerializeField] Image[] barrasGangues;
        [SerializeField] Text dinheiroText;
    [Header("Personagens")]
    public GameObject[] personagens;
    public int personagemIndex;
    public GameObject personagemInstancia;
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    [Header("Pedidos")]
    [SerializeField] float quantidadeDePedidos;
    [SerializeField] float quantidadeDePedidosMax;
    // Start is called before the first frame update
    void Start()
    {
        
        personagemInstancia = Instantiate(personagens[personagemIndex]);
        
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }

        dinheiroText.text = gangues[6].ToString("F0");

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
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);

    }

}
