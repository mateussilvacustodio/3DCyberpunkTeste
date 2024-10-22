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
        [SerializeField] Text[] barrasGanguesPCT;
        [SerializeField] Text[] barrasGanguesPCT2;
        [SerializeField] Text dinheiroText;
        [SerializeField] string[] textoFimDeJogoGangues;
    [Header("Personagens")]
    public GameObject[] personagens;
    [SerializeField] List<int> indicesDisponiveis = new List<int>(); 
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
    [SerializeField] Text textoFimDoDia;
    [SerializeField] GameObject painelFimDeJogo;
    [SerializeField] Text fimDeJogo;
    [SerializeField] Text textoFimDeJogo;
    [SerializeField] bool GameOver;
    [Header("Cores")]
    public Color corTeste;
    public float R;
    public float G;
    void Start()
    {   
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            R = (100 - gangues[i]) / 50;
            G = gangues[i] /  50;
            corTeste = new Color(R, G, 0f, 1f);
            barrasGangues[i].color = corTeste;    
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT[i].text = gangues[i].ToString() + "%";
        }

        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].fillAmount = barrasGangues[i].fillAmount;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].color = barrasGangues[i].color;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT2[i].text = barrasGanguesPCT[i].text;
        }


        for (int i = 0; i < personagens.Length; i++) {

            indicesDisponiveis.Add(i);

        }

        criarPersonagem();
    }

    void Update()
    {
        // for (int i = 0; i < barrasGangues.Length; i++)
        // {
        //     barrasGangues[i].fillAmount = gangues[i] / 100;
        // }
        dinheiroText.text = gangues[6].ToString("F0");

        // for (int i = 0; i < barrasGangues.Length; i++)
        // {
        //     R = (100 - gangues[i]) / 50;
        //     G = gangues[i] /  50;
        //     corTeste = new Color(R, G, 0f, 1f);
        //     barrasGangues[i].color = corTeste;    
        // }

        // for (int i = 0; i < barrasGangues.Length; i++)
        // {
        //     barrasGanguesPCT[i].text = gangues[i].ToString() + "%";
        // }

    }

    public void criarPersonagem() {

        if(personagemInstancia != null) {
            
            Destroy(personagemInstancia);

        }       
        
        int aleatoria = Random.Range(0,indicesDisponiveis.Count);

        personagemInstancia = Instantiate(personagens[indicesDisponiveis[aleatoria]]);
        personagemIndex = aleatoria;
        botaoSim.onClick.RemoveAllListeners();
        botaoNao.onClick.RemoveAllListeners();
        botaoSim.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().concordo);
        botaoNao.onClick.AddListener(personagemInstancia.GetComponent<Personagem>().discordo);

        indicesDisponiveis.RemoveAt(aleatoria);

        if(indicesDisponiveis.Count == 0) {

            for (int i = 0; i < personagens.Length; i++) {

            indicesDisponiveis.Add(i);
            }

        }

    }

    public void FimDoDia() {

        botaoTablet.interactable = false;
        tabletScript.FecharTablet();
        dia++;
        float DiaImpresso = dia - 1;
        textoFimDoDia.text = "Fim do dia " + DiaImpresso.ToString();
        
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues[i].fillAmount = gangues[i] / 100;
        }
        dinheiroText.text = gangues[6].ToString("F0");
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            R = (100 - gangues[i]) / 50;
            G = gangues[i] /  50;
            corTeste = new Color(R, G, 0f, 1f);
            barrasGangues[i].color = corTeste;    
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT[i].text = gangues[i].ToString() + "%";
        }
        
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].fillAmount = barrasGangues[i].fillAmount;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGangues2[i].color = barrasGangues[i].color;
        }
        for (int i = 0; i < barrasGangues.Length; i++)
        {
            barrasGanguesPCT2[i].text = barrasGanguesPCT[i].text;
        }
        
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

            criarPersonagem();

        } else {
            painelFimDeDia.SetActive(false);
            float DiaImpresso = dia - 1;
            fimDeJogo.text = "Fim de jogo no dia " + DiaImpresso.ToString();
            painelFimDeJogo.SetActive(true);
        }

    }

    public void Reiniciar() {

        SceneManager.LoadScene(0);

    }

}
