using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Recursos")]
        [Tooltip("NB, RR, GS, SZ, NX, Policia, dinheiro")]
        public float[] gangues;
        [SerializeField] Image[] barrasGangues;
        [SerializeField] Image[] barrasGangues2;
        [SerializeField] Text dinheiroText;
        [SerializeField] string[] textoFimDeJogoGangues;
    [Header("Personagens")]
    public GameObject[] personagens;
    public int personagemIndex;
    public GameObject personagemInstancia;
    [Header("Botões")]
    [SerializeField] Button botaoSim;
    [SerializeField] Button botaoNao;
    public Button botaoTablet;
    [SerializeField] Tablet tabletScript;
    [Header("Pedidos")]
    public float dia;
    public float quantidadeDePedidos;
    public float quantidadeDePedidosPorDia;
    [SerializeField] GameObject painelFimDeDia;
    [SerializeField] GameObject painelFimDeJogo;
    [SerializeField] Text textoFimDeJogo;
    [SerializeField] bool GameOver;
    void Start()
    {
        personagemInstancia = Instantiate(personagens[personagemIndex]);
        
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }
        
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].fillAmount = barrasGangues[i].fillAmount;
        }
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

        Destroy(personagemInstancia);
        
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

    public void FimDoDia() {

        botaoTablet.interactable = false;
        tabletScript.FecharTablet();
        dia++;
        painelFimDeDia.SetActive(true);

    }

    public void ProximoDia() {

        for (int i = 0; i < gangues.Length; i++)
        {
            if(gangues[i] <= 0) {

                GameOver = true;
                textoFimDeJogo.text += "Você perdeu porque ficou sem " + textoFimDeJogoGangues[i] + "\n";
            }
        }

        if(!GameOver) {

            painelFimDeDia.SetActive(false);
            botaoTablet.interactable = true;

            for (int i = 0; i < barrasGangues.Length; i++)
            {
                barrasGangues2[i].fillAmount = barrasGangues[i].fillAmount;
            }

            criarPersonagem();

        } else {
            painelFimDeDia.SetActive(false);
            painelFimDeJogo.SetActive(true);
        }

    }

    public void Reiniciar() {

        SceneManager.LoadScene(0);

    }

}
