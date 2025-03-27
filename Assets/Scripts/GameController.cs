using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EventosFimDeDia {

    public string textoDoEvento;
    public int quantasGanguesAfeta;
    public int[] gangueQueAfeta;
    public float[] quantoAfetaNaGangue;

}

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
        [SerializeField] List<EventosFimDeDia> ListaDeEventosFimDeDia = new List<EventosFimDeDia>();
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
        [SerializeField] float diaMax;
        public float quantidadeDePedidos;
        public float quantidadeDePedidosPorDia;
        [SerializeField] GameObject painelFimDeDia;
        [SerializeField] Text textoFimDoDia;
        [SerializeField] Text textoEventoFimDoDia;
        [SerializeField] GameObject painelFimDeJogo;
        [SerializeField] Text fimDeJogo;
        [SerializeField] Text textoFimDeJogo;
        [SerializeField] bool GameOver;
        [SerializeField] GameObject painelVitoria;
    [Header("Cores")]
        public Color corTeste;
        public float R;
        public float G;
    [Header("Inventario")]
        [SerializeField] Inventario inventario;
        public bool HaEncomenda;
        public List<Item> itensEncomendados = new List<Item>();
        public List<GameObject> personagPraQuemDevo = new List<GameObject>();
        public ParametrosEncomendas parametrosEncomendasGC;
    [Header("Mercenarios")]
        [SerializeField] Mercenarios mercenarioScript;
        [SerializeField] bool HaMissoesDeMercenario;
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
        dinheiroText.text = gangues[6].ToString("F0");
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

        if(HaEncomenda) {

            inventario.Encomendar();
            
        }
        inventario.PagarDivida();

        for (int i = 0; i < mercenarioScript.pedidosAceitos.Count; i++)
        {
            if(mercenarioScript.pedidosAceitos[i] != null) {

                for (int j = 0; j < gangues.Length; j++)
                {
                    gangues[j] -= mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().mudadoresMercenarios[j] * 2;
                }

                mercenarioScript.textoFimDoDia.text += "A missão de " + mercenarioScript.pedidosAceitos[i].GetComponent<MissoesMercenario>().nomeNPC + " não foi executada \n";
                Destroy(mercenarioScript.pedidosAceitos[i]);
                
            }
        }

        for (int i = 0; i < mercenarioScript.pedidosFalhos.Count; i++)
        {
            for (int j = 0; j < gangues.Length; j++)
            {
                gangues[j] -= mercenarioScript.pedidosFalhos[i].GetComponent<MissoesMercenario>().mudadoresMercenarios[j] * 2;
            }

            Destroy(mercenarioScript.pedidosFalhos[i]);
        }
        
        botaoTablet.interactable = false;
        tabletScript.FecharTablet();
        dia++;
        float DiaImpresso = dia - 1;
        textoFimDoDia.text = "Fim do dia " + DiaImpresso.ToString();

        gangues[6] -= 100;
        int eventoAleatorio = Random.Range(0, ListaDeEventosFimDeDia.Count);
        //int eventoAleatorio = 0;
        textoEventoFimDoDia.text = ListaDeEventosFimDeDia[eventoAleatorio].textoDoEvento;
        

        for (int i = 0; i < gangues.Length; i++)
        {
            
            for (int j = 0; j < ListaDeEventosFimDeDia[eventoAleatorio].quantasGanguesAfeta; j++)
            {
                                
                if(i == ListaDeEventosFimDeDia[eventoAleatorio].gangueQueAfeta[j]) {

                    gangues[i] += ListaDeEventosFimDeDia[eventoAleatorio].quantoAfetaNaGangue[j];

                }
            }
            
        }

        ListaDeEventosFimDeDia.RemoveAt(eventoAleatorio);
        
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
        
        inventario.textoEncomendaFimDoDia.text = "";
        inventario.textoDevidosFimDoDia.text = "";
        mercenarioScript.textoFimDoDia.text = "";
        
        for (int i = 0; i < gangues.Length; i++) //confere o valor de todas as gangues do jogo (incluindo o dinheiro)
        {
            if(gangues[i] <= 0) { //se alguma for menor que zero...

                GameOver = true; //... o jogo acaba...
                textoFimDeJogo.text += "Você perdeu porque ficou sem " + textoFimDeJogoGangues[i] + "\n";
                //...e se o jogad ficar sem reputação com mais de uma gangue, mais de uma mensagem será exibida
            }
        }

        if(!GameOver) {

            if(dia < diaMax + 1) {

                painelFimDeDia.SetActive(false);
                botaoTablet.interactable = true;
                criarPersonagem();

            } else {

                painelFimDeDia.SetActive(false);
                painelVitoria.SetActive(true);

            }           

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
